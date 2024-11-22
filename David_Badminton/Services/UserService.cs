using David_Badminton.IServices;
using David_Badminton.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace David_Badminton.Services
{
    public class UserService : IUserService
    {
        private readonly DavidBadmintonContext _context;
        private readonly IConfiguration _config;

        public UserService(DavidBadmintonContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<string> LoginAsync(string phone, string password)
        {
            var coach = await _context.Coachs.SingleOrDefaultAsync(c => c.Phone == phone);
            if (coach == null || !VerifyPassword(password, coach.Password))
            {
                throw new Exception("Invalid phone or password.");
            }

            return await GenerateJwtToken(coach);
        }

        //--------------------------------------------------
        public async Task<string> LoginStudentAsync(string studentCode, string password)
        {
            var student = await _context.Students.FirstOrDefaultAsync(c => c.StudentCode == studentCode);
            if (student == null || !VerifyPassword(password, student.Password))
            {
                throw new Exception("Invalid phone or password.");
            }

            return await GenerateJwtTokenStudent(student);
        }
        //--------------------------------------------------

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            
            return enteredPassword == storedPassword; // Change this to your actual hash check
        }

        public async Task<string> GenerateJwtToken(Coach coach)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, coach.CoachId.ToString()),
                new Claim(ClaimTypes.Name, coach.CoachName),
                new Claim(ClaimTypes.Role, coach.TypeUserId == 1 ? "HLV" : "Admin") // Assuming 1 is HLV and 2 is Admin
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //--------------------------------------------------
        public async Task<string> GenerateJwtTokenStudent(Student std)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, std.StudentId.ToString()),
                new Claim(ClaimTypes.Name, std.StudentName),
                new Claim(ClaimTypes.Role, "HocVien")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        //--------------------------------------------------
    }
}
