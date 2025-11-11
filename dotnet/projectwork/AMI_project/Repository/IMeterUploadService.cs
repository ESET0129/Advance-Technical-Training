using AMI_project.Dtos;

namespace AMI_project.Repository
{
    public interface IMeterUploadService
    {
        Task<UploadResultDto> ProcessMeterUploadAsync(IFormFile file);
    }
}
