using AMI_project.Dtos;

namespace AMI_project.Repository
{
    public interface IBillingService
    {
        Task<GenerateBillsResponseDto> GenerateMonthlyBillsAsync(string billingMonth);
    }
}
