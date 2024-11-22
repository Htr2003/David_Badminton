using David_Badminton.IServices;
using David_Badminton.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace David_Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachsController : ControllerBase
    {
        private readonly ICoachService _coachService;

        public CoachsController(ICoachService coachService)
        {
            _coachService = coachService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllCoaches()
        {
            var coaches = await _coachService.GetAllCoachesAsync();
            return Ok(coaches);
        }

        //------------------------------
        [Authorize(Roles = "Admin,HLV")]
        [HttpGet("phone/{phone}")]
        public async Task<ActionResult<Coach>> GetCoachesByPhone(String phone)
        {
            var coaches = await _coachService.GetCoachByPhoneAsync(phone);
            return Ok(coaches);
        }

        [Authorize(Roles = "Admin,HLV")]
        [HttpGet("search/{column}/{value}")]
        public async Task<ActionResult<IEnumerable<Coach>>> GetCoachesByColumn(string column, string value)
        {
            var coaches = await _coachService.GetCoachByColumnAsync(column, value);
            return Ok(coaches);
        }
        //------------------------------
        //------------------------------
        [Authorize(Roles = "Admin,HLV")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoachById(int id)
        {
            var coach = await _coachService.GetCoachByIdAsync(id);
            if (coach == null)
            {
                return NotFound();
            }
            return Ok(coach);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddCoach([FromBody] Coach coach)
        {
            await _coachService.AddCoachAsync(coach);
            return CreatedAtAction(nameof(GetCoachById), new { id = coach.CoachId }, coach);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoach(int id, [FromBody] Coach coach)
        {
            if (id != coach.CoachId)
            {
                return BadRequest();
            }
            await _coachService.UpdateCoachAsync(coach);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoach(int id)
        {
            await _coachService.DeleteCoachAsync(id);
            return NoContent();
        }

        //-------------------------------------
        // Update coacch by id and another field key
        [Authorize(Roles = "Admin,HLV")]
        [HttpPut("coachId/{id}")]
        public async Task<IActionResult> UpdateCoachById(int id, [FromBody] Dictionary<string, object> updateFields)
        {
            var coach = await _coachService.GetCoachByIdAsync(id);
            if (coach == null)
            {
                return NotFound();
            }

            foreach (var field in updateFields)
            {
                var property = coach.GetType().GetProperty(field.Key);
                if (property != null && property.CanWrite && field.Key != "CoachId")
                {
                    try
                    {
                        var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        object? value = ConvertJsonValue(field.Value, propertyType);
                        property.SetValue(coach, value);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest($"Failed to update {field.Key}: {ex.Message}");
                    }
                }
                else
                {
                    return BadRequest($"Field {field.Key} is invalid or cannot be updated.");
                }
            }

            await _coachService.UpdateCoachAsync(coach);
            return NoContent();
        }

        // Helper method for converting JsonElement to a specified type
        private object? ConvertJsonValue(object? value, Type targetType)
        {
            if (value is JsonElement jsonElement)
            {
                return targetType switch
                {
                    Type t when t == typeof(string) => jsonElement.GetString(),
                    Type t when t == typeof(DateTime) => jsonElement.GetDateTime(),
                    Type t when t == typeof(int) => jsonElement.GetInt32(),
                    Type t when t == typeof(decimal) => jsonElement.GetDecimal(),
                    Type t when t.IsEnum => Enum.Parse(targetType, jsonElement.GetString()),
                    _ => Convert.ChangeType(jsonElement.ToString(), targetType),
                };
            }
            return Convert.ChangeType(value, targetType);
        }
        //------------------------------------------------
    }
}
