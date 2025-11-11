using AMI_project.Dtos;
using AMI_project.Models;
using Microsoft.EntityFrameworkCore;

namespace AMI_project.Repository
{
    public class BillingRepository : IBillingRepository
    {
        private readonly AmidbContext _context;

        public BillingRepository(AmidbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<BillViewDto>, int)> GetBillsAsync(BillQueryParameters queryParams)
        {
            var query = _context.MonthlyBills.AsNoTracking();

            // --- Apply Filtering ---
            if (!string.IsNullOrEmpty(queryParams.MeterSerialNo))
            {
                query = query.Where(b => b.MeterSerialNo.Contains(queryParams.MeterSerialNo));
            }
            if (!string.IsNullOrEmpty(queryParams.Status))
            {
                query = query.Where(b => b.Status == queryParams.Status);
            }
            if (queryParams.BillingMonth.HasValue)
            {
                var monthStart = queryParams.BillingMonth.Value;
                query = query.Where(b => b.BillingMonth == monthStart);
            }

            // Get Total Count
            var totalCount = await query.CountAsync();

            // Apply Sorting & Paging
            query = query.OrderByDescending(b => b.BillingMonth).ThenBy(b => b.MeterSerialNo)
                         .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
                         .Take(queryParams.PageSize);

            // --- THIS IS THE FIX ---
            // 1. Pull the data from SQL into memory
            var billsFromDb = await query.ToListAsync();

            // 2. Now, in C# memory, safely convert to our DTO
            var bills = billsFromDb.Select(b => new BillViewDto
            {
                BillId = b.BillId,
                MeterSerialNo = b.MeterSerialNo,
                BillingMonth = b.BillingMonth,
                TotalKwh = b.TotalKwh,
                BaseRateApplied = b.BaseRateApplied,
                SlabCharge = b.SlabCharge,
                TotalBillAmount = b.TotalBillAmount,
                Status = b.Status,
                DueDate = b.DueDate
            }).ToList();

            return (bills, totalCount);
        }

        public async Task<MonthlyBill> UpdateBillStatusAsync(long billId, string status)
        {
            var bill = await _context.MonthlyBills.FindAsync(billId);
            if (bill == null) return null;

            bill.Status = status;
            await _context.SaveChangesAsync();
            return bill;
        }
    }
}
