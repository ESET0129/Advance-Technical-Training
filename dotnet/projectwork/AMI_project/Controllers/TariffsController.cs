using AMI_project.Dtos;
using AMI_project.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMI_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class TariffsController : ControllerBase
    {
        private readonly ITariffRepository _tariffRepo;

        public TariffsController(ITariffRepository tariffRepo)
        {
            _tariffRepo = tariffRepo;
        }

        // GET: api/tariffs
        // This gets all tariffs, perfect for populating dropdowns
        [HttpGet]
        public async Task<IActionResult> GetTariffs()
        {
            var tariffs = await _tariffRepo.GetTariffsAsync();
            return Ok(tariffs);
        }

        // GET: api/tariffs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTariff(int id)
        {
            var tariff = await _tariffRepo.GetTariffByIdAsync(id);
            if (tariff == null) return NotFound();
            return Ok(tariff);
        }

        // POST: api/tariffs
        [HttpPost]
        public async Task<IActionResult> CreateTariff([FromBody] TariffCreateUpdateDto tariffDto)
        {
            var createdTariff = await _tariffRepo.CreateTariffAsync(tariffDto);
            return CreatedAtAction(nameof(GetTariff), new { id = createdTariff.TariffId }, createdTariff);
        }

        // PUT: api/tariffs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTariff(int id, [FromBody] TariffCreateUpdateDto tariffDto)
        {
            var updatedTariff = await _tariffRepo.UpdateTariffAsync(id, tariffDto);
            if (updatedTariff == null) return NotFound();
            return Ok(updatedTariff);
        }

        // DELETE: api/tariffs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTariff(int id)
        {
            var result = await _tariffRepo.DeleteTariffAsync(id);
            if (!result)
            {
                return Conflict("Cannot delete tariff. It may be in use by meters or have existing slabs.");
            }
            return NoContent();
        }
    }
}
