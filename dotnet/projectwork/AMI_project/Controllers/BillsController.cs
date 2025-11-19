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
    public class BillsController : ControllerBase
    {
        private readonly IBillingRepository _billingRepo;
        private readonly IBillingService _billingService;

        public BillsController(IBillingRepository billingRepo, IBillingService billingService)
        {
            _billingRepo = billingRepo;
            _billingService = billingService;
        }

        // GET: api/bills
        [HttpGet]
        public async Task<IActionResult> GetBills([FromQuery] BillQueryParameters queryParams)
        {
            var (bills, totalCount) = await _billingRepo.GetBillsAsync(queryParams);

            var paginationMetadata = new
            {
                totalCount,
                queryParams.PageSize,
                queryParams.PageNumber,
                totalPages = (int)Math.Ceiling(totalCount / (double)queryParams.PageSize)
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            return Ok(bills);
        }

        // PUT: api/bills/1/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateBillStatus(long id, [FromBody] object statusDto)
        {
            // Simple logic to extract "status" from the anonymous object
            string status;
            try
            {
                var json = JsonDocument.Parse(statusDto.ToString());
                status = json.RootElement.GetProperty("status").GetString();
            }
            catch
            {
                return BadRequest(new { message = "Invalid JSON format. Expected {'status':'Paid'}" });
            }

            if (status != "Paid")
            {
                return BadRequest(new { message = "Invalid status update. Only 'Paid' is allowed." });
            }

            var bill = await _billingRepo.UpdateBillStatusAsync(id, status);
            if (bill == null)
            {
                return NotFound();
            }

            return Ok(bill);
        }

        // POST: api/bills/generate
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateBills([FromBody] GenerateBillsRequestDto request)
        {
            var result = await _billingService.GenerateMonthlyBillsAsync(request.BillingMonth);

            if (result.Errors.Any())
            {
                return StatusCode(500, result);
            }

            return Ok(result);
        }
    }
}