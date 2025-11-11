using Microsoft.EntityFrameworkCore;
using AMI_project.Dtos;
using AMI_project.Models;
using CsvHelper;

using System.Globalization;

namespace AMI_project.Repository
{
    public class MeterUploadService : IMeterUploadService
    {
        private readonly AmidbContext _context;

        public MeterUploadService(AmidbContext context)
        {
            _context = context;
        }

        public async Task<UploadResultDto> ProcessMeterUploadAsync(IFormFile file)
        {
            var result = new UploadResultDto();
            var metersToCreate = new List<Meter>();

            // We need to get existing keys to prevent duplicates
            var existingSerialNumbers = await _context.Meters.Select(m => m.MeterSerialNo).ToListAsync();
            var existingIccids = await _context.Meters.Select(m => m.Iccid).ToListAsync();
            var existingImsis = await _context.Meters.Select(m => m.Imsi).ToListAsync(); ;

            // We also need to validate foreign keys
            var existingConsumerIds = await _context.Consumers.Select(c => c.ConsumerId).ToListAsync();
            var existingOrgUnitIds = await _context.OrgUnits.Select(o => o.OrgUnitId).ToListAsync();
            var existingTariffIds = await _context.Tariffs.Select(t => t.TariffId).ToListAsync();

            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<MeterCsvRecordDto>().ToList();
                    result.TotalRows = records.Count;

                    for (int i = 0; i < records.Count; i++)
                    {
                        var record = records[i];
                        int rowNumber = i + 2; // +1 for 0-index, +1 for header row

                        // --- Validation ---
                        if (string.IsNullOrWhiteSpace(record.MeterSerialNo))
                        {
                            result.ErrorMessages.Add($"Row {rowNumber}: MeterSerialNo is required.");
                            continue;
                        }
                        if (existingSerialNumbers.Contains(record.MeterSerialNo))
                        {
                            result.ErrorMessages.Add($"Row {rowNumber}: MeterSerialNo '{record.MeterSerialNo}' already exists.");
                            continue;
                        }
                        if (existingIccids.Contains(record.Iccid))
                        {
                            result.ErrorMessages.Add($"Row {rowNumber}: ICCID '{record.Iccid}' already exists.");
                            continue;
                        }
                        if (existingImsis.Contains(record.Imsi))
                        {
                            result.ErrorMessages.Add($"Row {rowNumber}: IMSI '{record.Imsi}' already exists.");
                            continue;
                        }
                        if (!existingConsumerIds.Contains(record.ConsumerId))
                        {
                            result.ErrorMessages.Add($"Row {rowNumber}: ConsumerId '{record.ConsumerId}' does not exist.");
                            continue;
                        }
                        if (!existingOrgUnitIds.Contains(record.OrgUnitId))
                        {
                            result.ErrorMessages.Add($"Row {rowNumber}: OrgUnitId '{record.OrgUnitId}' does not exist.");
                            continue;
                        }
                        if (!existingTariffIds.Contains(record.TariffId))
                        {
                            result.ErrorMessages.Add($"Row {rowNumber}: TariffId '{record.TariffId}' does not exist.");
                            continue;
                        }

                        // --- Add to list ---
                        var meter = new Meter
                        {
                            MeterSerialNo = record.MeterSerialNo,
                            IpAddress = record.IpAddress,
                            Iccid = record.Iccid,
                            Imsi = record.Imsi,
                            Manufacturer = record.Manufacturer,
                            Firmware = record.Firmware,
                            Category = record.Category,
                            InstallTsUtc = record.InstallTsUtc.ToUniversalTime(),
                            Status = record.Status,
                            ConsumerId = record.ConsumerId,
                            OrgUnitId = record.OrgUnitId,
                            TariffId = record.TariffId
                        };
                        metersToCreate.Add(meter);

                        // Add to our sets to catch duplicates *within the same file*
                        existingSerialNumbers.Add(meter.MeterSerialNo);
                        existingIccids.Add(meter.Iccid);
                        existingImsis.Add(meter.Imsi);
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessages.Add($"File processing failed: {ex.Message}");
                return result;
            }

            // --- Database Insertion ---
            if (metersToCreate.Any())
            {
                await _context.Meters.AddRangeAsync(metersToCreate);
                await _context.SaveChangesAsync();
            }

            result.SuccessfullyImported = metersToCreate.Count;
            result.FailedRows = result.TotalRows - result.SuccessfullyImported;

            return result;
        }
    }
}
