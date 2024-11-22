using David_Badminton.IServices;
using David_Badminton.Models;
using Microsoft.EntityFrameworkCore;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace David_Badminton.Services
{
    public class LearningProcessService : ILearningProcessService
    {
        private readonly DavidBadmintonContext _context;

        public LearningProcessService(DavidBadmintonContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LearningProcess>> GetAllSchedulesAsync()
        {
            return await _context.LearningProcesses.ToListAsync();
        }

        public async Task<LearningProcess> GetScheduleByIdAsync(int id)
        {
            return await _context.LearningProcesses.FindAsync(id);
        }

        public async Task AddScheduleAsync(LearningProcess schedule)
        {
            await _context.LearningProcesses.AddAsync(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateScheduleAsync(LearningProcess schedule)
        {
            _context.LearningProcesses.Update(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteScheduleAsync(int id)
        {
            var schedule = await _context.LearningProcesses.FindAsync(id);
            if (schedule != null)
            {
                _context.LearningProcesses.Remove(schedule);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<LearningProcess>> GetSchedulesByStudentIdsAsync(IEnumerable<int> studentIds)
        {
            return await _context.LearningProcesses
                .Where(lp => studentIds.Contains(lp.StudentId))
                .ToListAsync();
        }
        //-----------------------------------------
        public async Task<IEnumerable<LearningProcess>> GetByStudentIdAndDateCreatedAsync(int studentId, string dateCreated)
        {
            DateTime formattedDate = DateTime.Parse(dateCreated);
            return await _context.LearningProcesses
                .Where(lp => lp.StudentId==studentId && lp.DateCreated.Date==formattedDate.Date)
                .ToListAsync();
        }
        public async Task<IEnumerable<LearningProcess>> GetByStudentIdWithPublishAsync(int studentId)
        {
            return await _context.LearningProcesses
                .Where(lp => lp.StudentId == studentId && lp.IsPublish==1)
                .ToListAsync();
        }
        //-----------------------------------------
    }
}
