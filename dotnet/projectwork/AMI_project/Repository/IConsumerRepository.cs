using AMI_project.Dtos;
using AMI_project.Models;

namespace AMI_project.Repository
{
    public interface IConsumerRepository
    {
        Task<(IEnumerable<Consumer>, int)> GetConsumersAsync(ConsumerQueryParameters queryParams);
        Task<Consumer> GetConsumerByIdAsync(long consumerId);
        Task<Consumer> CreateConsumerAsync(ConsumerCreateDto consumerDto);
        Task<Consumer> UpdateConsumerAsync(long consumerId, ConsumerUpdateDto consumerDto);
        Task<bool> DeleteConsumerAsync(long consumerId);
        Task<bool> ConsumerExistsAsync(long consumerId);
    }
}
