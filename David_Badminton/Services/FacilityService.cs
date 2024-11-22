using David_Badminton.IServices;
using David_Badminton.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace David_Badminton.Services
{
    public class FacilityService : IFacilityService
    {
        private readonly DavidBadmintonContext _context;

        public FacilityService(DavidBadmintonContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Facility>> GetAllFacilitiesAsync()
        {
            return await _context.Facilitys.ToListAsync();
        }

        public async Task<Facility> GetFacilityByIdAsync(int id)
        {
            return await _context.Facilitys.FindAsync(id);
        }

        public async Task AddFacilityAsync(Facility facility)
        {
            await _context.Facilitys.AddAsync(facility);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFacilityAsync(Facility facility)
        {
            _context.Facilitys.Update(facility);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFacilityAsync(int id)
        {
            var facility = await _context.Facilitys.FindAsync(id);
            if (facility != null)
            {
                _context.Facilitys.Remove(facility);
                await _context.SaveChangesAsync();
            }
        }
    }
}
