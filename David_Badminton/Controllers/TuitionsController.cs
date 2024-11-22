using David_Badminton.IServices;
using David_Badminton.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace David_Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TuitionsController : ControllerBase
    {
        private readonly ITuitionsService _tuitionsService;

        public TuitionsController(ITuitionsService tuitionsService)
        {
            _tuitionsService = tuitionsService;
        }

        [Authorize(Roles = "Admin,HocVien")]
        [HttpGet("studentId/{id}")]
        public async Task<IActionResult> GetListWithStudentId(int id)
        {
            var tuitions = await _tuitionsService.GetListWithStudentIdAsync(id);
            return Ok(tuitions);
        }
    }
}
