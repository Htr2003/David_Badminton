using David_Badminton.IServices;
using David_Badminton.Models;
using Microsoft.EntityFrameworkCore;

namespace David_Badminton.Services
{
    public class RollCallCoachService : IRollCallCoachService
    {
        private readonly DavidBadmintonContext _context;

        public RollCallCoachService(DavidBadmintonContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RollCallCoach>> GetAllRollCallsAsync()
        {
            return await _context.RollCallCoachs.ToListAsync();
        }

        public async Task<RollCallCoach> GetRollCallByIdAsync(int id)
        {
            return await _context.RollCallCoachs.FindAsync(id);
        }

        //-----------------------------
        public async Task<IEnumerable<RollCallCoach>> GetRollCallByCoachsIdAsync(int coachsId)
        {
            return await _context.RollCallCoachs
                .Where(rc => rc.CoachId == coachsId)
                .OrderByDescending(rc => rc.DateCreated)
                .ToListAsync();
        }
        //-----------------------------

        public async Task AddRollCallAsync(RollCallCoach rollCallCoach)
        {
            await _context.RollCallCoachs.AddAsync(rollCallCoach);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRollCallAsync(RollCallCoach rollCallCoach)
        {
            _context.RollCallCoachs.Update(rollCallCoach);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRollCallAsync(int id)
        {
            var rollCall = await _context.RollCallCoachs.FindAsync(id);
            if (rollCall != null)
            {
                _context.RollCallCoachs.Remove(rollCall);
                await _context.SaveChangesAsync();
            }
        }
    }
}
