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
    public class RollCallCoachController : ControllerBase
    {
        private readonly IRollCallCoachService _rollCallService;

        public RollCallCoachController(IRollCallCoachService rollCallService)
        {
            _rollCallService = rollCallService;
        }

        [Authorize(Roles = "Admin,HLV")]
        [HttpGet]
        public async Task<IActionResult> GetAllRollCalls()
        {
            var rollCalls = await _rollCallService.GetAllRollCallsAsync();
            return Ok(rollCalls);
        }

        [Authorize(Roles = "Admin,HLV")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRollCallById(int id)
        {
            var rollCall = await _rollCallService.GetRollCallByIdAsync(id);
            if (rollCall == null)
            {
                return NotFound();
            }
            return Ok(rollCall);
        }

        //----------------------------------------
        [Authorize(Roles = "Admin,HLV")]
        [HttpGet("{coachsId}/getHistory")]
        public async Task<ActionResult<List<RollCallCoach>>> GetRollCallByCoachsId(int coachsId)
        {
            var rollCall = await _rollCallService.GetRollCallByCoachsIdAsync(coachsId);
            if (rollCall == null)
            {
                return NotFound();
            }
            return Ok(rollCall);
        }
        //----------------------------------------

        //----------------------------------------
        [Authorize(Roles = "Admin,HLV")]
        [HttpPost]
        public async Task<IActionResult> AddRollCall([FromBody] RollCallCoach rollCallCoach)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Logic để gán CoachId nếu cần dựa trên vai trò
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole == "HLV")
            {
                var coachIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(coachIdString, out int coachId))
                {
                    rollCallCoach.CoachId = coachId; // Gán CoachId từ token
                }
                else
                {
                    return BadRequest("Invalid Coach ID.");
                }
            }

            rollCallCoach.DateCreated = DateTime.UtcNow;
            rollCallCoach.UserCreated = User.Identity.Name; // Lấy tên người dùng tạo
            await _rollCallService.AddRollCallAsync(rollCallCoach);
            return CreatedAtAction(nameof(GetRollCallById), new { id = rollCallCoach.RollCallCoachId }, rollCallCoach);
        }

        [Authorize(Roles = "Admin,HLV")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRollCall(int id, [FromBody] RollCallCoach rollCallCoach)
        {
            if (id != rollCallCoach.RollCallCoachId)
            {
                return BadRequest();
            }

            rollCallCoach.DateUpdated = DateTime.UtcNow;
            rollCallCoach.UserUpdated = User.Identity.Name;
            await _rollCallService.UpdateRollCallAsync(rollCallCoach);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRollCall(int id)
        {
            await _rollCallService.DeleteRollCallAsync(id);
            return NoContent();
        }
    }
}
