using David_Badminton.Models;

namespace David_Badminton.IServices
{
    public interface ILearningScheduleService
    {
        Task<List<Time>> GetAllTimesAsync();
        Task<Time> GetTimeByIdAsync(int id);
        Task CreateTimeAsync(Time time);
        Task UpdateTimeAsync(Time time);
        Task DeleteTimeAsync(int id);
    }
}
