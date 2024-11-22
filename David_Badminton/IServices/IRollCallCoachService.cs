using David_Badminton.Models;

namespace David_Badminton.IServices
{
    public interface IRollCallCoachService
    {
        Task<IEnumerable<RollCallCoach>> GetAllRollCallsAsync();
        Task<RollCallCoach> GetRollCallByIdAsync(int id);

        //-----------------------------------
        Task<IEnumerable<RollCallCoach>> GetRollCallByCoachsIdAsync(int id);
        //-----------------------------------

        Task AddRollCallAsync(RollCallCoach rollCallCoach);
        Task UpdateRollCallAsync(RollCallCoach rollCallCoach);
        Task DeleteRollCallAsync(int id);
    }
}
