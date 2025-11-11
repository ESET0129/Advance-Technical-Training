using AMI_project.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMI_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class UploadController : ControllerBase
    {
        private readonly IMeterUploadService _meterUploadService;

        public UploadController(IMeterUploadService meterUploadService)
        {
            _meterUploadService = meterUploadService;
        }

        // POST: api/upload/meters
        [HttpPost("meters")]
        public async Task<IActionResult> UploadMeters(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            if (Path.GetExtension(file.FileName).ToLower() != ".csv")
            {
                return BadRequest("Invalid file type. Only .csv files are allowed.");
            }

            var result = await _meterUploadService.ProcessMeterUploadAsync(file);

            if (result.FailedRows > 0 && result.SuccessfullyImported == 0)
            {
                // Total failure
                return BadRequest(result);
            }

            if (result.FailedRows > 0)
            {
                // Partial success
                return Ok(result);
            }

            // Total success
            return Ok(result);
        }
    }
}
