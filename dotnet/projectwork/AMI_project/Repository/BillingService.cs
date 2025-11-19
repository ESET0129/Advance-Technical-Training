using AMI_project.Dtos;
using AMI_project.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace AMI_project.Repository
{
    public class BillingService : IBillingService
    {
        private readonly AmidbContext _context;

        public BillingService(AmidbContext context)
        {
            _context = context;
        }

        public async Task<GenerateBillsResponseDto> GenerateMonthlyBillsAsync(string billingMonth)
        {
            var response = new GenerateBillsResponseDto();

            if (!DateTime.TryParseExact(billingMonth + "-01", "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var monthDate))
            {
                response.Errors.Add("Invalid billing month format. Use YYYY-MM.");
                return response;
            }
            var billingMonthDateOnly = DateOnly.FromDateTime(monthDate);
            var year = monthDate.Year;
            var month = monthDate.Month;

            var meters = await _context.Meters.Include(m => m.Tariff).ToListAsync();

            foreach (var meter in meters)
            {
                try
                {
                    bool billExists = await _context.MonthlyBills.AnyAsync(b =>
                        b.MeterSerialNo == meter.MeterSerialNo &&
                        b.BillingMonth == billingMonthDateOnly);

                    if (billExists)
                    {
                        response.AlreadyExist++;
                        continue;
                    }

                    decimal totalKwh = await _context.DailyReadings
                        .Where(r => r.MeterSerialNo == meter.MeterSerialNo &&
                                    r.ReadingDate.Year == year &&
                                    r.ReadingDate.Month == month)
                        .SumAsync(r => r.ReadingKwh);

                    if (totalKwh <= 0)
                    {
                        response.NoReadingsFound++;
                        continue;
                    }

                    var slabs = await _context.TariffSlabs
                        .Where(s => s.TariffId == meter.TariffId)
                        .OrderBy(s => s.FromKwh)
                        .ToListAsync();

                    // --- Simplified Slab Calculation ---
                    decimal slabCharge = 0;
                    var kwhToProcess = totalKwh;
                    foreach (var slab in slabs)
                    {
                        if (kwhToProcess <= 0) break;
                        if (totalKwh < slab.FromKwh) continue;

                        // Calculate the start and end of the billable amount in this slab
                        decimal slabStart = slab.FromKwh;
                        decimal slabEnd = (slab.ToKwh >= 999999) ? decimal.MaxValue : slab.ToKwh;

                        // kwh already billed in previous (lower) slabs
                        decimal kwhAlreadyBilled = totalKwh - kwhToProcess;

                        decimal billableKwhInSlab = Math.Min(totalKwh, slabEnd) - Math.Max(slabStart, kwhAlreadyBilled);

                        // Adjust for gaps like 0-100, 101-300
                        if (slab.FromKwh > 0 && kwhAlreadyBilled < slab.FromKwh)
                        {
                            billableKwhInSlab = Math.Min(kwhToProcess, slabEnd - slab.FromKwh + 1); // +1 to include 101
                        }

                        if (billableKwhInSlab < 0) billableKwhInSlab = 0;

                        slabCharge += billableKwhInSlab * slab.RatePerKwh;
                        kwhToProcess -= billableKwhInSlab;
                    }

                    var newBill = new MonthlyBill
                    {
                        MeterSerialNo = meter.MeterSerialNo,
                        BillingMonth = billingMonthDateOnly,
                        TotalKwh = totalKwh,
                        BaseRateApplied = meter.Tariff.BaseRate,
                        SlabCharge = slabCharge,
                        TotalBillAmount = meter.Tariff.BaseRate + slabCharge,
                        Status = "Unpaid",
                        DueDate = billingMonthDateOnly.AddMonths(1).AddDays(20)
                    };

                    _context.MonthlyBills.Add(newBill);
                    response.SuccessfullyGenerated++;
                }
                catch (Exception ex)
                {
                    response.Errors.Add($"Failed for {meter.MeterSerialNo}: {ex.Message}");
                }
            }

            await _context.SaveChangesAsync();
            return response;
        }
    }
}