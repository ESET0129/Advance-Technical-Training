using AMI_project.Dtos;
using AMI_project.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AMI_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class ConsumersController : ControllerBase
    {
        private readonly IConsumerRepository _consumerRepository;

        public ConsumersController(IConsumerRepository consumerRepository)
        {
            _consumerRepository = consumerRepository;
        }
        private readonly ILogger _logger;

        public ConsumersController(ILogger logger)
        {
            _logger = logger;
        }




        // GET: api/consumers
        [HttpGet]
        public async Task<IActionResult> GetConsumers([FromQuery] ConsumerQueryParameters queryParams)
        {
            _logger.LogInformation("Executing ConsumersController.Get Method");



            var (consumers, totalCount) = await _consumerRepository.GetConsumersAsync(queryParams);

            var paginationMetadata = new
            {
                totalCount,
                queryParams.PageSize,
                queryParams.PageNumber,
                totalPages = (int)Math.Ceiling(totalCount / (double)queryParams.PageSize)
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(consumers);
        }

        // GET: api/consumers/1001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsumer(long id)
        {
            var consumer = await _consumerRepository.GetConsumerByIdAsync(id);
            if (consumer == null)
            {
                return NotFound();
            }
            return Ok(consumer);
        }

        // POST: api/consumers
        [HttpPost]
        public async Task<IActionResult> CreateConsumer([FromBody] ConsumerCreateDto consumerDto)
        {
            // You might want to add a check for existing email/phone
            var createdConsumer = await _consumerRepository.CreateConsumerAsync(consumerDto);
            return CreatedAtAction(nameof(GetConsumer), new { id = createdConsumer.ConsumerId }, createdConsumer);
        }

        // PUT: api/consumers/1001
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsumer(long id, [FromBody] ConsumerUpdateDto consumerDto)
        {
            if (!await _consumerRepository.ConsumerExistsAsync(id))
            {
                return NotFound();
            }

            var updatedConsumer = await _consumerRepository.UpdateConsumerAsync(id, consumerDto);
            return Ok(updatedConsumer);
        }

        // DELETE: api/consumers/1001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsumer(long id)
        {
            if (!await _consumerRepository.ConsumerExistsAsync(id))
            {
                return NotFound();
            }

            var deleteResult = await _consumerRepository.DeleteConsumerAsync(id);
            if (!deleteResult)
            {
                // This means the consumer has meters and cannot be deleted
                return Conflict(new { message = "Cannot delete consumer. Consumer is associated with one or more meters." });
            }

            return NoContent(); // Success
        }
    }
}