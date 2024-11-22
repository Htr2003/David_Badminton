using David_Badminton.IServices;
using David_Badminton.Models;
using David_Badminton.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace David_Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [Authorize(Roles = "Admin,HLV")]
        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [Authorize(Roles = "Admin,HLV")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            return student == null ? NotFound() : Ok(student);
        }

        //-----------------------------------
        [Authorize(Roles = "Admin,HLV")]
        [HttpGet("coachId/{id}")]
        public async Task<ActionResult<Student>> GetStudentByCoachId(int id)
        {
            var student = await _studentService.GetStudentsByCoachIdAsync(id);
            return student == null ? NotFound() : Ok(student);
        }
        [Authorize(Roles = "HocVien")]
        [HttpGet("input/{input}")]
        public async Task<ActionResult<Student>> GetStudentByInput(String input)
        {
            var student = await _studentService.GetStudentByInputAsync(input);
            return student == null ? NotFound() : Ok(student);
        }
        [Authorize(Roles = "Admin,HLV,HocVien")]
        [HttpGet("search/{column}/{value}")]
        public async Task<ActionResult<IEnumerable<Coach>>> GetStudentsByColumn(string column, string value)
        {
            var coaches = await _studentService.GetStudentByColumnAsync(column, value);
            return Ok(coaches);
        }
        //-----------------------------------

        [Authorize(Roles = "Admin,HLV")]
        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            if (User.IsInRole("HLV"))
            {
                var coachId = GetCurrentCoachId(); // Retrieve the coach's ID from the JWT token
                student.CoachId = coachId; 
            }
            else if (User.IsInRole("Admin"))
            {
                if (student.CoachId == 0)
                {
                    return BadRequest("CoachId must be provided when adding a student as an Admin.");
                }
            }

            await _studentService.AddStudentAsync(student);
            return CreatedAtAction(nameof(GetStudentById), new { id = student.StudentId }, student);
        }

        [Authorize(Roles = "Admin,HLV")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, [FromBody] Student student)
        {
            if (id != student.StudentId)
                return BadRequest();

            await _studentService.UpdateStudentAsync(student);
            return NoContent();
        }

        [Authorize(Roles = "Admin,HLV")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("monthly-revenue")]
        public async Task<IActionResult> GetMonthlyRevenue(int year, int month)
        {
            var revenueData = await _studentService.GetMonthlyRevenue(year);
            return Ok(revenueData);
        }

        private int GetCurrentCoachId()
        {
            var coachIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(coachIdClaim);
        }

        //-------------------------------------
        // Update coacch by id and another field key
        [Authorize(Roles = "Admin,HocVien")]
        [HttpPut("studentId/{id}")]
        public async Task<IActionResult> UpdateStudentById(int id, [FromBody] Dictionary<string, object> updateFields)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            foreach (var field in updateFields)
            {
                var property = student.GetType().GetProperty(field.Key);
                if (property != null && property.CanWrite && field.Key != "StudentId")
                {
                    try
                    {
                        var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        object? value = ConvertJsonValue(field.Value, propertyType);
                        property.SetValue(student, value);
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

            await _studentService.UpdateStudentAsync(student);
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
