using AMI_project.Models;

namespace AMI_project.Repository
{
    public interface IOrgUnitRepository
    {
        // This will get the top-level items, e.g., all "Zones"
        Task<IEnumerable<OrgUnit>> GetOrgUnitsByTypeAsync(string type);

        // This will get the children of a selected item,
        // e.g., all "Substations" where ParentId = 1
        Task<IEnumerable<OrgUnit>> GetOrgUnitsByParentIdAsync(int parentId);
        Task<IEnumerable<OrgUnit>> GetAllDtrsAsync();
        Task<List<OrgUnit>> GetHierarchyAsync(int id);
    }
}
