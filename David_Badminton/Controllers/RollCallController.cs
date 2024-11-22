using David_Badminton.IServices;
using David_Badminton.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace David_Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RollCallController : ControllerBase
    {
        private readonly IRollCallService _rollCallService;

        public RollCallController(IRollCallService rollCallService)
        {
            _rollCallService = rollCallService;
        }

        private string GetCurrentCoachId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier); 
        }

        //------------------------------------
        [Authorize(Roles = "Admin,HLV")]
        [HttpGet("date/{date}")]
        public async Task<ActionResult<List<RollCall>>> GetAttendanceHistoryByDate(String date)
        {
            var attendances = await _rollCallService.GetRollCallHistoryByDateAsync(date);
            return Ok(attendances);
        }
        //------------------------------------

        [Authorize(Roles ="Admin,HLV")]
        [HttpPost]
        public async Task<ActionResult> MarkAttendance(int studentId, bool isPresent, bool status)
        {
            var coachId = int.Parse(GetCurrentCoachId());
            var userCreated = User.Identity.Name;

            await _rollCallService.MarkRollCallAsync(studentId, coachId, isPresent, status, userCreated);
            return Ok(new { message = "Attendance marked successfully" });
        }

        //------------------------------------
        [Authorize(Roles = "Admin,HLV,HocVien")]
        [HttpGet("student/{studentId}/attendance-history")]
        public async Task<ActionResult<List<RollCall>>> GetAttendanceHistory(int studentId)
        {
            var attendanceHistory = await _rollCallService.GetRollCallHistoryByStudentIdAsync(studentId);
            return Ok(attendanceHistory);
        }

        [Authorize(Roles = "Admin,HLV")]
        [HttpGet("student/{studentId}/total-attendance")]
        public async Task<ActionResult<int>> GetTotalAttendance(int studentId)
        {
            var totalAttendance = await _rollCallService.GetRollCallHistoryByStudentIdAsync(studentId);
            return Ok(totalAttendance);
        }
    }
}
