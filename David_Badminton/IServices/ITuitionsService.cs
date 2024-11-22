using David_Badminton.Models;

namespace David_Badminton.IServices
{
    public interface ITuitionsService
    {
        Task<IEnumerable<Tuitions>> GetListWithStudentIdAsync(int id);
    }
}
