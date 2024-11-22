using David_Badminton.IServices;
using David_Badminton.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace David_Badminton.Services
{
    public class RollCallService : IRollCallService
    {
        private readonly DavidBadmintonContext _context;

        public RollCallService(DavidBadmintonContext context)
        {
            _context = context;
        }

        // Điểm danh học viên
        public async Task MarkRollCallAsync(int studentId, int coachId, bool isPresent, bool status, string userCreated)
        {
            var rollCall = new RollCall
            {
                StudentId = studentId,
                CoachId = coachId,
                IsCheck = isPresent ? 1 : 0,// 1 là có mặt, 0 là vắng mặt
                //---------------------------------------------
                IsNull = isPresent ? 0 : 1,
                //---------------------------------------------
                StatusId = status ? 1 : 0, 
                UserCreated = userCreated,
                UserUpdated = userCreated,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now
            };

            _context.RollCalls.Add(rollCall);
            await _context.SaveChangesAsync();
        }

        // Xem lịch sử điểm danh
        public async Task<IEnumerable<RollCall>> GetRollCallHistoryByStudentIdAsync(int studentId)
        {
            return await _context.RollCalls
                .Include(rc => rc.Coach)
                .Where(rc => rc.StudentId == studentId)
                .OrderByDescending(rc => rc.DateCreated)
                .ToListAsync();
        }

        // Tổng số buổi điểm danh theo từng học viên
        public async Task<int> GetTotalRollCallByStudentIdAsync(int studentId)
        {
            return await _context.RollCalls
                .Where(rc => rc.StudentId == studentId && rc.IsCheck == 1)
                .CountAsync();
        }

        //-------------------------------------------
        public async Task<IEnumerable<RollCall>> GetRollCallHistoryByDateAsync(string date)
        {
            DateTime formattedDate = DateTime.Parse(date);

            return await _context.RollCalls
                .Where(rc => rc.DateUpdated.Date == formattedDate.Date)
                .OrderByDescending(rc => rc.DateUpdated)
                .ToListAsync();
        }
        //-------------------------------------------
    }
}
