using David_Badminton.IServices;
using David_Badminton.Models;
using David_Badminton.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace David_Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearningProcessController : ControllerBase
    {
        private readonly ILearningProcessService _scheduleService;
        private readonly IStudentService _studentService;

        public LearningProcessController(ILearningProcessService scheduleService, IStudentService studentService)
        {
            _scheduleService = scheduleService;
            _studentService = studentService;
        }

        [Authorize(Roles = "Admin,HLV")]
        [HttpGet]
        public async Task<IActionResult> GetAllSchedules()
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole == "HLV") 
            {
                var coachIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Get the coach's ID
                if (!int.TryParse(coachIdString, out int coachId)) // Convert to int
                {
                    return BadRequest("Invalid coach ID."); 
                }
                var students = await _studentService.GetStudentsByCoachIdAsync(coachId); 

                var studentIds = students.Select(s => s.StudentId).ToList();
                var schedules = await _scheduleService.GetSchedulesByStudentIdsAsync(studentIds); 

                return Ok(schedules); // Return the learning processes for the coach's students
            }

            // If the user is an admin, return all schedules
            var allSchedules = await _scheduleService.GetAllSchedulesAsync();
            return Ok(allSchedules);
        }

        [Authorize(Roles = "Admin,HLV")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetScheduleById(int id)
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return Ok(schedule);
        }
        //------------------------------------------------
        [Authorize(Roles = "Admin,HLV,HocVien")]
        [HttpGet("studentId={studentId}&date={dateCreated}")]
        public async Task<IActionResult> GetByStudentIdAndDateCreated(int studentId, string dateCreated)
        {
            var schedule = await _scheduleService.GetByStudentIdAndDateCreatedAsync(studentId, dateCreated);
            if (schedule == null)
            {
                return NotFound();
            }
            return Ok(schedule);
        }
        [Authorize(Roles = "Admin,HLV,HocVien")]
        [HttpGet("studentId/{studentId}")]
        public async Task<IActionResult> GetByStudentId(int studentId)
        {
            var schedule = await _scheduleService.GetByStudentIdWithPublishAsync(studentId);
            if (schedule == null)
            {
                return NotFound();
            }
            return Ok(schedule);
        }
        //------------------------------------------------

        [Authorize(Roles = "Admin,HLV")]
        [HttpPost]
        public async Task<IActionResult> AddSchedule([FromBody] LearningProcess schedule)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole == "HLV") // If the user is a coach
            {
                var coachIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Get the coach's ID as string
                if (!int.TryParse(coachIdString, out int coachId)) // Convert to int
                {
                    return BadRequest("Invalid coach ID."); // Handle conversion failure
                }

                var student = await _studentService.GetStudentByIdAsync(schedule.StudentId); // Get the student by ID

                // Check if the student belongs to the logged-in coach
                if (student == null || student.CoachId != coachId)
                {
                    return Forbid(); // If not, forbid the action
                }
            }

            await _scheduleService.AddScheduleAsync(schedule); // Add the schedule
            return CreatedAtAction(nameof(GetScheduleById), new { id = schedule.LearningProcessId }, schedule);
        }

        [Authorize(Roles = "Admin,HLV")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, [FromBody] LearningProcess schedule)
        {
            if (id != schedule.LearningProcessId)
            {
                return BadRequest();
            }
            await _scheduleService.UpdateScheduleAsync(schedule);
            return NoContent();
        }

        [Authorize(Roles = "Admin,HLV")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            await _scheduleService.DeleteScheduleAsync(id);
            return NoContent();
        }

    }
}
