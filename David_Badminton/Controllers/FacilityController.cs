using David_Badminton.IServices;
using David_Badminton.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace David_Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private readonly IFacilityService _facilityService;

        public FacilityController(IFacilityService facilityService)
        {
            _facilityService = facilityService;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllFacilities()
        {
            var facilities = await _facilityService.GetAllFacilitiesAsync();
            return Ok(facilities);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFacilityById(int id)
        {
            var facility = await _facilityService.GetFacilityByIdAsync(id);
            if (facility == null)
            {
                return NotFound();
            }
            return Ok(facility);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddFacility([FromBody] Facility facility)
        {
            await _facilityService.AddFacilityAsync(facility);
            return CreatedAtAction(nameof(GetFacilityById), new { id = facility.FacilityId }, facility);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFacility(int id, [FromBody] Facility facility)
        {
            if (id != facility.FacilityId)
            {
                return BadRequest();
            }
            await _facilityService.UpdateFacilityAsync(facility);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacility(int id)
        {
            await _facilityService.DeleteFacilityAsync(id);
            return NoContent();
        }
    }
}
