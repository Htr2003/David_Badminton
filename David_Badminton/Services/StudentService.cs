using David_Badminton.IServices;
using David_Badminton.Models;
using David_Badminton.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace David_Badminton.Services
{
    public class StudentService : IStudentService
    {
        private readonly DavidBadmintonContext _context;

        public StudentService(DavidBadmintonContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task AddStudentAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }
        //------------------------------
        public async Task<IEnumerable<Student>> GetStudentsByCoachIdAsync(int coachId)
        {
            return await _context.Students
                .Where(s => s.CoachId == coachId)
                .ToListAsync();
        }
        public async Task<Student?> GetStudentByInputAsync(string input)
        {
            return await _context.Students
                .FirstOrDefaultAsync(student => student.StudentCode == input);
        }
        public async Task<IEnumerable<Student>> GetStudentByColumnAsync(string column, string value)
        {
            return await _context.Students
                .Where(c => EF.Property<string>(c, column).Equals(value))
                .ToListAsync();
        }
        //------------------------------

        public async Task<IEnumerable<MonthlyRevenueDto>> GetMonthlyRevenue(int year)
        {
            var revenueData = await _context.Students
                .Where(student => student.StudyStart.Year == year)
                .GroupBy(student => new { student.StudyStart.Year, student.StudyStart.Month })
                .Select(group => new MonthlyRevenueDto
                {
                    Year = group.Key.Year,
                    Month = group.Key.Month,
                    TotalRevenue = group.Sum(student => student.Tuitions)
                })
                .OrderBy(r => r.Year).ThenBy(r => r.Month)
                .ToListAsync();

            return revenueData;
        }
    }
}
