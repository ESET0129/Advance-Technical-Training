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
    public class TariffSlabsController : ControllerBase
    {
        private readonly ITariffRepository _tariffRepo; // We reuse the same repository

        public TariffSlabsController(ITariffRepository tariffRepo)
        {
            _tariffRepo = tariffRepo;
        }

        // GET: api/tariffslabs/by-tariff/1
        // This is the key endpoint for your "Tariff Slab Data" UI
        [HttpGet("by-tariff/{tariffId}")]
        public async Task<IActionResult> GetSlabsForTariff(int tariffId)
        {
            var slabs = await _tariffRepo.GetSlabsByTariffIdAsync(tariffId);
            return Ok(slabs);
        }

        // POST: api/tariffslabs
        [HttpPost]
        public async Task<IActionResult> CreateSlab([FromBody] TariffSlabCreateDto slabDto)
        {
            var createdSlab = await _tariffRepo.CreateSlabAsync(slabDto);
            // We don't have a "GetSlabById" method, so we just return Ok
            return Ok(createdSlab);
        }

        // DELETE: api/tariffslabs/10
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlab(int id)
        {
            if (!await _tariffRepo.SlabExistsAsync(id))
            {
                return NotFound();
            }

            await _tariffRepo.DeleteSlabAsync(id);
            return NoContent();
        }
    }
}