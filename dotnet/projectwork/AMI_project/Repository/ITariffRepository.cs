using AMI_project.Dtos;
using AMI_project.Models;
//This repository will handle logic for both `Tariff` and `TariffSlab` tables.
namespace AMI_project.Repository
{
    public interface ITariffRepository
    {
        // Tariff Methods
        Task<IEnumerable<Tariff>> GetTariffsAsync();
        Task<Tariff> GetTariffByIdAsync(int tariffId);
        Task<Tariff> CreateTariffAsync(TariffCreateUpdateDto tariffDto);
        Task<Tariff> UpdateTariffAsync(int tariffId, TariffCreateUpdateDto tariffDto);
        Task<bool> DeleteTariffAsync(int tariffId);

        // Tariff Slab Methods
        Task<IEnumerable<TariffSlab>> GetSlabsByTariffIdAsync(int tariffId);
        Task<TariffSlab> CreateSlabAsync(TariffSlabCreateDto slabDto);
        Task<bool> DeleteSlabAsync(int slabId);
        Task<bool> SlabExistsAsync(int slabId);
    }
}