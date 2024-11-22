using David_Badminton.Models;

namespace David_Badminton.IServices
{
    public interface ILearningProcessService
    {
        Task<IEnumerable<LearningProcess>> GetAllSchedulesAsync();
        Task<LearningProcess> GetScheduleByIdAsync(int id);
        Task AddScheduleAsync(LearningProcess schedule);
        Task UpdateScheduleAsync(LearningProcess schedule);
        Task DeleteScheduleAsync(int id);
        Task<IEnumerable<LearningProcess>> GetSchedulesByStudentIdsAsync(IEnumerable<int> studentIds);
        //--------------------------------------
        Task<IEnumerable<LearningProcess>> GetByStudentIdAndDateCreatedAsync(int studentId, string dateCreated);
        Task<IEnumerable<LearningProcess>> GetByStudentIdWithPublishAsync(int studentId);
        //--------------------------------------
    }
}
