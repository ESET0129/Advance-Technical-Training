//Meter Repository Implementation:AMI_project.Api/Repository/MeterRepository.cs
using AMI_project.Dtos;
using AMI_project.Models;
using Microsoft.EntityFrameworkCore;


namespace AMI_project.Repository
{
    public class MeterRepository : IMeterRepository
    {
        private readonly AmidbContext _context;

        public MeterRepository(AmidbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Meter>, int)> GetMetersAsync(MeterQueryParameters queryParams)
        {
            // Start with the base query. AsNoTracking() improves read performance.
            var query = _context.Meters.AsNoTracking();

            // --- Apply Filtering ---
            if (!string.IsNullOrEmpty(queryParams.SerialNo))
            {
                query = query.Where(m => m.MeterSerialNo.Contains(queryParams.SerialNo));
            }

            if (!string.IsNullOrEmpty(queryParams.Status))
            {
                query = query.Where(m => m.Status == queryParams.Status);
            }

            if (queryParams.InstallDate.HasValue)
            {
                query = query.Where(m => m.InstallTsUtc.Date == queryParams.InstallDate.Value.Date);
            }

            // --- Get Total Count (for pagination) ---
            // This must be done *before* sorting and paging
            var totalCount = await query.CountAsync();

            // --- Apply Sorting ---
            // This is a basic example. A real-world app might use a helper
            // to handle sorting by different columns.
            if (queryParams.SortOrder?.ToLower() == "desc")
            {
                query = query.OrderByDescending(m => m.MeterSerialNo);
            }
            else
            {
                query = query.OrderBy(m => m.MeterSerialNo);
            }

            // --- Apply Paging ---
            query = query.Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
                         .Take(queryParams.PageSize);

            var meters = await query.ToListAsync();

            return (meters, totalCount);
        }

        public async Task<Meter> GetMeterByIdAsync(string meterSerialNo)
        {
            return await _context.Meters.AsNoTracking()
                .FirstOrDefaultAsync(m => m.MeterSerialNo == meterSerialNo);
        }

        public async Task<Meter> CreateMeterAsync(MeterCreateUpdateDto meterDto)
        {
            var meter = new Meter
            {
                MeterSerialNo = meterDto.MeterSerialNo,
                IpAddress = meterDto.IpAddress,
                Iccid = meterDto.Iccid,
                Imsi = meterDto.Imsi,
                Manufacturer = meterDto.Manufacturer,
                Firmware = meterDto.Firmware,
                Category = meterDto.Category,
                InstallTsUtc = meterDto.InstallTsUtc,
                Status = meterDto.Status,
                ConsumerId = meterDto.ConsumerId,
                OrgUnitId = meterDto.OrgUnitId,
                TariffId = meterDto.TariffId
            };

            await _context.Meters.AddAsync(meter);
            await _context.SaveChangesAsync();
            return meter;
        }

        public async Task<Meter> UpdateMeterAsync(string meterSerialNo, MeterCreateUpdateDto meterDto)
        {
            var meter = await _context.Meters.FindAsync(meterSerialNo);
            if (meter == null) return null;

            // Update properties
            meter.IpAddress = meterDto.IpAddress;
            meter.Iccid = meterDto.Iccid;
            meter.Imsi = meterDto.Imsi;
            meter.Manufacturer = meterDto.Manufacturer;
            meter.Firmware = meterDto.Firmware;
            meter.Category = meterDto.Category;
            meter.InstallTsUtc = meterDto.InstallTsUtc;
            meter.Status = meterDto.Status;
            meter.ConsumerId = meterDto.ConsumerId;
            meter.OrgUnitId = meterDto.OrgUnitId;
            meter.TariffId = meterDto.TariffId;

            await _context.SaveChangesAsync();
            return meter;
        }

        public async Task<bool> DeleteMeterAsync(string meterSerialNo)
        {
            var meter = await _context.Meters.FindAsync(meterSerialNo);
            if (meter == null) return false;

            _context.Meters.Remove(meter);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MeterExistsAsync(string meterSerialNo)
        {
            return await _context.Meters.AnyAsync(m => m.MeterSerialNo == meterSerialNo);
        }


        //new code for daily reading
       
       
    }
}
