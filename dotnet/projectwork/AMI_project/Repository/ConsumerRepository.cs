using AMI_project.Dtos;
using AMI_project.Models;
using Microsoft.EntityFrameworkCore;

namespace AMI_project.Repository
{
    public class ConsumerRepository : IConsumerRepository
    {
        private readonly AmidbContext _context;

        public ConsumerRepository(AmidbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Consumer>, int)> GetConsumersAsync(ConsumerQueryParameters queryParams)
        {
            var query = _context.Consumers.AsNoTracking();

            // Apply Filtering
            if (!string.IsNullOrEmpty(queryParams.Name))
            {
                query = query.Where(c => c.Name.Contains(queryParams.Name));
            }
            if (!string.IsNullOrEmpty(queryParams.Phone))
            {
                query = query.Where(c => c.Phone.Contains(queryParams.Phone));
            }
            if (!string.IsNullOrEmpty(queryParams.Email))
            {
                query = query.Where(c => c.Email.Contains(queryParams.Email));
            }
            if (!string.IsNullOrEmpty(queryParams.Status))
            {
                query = query.Where(c => c.Status == queryParams.Status);
            }

            // Get Total Count
            var totalCount = await query.CountAsync();

            // Apply Sorting
            if (queryParams.SortOrder?.ToLower() == "desc")
            {
                query = query.OrderByDescending(c => c.Name);
            }
            else
            {
                query = query.OrderBy(c => c.Name);
            }

            // Apply Paging
            query = query.Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
                         .Take(queryParams.PageSize);

            var consumers = await query.ToListAsync();
            return (consumers, totalCount);
        }

        public async Task<Consumer> GetConsumerByIdAsync(long consumerId)
        {
            return await _context.Consumers.AsNoTracking()
                .FirstOrDefaultAsync(c => c.ConsumerId == consumerId);
        }

        public async Task<Consumer> CreateConsumerAsync(ConsumerCreateDto consumerDto)
        {
            var consumer = new Consumer
            {
                Name = consumerDto.Name,
                Address = consumerDto.Address,
                Phone = consumerDto.Phone,
                Email = consumerDto.Email,
                Status = consumerDto.Status,
                CreatedBy = consumerDto.CreatedBy,
                CreatedAt = DateTime.UtcNow // Set creation time
            };

            await _context.Consumers.AddAsync(consumer);
            await _context.SaveChangesAsync();
            return consumer;
        }

        public async Task<Consumer> UpdateConsumerAsync(long consumerId, ConsumerUpdateDto consumerDto)
        {
            var consumer = await _context.Consumers.FindAsync(consumerId);
            if (consumer == null) return null;

            consumer.Name = consumerDto.Name;
            consumer.Address = consumerDto.Address;
            consumer.Phone = consumerDto.Phone;
            consumer.Email = consumerDto.Email;
            consumer.Status = consumerDto.Status;

            consumer.UpdatedAt = DateTime.UtcNow;
            consumer.UpdatedBy = consumerDto.UpdatedBy;

            await _context.SaveChangesAsync();
            return consumer;
        }

        public async Task<bool> DeleteConsumerAsync(long consumerId)
        {
            // First, check if this consumer is linked to any meters.
            // You cannot delete a consumer that has meters assigned.
            var hasMeters = await _context.Meters.AnyAsync(m => m.ConsumerId == consumerId);
            if (hasMeters)
            {
                // Cannot delete. We should throw a specific exception here,
                // but for now, we just return false.
                return false;
            }

            var consumer = await _context.Consumers.FindAsync(consumerId);
            if (consumer == null) return false;

            _context.Consumers.Remove(consumer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ConsumerExistsAsync(long consumerId)
        {
            return await _context.Consumers.AnyAsync(c => c.ConsumerId == consumerId);
        }
    }
}