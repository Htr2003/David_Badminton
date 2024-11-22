using David_Badminton.Models;

namespace David_Badminton.IServices
{
    public interface IRollCallService
    {
        Task MarkRollCallAsync(int studentId, int coachId, bool isPresent, bool status, string userCreated);
        //--------------------------------
        Task<IEnumerable<RollCall>> GetRollCallHistoryByDateAsync(String date);
        //--------------------------------
        Task<IEnumerable<RollCall>> GetRollCallHistoryByStudentIdAsync(int studentId);
        Task<int> GetTotalRollCallByStudentIdAsync(int studentId);
    }
}
