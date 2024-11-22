using David_Badminton.Models;

namespace David_Badminton.IServices
{
    public interface ICoachService
    {
        Task<IEnumerable<Coach>> GetAllCoachesAsync();
        //------------------------
        Task<Coach?> GetCoachByPhoneAsync(String phone);
        Task<IEnumerable<Coach>> GetCoachByColumnAsync(String column, String value);
        //------------------------
        Task<Coach> GetCoachByIdAsync(int id);
        Task AddCoachAsync(Coach coach);
        Task UpdateCoachAsync(Coach coach);
        Task DeleteCoachAsync(int id);
    }
}
