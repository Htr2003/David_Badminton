using David_Badminton.IServices;
using David_Badminton.Models;
using Microsoft.EntityFrameworkCore;

namespace David_Badminton.Services
{
    public class LearningScheduleService : ILearningScheduleService
    {
        private readonly DavidBadmintonContext _context;

        public LearningScheduleService(DavidBadmintonContext context)
        {
            _context = context;
        }

        public async Task<List<Time>> GetAllTimesAsync()
        {
            return await _context.Times.ToListAsync();
        }

        public async Task<Time> GetTimeByIdAsync(int id)
        {
            return await _context.Times.FindAsync(id);
        }

        public async Task CreateTimeAsync(Time time)
        {
            _context.Times.Add(time);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTimeAsync(Time time)
        {
            _context.Entry(time).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTimeAsync(int id)
        {
            var time = await _context.Times.FindAsync(id);
            if (time != null)
            {
                _context.Times.Remove(time);
                await _context.SaveChangesAsync();
            }
        }
    }
}
