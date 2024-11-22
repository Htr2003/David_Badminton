using David_Badminton.IServices;
using David_Badminton.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace David_Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearningScheduleController : ControllerBase
    {
        private readonly ILearningScheduleService _timeService;

        public LearningScheduleController(ILearningScheduleService timeService)
        {
            _timeService = timeService;
        }
        
        //------------------------------
        [Authorize(Roles = "Admin,HLV")]
        [HttpGet]
        public async Task<IActionResult> GetAllTimes()
        {
            var times = await _timeService.GetAllTimesAsync();
            return Ok(times);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimeById(int id)
        {
            var time = await _timeService.GetTimeByIdAsync(id);
            if (time == null)
            {
                return NotFound();
            }
            return Ok(time);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateTime([FromBody] Time time)
        {
            if (time == null)
            {
                return BadRequest();
            }

            await _timeService.CreateTimeAsync(time);
            return CreatedAtAction(nameof(GetTimeById), new { id = time.TimeId }, time);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTime(int id, [FromBody] Time time)
        {
            if (id != time.TimeId)
            {
                return BadRequest();
            }

            await _timeService.UpdateTimeAsync(time);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTime(int id)
        {
            await _timeService.DeleteTimeAsync(id);
            return NoContent();
        }
    }
}
