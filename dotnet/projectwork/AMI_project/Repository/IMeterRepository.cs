//Meter Repository Interface:AMI_project.Api/Repository/IMeterRepository.cs
using AMI_project.Dtos;
//using AMI_project.Dtos;
using AMI_project.Models;
//using System.Diagnostics.Metrics;

namespace AMI_project.Repository
{
    public interface IMeterRepository
    {
        Task<(IEnumerable<Meter>, int)> GetMetersAsync(MeterQueryParameters queryParameters);
        Task<Meter> GetMeterByIdAsync(string meterSerialNo);
        Task<Meter> CreateMeterAsync(MeterCreateUpdateDto meterDto);
        Task<Meter> UpdateMeterAsync(string meterSerialNo, MeterCreateUpdateDto meterDto);
        Task<bool> DeleteMeterAsync(string meterSerialNo);
        Task<bool> MeterExistsAsync(string meterSerialNo);
        //Task<MeterDetailsDto> GetMeterDetailsAsync(string meterSerialNo);
        
    }
}
