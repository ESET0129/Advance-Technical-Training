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
    public class MetersController : ControllerBase
    {
        private readonly IMeterRepository _meterRepository;

        public MetersController(IMeterRepository meterRepository)
        {
            _meterRepository = meterRepository;
        }

        // GET: api/meters?pageNumber=1&pageSize=10&serialNo=MTR-SN-1001&status=Active
        [HttpGet]
        public async Task<IActionResult> GetMeters([FromQuery] MeterQueryParameters queryParams)
        {
            var (meters, totalCount) = await _meterRepository.GetMetersAsync(queryParams);

            var paginationMetadata = new
            {
                totalCount,
                queryParams.PageSize,
                queryParams.PageNumber,
                totalPages = (int)Math.Ceiling(totalCount / (double)queryParams.PageSize)
            };

            // Add pagination data to the response header
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(meters);
        }

        // GET: api/meters/MTR-SN-1001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeter(string id)
        {
            var meter = await _meterRepository.GetMeterByIdAsync(id);
            if (meter == null)
            {
                return NotFound();
            }
            return Ok(meter);
        }

        // POST: api/meters
        [HttpPost]
        public async Task<IActionResult> CreateMeter([FromBody] MeterCreateUpdateDto meterDto)
        {
            if (meterDto.MeterSerialNo != null && await _meterRepository.MeterExistsAsync(meterDto.MeterSerialNo))
            {
                return Conflict(new { message = $"Meter with Serial No '{meterDto.MeterSerialNo}' already exists." });
            }

            var createdMeter = await _meterRepository.CreateMeterAsync(meterDto);

            // Return a 201 Created response with a link to the new resource
            return CreatedAtAction(nameof(GetMeter), new { id = createdMeter.MeterSerialNo }, createdMeter);
        }

        // PUT: api/meters/MTR-SN-1001
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeter(string id, [FromBody] MeterCreateUpdateDto meterDto)
        {
            if (id != meterDto.MeterSerialNo)
            {
                return BadRequest("Meter Serial No in URL does not match body.");
            }

            if (!await _meterRepository.MeterExistsAsync(id))
            {
                return NotFound();
            }

            var updatedMeter = await _meterRepository.UpdateMeterAsync(id, meterDto);
            return Ok(updatedMeter);
        }

        // DELETE: api/meters/MTR-SN-1001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeter(string id)
        {
            if (!await _meterRepository.MeterExistsAsync(id))
            {
                return NotFound();
            }

            await _meterRepository.DeleteMeterAsync(id);
            return NoContent(); // 204 No Content is the standard response for a successful delete
        }

        
    }
}
