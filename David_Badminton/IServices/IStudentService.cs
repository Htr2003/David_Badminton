using David_Badminton.Models;
using David_Badminton.ViewModels;

namespace David_Badminton.IServices
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
        //---------------------------------
        Task<IEnumerable<Student>> GetStudentsByCoachIdAsync(int coachId);
        Task<Student?> GetStudentByInputAsync(String input);
        Task<IEnumerable<Student>> GetStudentByColumnAsync(String column, String value);
        //---------------------------------

        Task<IEnumerable<MonthlyRevenueDto>> GetMonthlyRevenue(int year);
    }
}
