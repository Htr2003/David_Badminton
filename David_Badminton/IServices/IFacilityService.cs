using David_Badminton.Models;

namespace David_Badminton.IServices
{
    public interface IFacilityService
    {
        Task<IEnumerable<Facility>> GetAllFacilitiesAsync();
        Task<Facility> GetFacilityByIdAsync(int id);
        Task AddFacilityAsync(Facility facility);
        Task UpdateFacilityAsync(Facility facility);
        Task DeleteFacilityAsync(int id);
    }
}
