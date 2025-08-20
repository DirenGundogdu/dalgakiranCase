using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.Entities;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserEquipmentsController : ControllerBase
{
    private readonly IUserEquipmentService _userEquipmentService;

    public UserEquipmentsController(IUserEquipmentService userEquipmentService)
    {
        _userEquipmentService = userEquipmentService;

    }
    [HttpGet("user")]
    public async Task<ActionResult<IEnumerable<UserEquipmentRequest>>> GetRequestsByUser()
    {
        try
        {
            // JWT token'dan user ID'yi al
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token");
            }

            if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return BadRequest("Invalid user ID format");
            }

            var response = await _userEquipmentService.GetUserEquipmentsByUserIdAsync(userId);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<UserEquipment>>> GetAllUserEquipments()
    // {
    //     try
    //     {
    //         var userEquipments = await _userEquipmentService.GetAllUserEquipmentsAsync();
    //         return Ok(userEquipments);
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, $"Internal server error: {ex.Message}");
    //     }
    // }
    //
    // [HttpGet("{id}")]
    // public async Task<ActionResult<UserEquipment>> GetUserEquipment(Guid id)
    // {
    //     try
    //     {
    //         var userEquipment = await _userEquipmentService.GetUserEquipmentByIdAsync(id);
    //         if (userEquipment == null)
    //             return NotFound($"UserEquipment with ID {id} not found");
    //
    //         return Ok(userEquipment);
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, $"Internal server error: {ex.Message}");
    //     }
    // }
    //
    // [HttpGet("user/{userId}")]
    // public async Task<ActionResult<IEnumerable<UserEquipment>>> GetUserEquipmentsByUser(Guid userId)
    // {
    //     try
    //     {
    //         var userEquipments = await _userEquipmentService.GetUserEquipmentsByUserIdAsync(userId);
    //         return Ok(userEquipments);
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, $"Internal server error: {ex.Message}");
    //     }
    // }
    //
    // [HttpGet("equipment/{equipmentId}")]
    // public async Task<ActionResult<IEnumerable<UserEquipment>>> GetUserEquipmentsByEquipment(Guid equipmentId)
    // {
    //     try
    //     {
    //         var userEquipments = await _userEquipmentService.GetUserEquipmentsByEquipmentIdAsync(equipmentId);
    //         return Ok(userEquipments);
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, $"Internal server error: {ex.Message}");
    //     }
    // }

    // [HttpPost]
    // public async Task<ActionResult<UserEquipment>> CreateUserEquipment([FromBody] UserEquipment userEquipment)
    // {
    //     try
    //     {
    //         if (!ModelState.IsValid)
    //             return BadRequest(ModelState);
    //
    //         var createdUserEquipment = await _userEquipmentService.CreateUserEquipmentAsync(userEquipment);
    //         return CreatedAtAction(nameof(GetUserEquipment), new { id = createdUserEquipment.Id }, createdUserEquipment);
    //     }
    //     catch (ArgumentException ex)
    //     {
    //         return BadRequest(ex.Message);
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, $"Internal server error: {ex.Message}");
    //     }
    // }
    //
    // [HttpPut("{id}")]
    // public async Task<ActionResult<UserEquipment>> UpdateUserEquipment(Guid id, [FromBody] UserEquipment userEquipment)
    // {
    //     try
    //     {
    //         if (!ModelState.IsValid)
    //             return BadRequest(ModelState);
    //
    //         userEquipment.Id = id;
    //         var updatedUserEquipment = await _userEquipmentService.UpdateUserEquipmentAsync(userEquipment);
    //         return Ok(updatedUserEquipment);
    //     }
    //     catch (ArgumentException ex)
    //     {
    //         return BadRequest(ex.Message);
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, $"Internal server error: {ex.Message}");
    //     }
    // }
    //
    // [HttpDelete("{id}")]
    // public async Task<ActionResult> DeleteUserEquipment(Guid id)
    // {
    //     try
    //     {
    //         await _userEquipmentService.DeleteUserEquipmentAsync(id);
    //         return NoContent();
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, $"Internal server error: {ex.Message}");
    //     }
    // }
}

public class AssignEquipmentRequest
{
    public Guid UserId { get; set; }
    public Guid EquipmentId { get; set; }
}
