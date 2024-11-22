using David_Badminton.IServices;
using David_Badminton.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace David_Badminton.Services
{
    public class CoachService : ICoachService
    {
        private readonly DavidBadmintonContext _context;

        public CoachService(DavidBadmintonContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Coach>> GetAllCoachesAsync()
        {
            return await _context.Coachs.ToListAsync();
        }

        //-----------------------------
        public async Task<Coach?> GetCoachByPhoneAsync(String phone)
        {
            return await _context.Coachs.FirstOrDefaultAsync(rc => rc.Phone == phone);
        }
        public async Task<IEnumerable<Coach>> GetCoachByColumnAsync(string column, string value)
        {
            return await _context.Coachs
                .Where(c => EF.Property<string>(c, column).Equals(value))
                .ToListAsync();
        }
        //-----------------------------

        public async Task<Coach> GetCoachByIdAsync(int id)
        {
            return await _context.Coachs.FindAsync(id);
        }

        public async Task AddCoachAsync(Coach coach)
        {
            await _context.Coachs.AddAsync(coach);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCoachAsync(Coach coach)
        {
            _context.Coachs.Update(coach);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCoachAsync(int id)
        {
            var coach = await _context.Coachs.FindAsync(id);
            if (coach != null)
            {
                _context.Coachs.Remove(coach);
                await _context.SaveChangesAsync();
            }
        }

    }
}
