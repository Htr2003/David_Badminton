using David_Badminton.Models;

namespace David_Badminton.IServices
{
    public interface IUserService
    {
        Task<string> LoginAsync(string phone, string password);
        Task<string> GenerateJwtToken(Coach coach);
        //------------------------------------
        Task<string> GenerateJwtTokenStudent(Student student);
        Task<string> LoginStudentAsync(string studentCode, string password);
        //------------------------------------
    }
}
