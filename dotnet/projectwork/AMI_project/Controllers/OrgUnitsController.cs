using AMI_project.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMI_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class OrgUnitsController : ControllerBase
    {
        private readonly IOrgUnitRepository _orgUnitRepo;

        public OrgUnitsController(IOrgUnitRepository orgUnitRepo)
        {
            _orgUnitRepo = orgUnitRepo;
        }

        // GET: api/orgunits/type/Zone
        // This is for the first dropdown, to get all Zones
        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetOrgUnitsByType(string type)
        {
            var orgUnits = await _orgUnitRepo.GetOrgUnitsByTypeAsync(type);
            return Ok(orgUnits);
        }

        // GET: api/orgunits/children/1
        // This is for all cascading dropdowns
        // e.g., get children of Zone 1 (Substations)
        // e.g., get children of Substation 2 (Feeders)
        [HttpGet("children/{parentId}")]
        public async Task<IActionResult> GetOrgUnitsByParent(int parentId)
        {
            var orgUnits = await _orgUnitRepo.GetOrgUnitsByParentIdAsync(parentId);
            return Ok(orgUnits);
        }


        // GET: api/orgunits/dtrs
        [HttpGet("dtrs")]
        public async Task<IActionResult> GetAllDtrs()
        {
            var dtrs = await _orgUnitRepo.GetAllDtrsAsync();
            return Ok(dtrs);
        }

        // GET: api/orgunits/hierarchy/7
        [HttpGet("hierarchy/{id}")]
        public async Task<IActionResult> GetHierarchy(int id)
        {
            var hierarchy = await _orgUnitRepo.GetHierarchyAsync(id);
            return Ok(hierarchy);
        }


    }
}