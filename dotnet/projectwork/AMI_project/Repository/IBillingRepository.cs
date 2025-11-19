using AMI_project.Dtos;
using AMI_project.Models;

namespace AMI_project.Repository
{
    public interface IBillingRepository
    {
        Task<(IEnumerable<BillViewDto>, int)> GetBillsAsync(BillQueryParameters queryParams);
        Task<MonthlyBill> UpdateBillStatusAsync(long billId, string status);
    }
}