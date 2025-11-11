using AMI_project.Dtos;
using AMI_project.Models;
using Microsoft.EntityFrameworkCore;

namespace AMI_project.Repository
{
    public class TariffRepository : ITariffRepository
    {
        private readonly AmidbContext _context;

        public TariffRepository(AmidbContext context)
        {
            _context = context;
        }

        // --- TARIFF METHODS ---

        public async Task<IEnumerable<Tariff>> GetTariffsAsync()
        {
            // Get all tariffs, ordered by name.
            return await _context.Tariffs.AsNoTracking()
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<Tariff> GetTariffByIdAsync(int tariffId)
        {
            return await _context.Tariffs.AsNoTracking()
                .FirstOrDefaultAsync(t => t.TariffId == tariffId);
        }

        //public async Task<Tariff> CreateTariffAsync(TariffCreateUpdateDto tariffDto)
        //{
        //    var tariff = new Tariff
        //    {
        //        Name = tariffDto.Name,
        //        EffectiveFrom = tariffDto.EffectiveFrom,
        //        EffectiveTo = tariffDto.EffectiveTo,
        //        BaseRate = tariffDto.BaseRate,
        //        TaxRate = tariffDto.TaxRate
        //    };

        //    await _context.Tariffs.AddAsync(tariff);
        //    await _context.SaveChangesAsync();
        //    return tariff;
        //}
        public async Task<Tariff> CreateTariffAsync(TariffCreateUpdateDto tariffDto)
        {
            var tariff = new Tariff
            {
                Name = tariffDto.Name,
                // FIX: Explicitly convert DateTime to DateOnly
                EffectiveFrom = DateOnly.FromDateTime(tariffDto.EffectiveFrom),
                // FIX: Handle the nullable DateTime?
                EffectiveTo = tariffDto.EffectiveTo.HasValue
                    ? DateOnly.FromDateTime(tariffDto.EffectiveTo.Value)
                    : null,
                BaseRate = tariffDto.BaseRate,
                TaxRate = tariffDto.TaxRate
            };

            await _context.Tariffs.AddAsync(tariff);
            await _context.SaveChangesAsync();
            return tariff;
        }

        //public async Task<Tariff> UpdateTariffAsync(int tariffId, TariffCreateUpdateDto tariffDto)
        //{
        //    var tariff = await _context.Tariffs.FindAsync(tariffId);
        //    if (tariff == null) return null;

        //    tariff.Name = tariffDto.Name;
        //    tariff.EffectiveFrom = tariffDto.EffectiveFrom;
        //    tariff.EffectiveTo = tariffDto.EffectiveTo;
        //    tariff.BaseRate = tariffDto.BaseRate;
        //    tariff.TaxRate = tariffDto.TaxRate;

        //    await _context.SaveChangesAsync();
        //    return tariff;
        //}
        public async Task<Tariff> UpdateTariffAsync(int tariffId, TariffCreateUpdateDto tariffDto)
        {
            var tariff = await _context.Tariffs.FindAsync(tariffId);
            if (tariff == null) return null;

            tariff.Name = tariffDto.Name;
            // FIX: Explicitly convert DateTime to DateOnly
            tariff.EffectiveFrom = DateOnly.FromDateTime(tariffDto.EffectiveFrom);
            // FIX: Handle the nullable DateTime?
            tariff.EffectiveTo = tariffDto.EffectiveTo.HasValue
                ? DateOnly.FromDateTime(tariffDto.EffectiveTo.Value)
                : null;
            tariff.BaseRate = tariffDto.BaseRate;
            tariff.TaxRate = tariffDto.TaxRate;

            await _context.SaveChangesAsync();
            return tariff;
        }
        public async Task<bool> DeleteTariffAsync(int tariffId)
        {
            // Check if this tariff is in use by any meters
            var isTariffInUse = await _context.Meters.AnyAsync(m => m.TariffId == tariffId);
            if (isTariffInUse)
            {
                return false; // Cannot delete, is in use
            }

            // Check if tariff has slabs (we should delete them first or use CASCADE)
            var hasSlabs = await _context.TariffSlabs.AnyAsync(s => s.TariffId == tariffId);
            if (hasSlabs)
            {
                // For simplicity, we'll block deletion.
                // A better approach might be to delete slabs first.
                return false; // Cannot delete, has slabs
            }

            var tariff = await _context.Tariffs.FindAsync(tariffId);
            if (tariff == null) return false;

            _context.Tariffs.Remove(tariff);
            await _context.SaveChangesAsync();
            return true;
        }

        // --- TARIFF SLAB METHODS ---

        public async Task<IEnumerable<TariffSlab>> GetSlabsByTariffIdAsync(int tariffId)
        {
            return await _context.TariffSlabs.AsNoTracking()
                .Where(s => s.TariffId == tariffId)
                .OrderBy(s => s.FromKwh) // Slabs must be in order
                .ToListAsync();
        }

        public async Task<TariffSlab> CreateSlabAsync(TariffSlabCreateDto slabDto)
        {
            var slab = new TariffSlab
            {
                TariffId = slabDto.TariffId,
                FromKwh = slabDto.FromKwh,
                ToKwh = slabDto.ToKwh,
                RatePerKwh = slabDto.RatePerKwh
            };

            await _context.TariffSlabs.AddAsync(slab);
            await _context.SaveChangesAsync();
            return slab;
        }

        public async Task<bool> DeleteSlabAsync(int slabId)
        {
            var slab = await _context.TariffSlabs.FindAsync(slabId);
            if (slab == null) return false;

            _context.TariffSlabs.Remove(slab);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SlabExistsAsync(int slabId)
        {
            return await _context.TariffSlabs.AnyAsync(s => s.TariffSlabId == slabId);
        }
    }
}
