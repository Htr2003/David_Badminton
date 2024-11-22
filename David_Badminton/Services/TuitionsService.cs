using David_Badminton.IServices;
using David_Badminton.Models;
using Microsoft.EntityFrameworkCore;

namespace David_Badminton.Services
{
    public class TuitionsService : ITuitionsService
    {
        private readonly DavidBadmintonContext _context;

        public TuitionsService(DavidBadmintonContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tuitions>> GetListWithStudentIdAsync(int id)
        {
            return await _context.Tuitions
                .Where(s => s.StudentId == id)
                .ToListAsync();
        }
    }
}
