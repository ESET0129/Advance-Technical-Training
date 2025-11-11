using AMI_project.Models;
using Microsoft.EntityFrameworkCore;

namespace AMI_project.Repository
{
    public class OrgUnitRepository : IOrgUnitRepository
    {
        private readonly AmidbContext _context;

        public OrgUnitRepository(AmidbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrgUnit>> GetOrgUnitsByTypeAsync(string type)
        {
            return await _context.OrgUnits.AsNoTracking()
                .Where(o => o.Type == type)
                .OrderBy(o => o.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrgUnit>> GetOrgUnitsByParentIdAsync(int parentId)
        {
            return await _context.OrgUnits.AsNoTracking()
                .Where(o => o.ParentId == parentId)
                .OrderBy(o => o.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrgUnit>> GetAllDtrsAsync()
        {
            return await _context.OrgUnits.AsNoTracking()
                .Where(o => o.Type == "DTR")
                .OrderBy(o => o.Name)
                .ToListAsync();
        }

        public async Task<List<OrgUnit>> GetHierarchyAsync(int id)
        {
            var hierarchy = new List<OrgUnit>();
            var current = await _context.OrgUnits.AsNoTracking().FirstOrDefaultAsync(o => o.OrgUnitId == id);

            // Keep walking up the tree until we hit the top (null ParentId)
            while (current != null)
            {
                hierarchy.Add(current);
                current = await _context.OrgUnits.AsNoTracking().FirstOrDefaultAsync(o => o.OrgUnitId == current.ParentId);
            }

            hierarchy.Reverse(); // Puts the Zone at the start [Zone, Substation, Feeder, DTR]
            return hierarchy;
        }


    }
}
