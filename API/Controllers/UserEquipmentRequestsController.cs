using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserEquipmentRequestsController : ControllerBase
{
    private readonly IUserEquipmentRequestService _requestService;

    public UserEquipmentRequestsController(IUserEquipmentRequestService requestService)
    {
        _requestService = requestService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserEquipmentRequest>>> GetAllRequests()
    {
        try
        {
            var requests = await _requestService.GetAllRequestsAsync();
            return Ok(requests);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserEquipmentRequest>> GetRequest(Guid id)
    {
        try
        {
            var request = await _requestService.GetRequestByIdAsync(id);
            if (request == null)
                return NotFound($"Request with ID {id} not found");

            return Ok(request);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }



    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<UserEquipmentRequest>>> GetRequestsByUserId(Guid userId)
    {
        try
        {
            var requests = await _requestService.GetRequestsByUserIdAsync(userId);
            return Ok(requests);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("equipment/{equipmentId}")]
    public async Task<ActionResult<IEnumerable<UserEquipmentRequest>>> GetRequestsByEquipment(Guid equipmentId)
    {
        try
        {
            var requests = await _requestService.GetRequestsByEquipmentIdAsync(equipmentId);
            return Ok(requests);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("priority/{priority}")]
    public async Task<ActionResult<IEnumerable<UserEquipmentRequest>>> GetRequestsByPriority(Priority priority)
    {
        try
        {
            var requests = await _requestService.GetRequestsByPriorityAsync(priority);
            return Ok(requests);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("pending")]
    public async Task<ActionResult<IEnumerable<UserEquipmentRequest>>> GetPendingRequests()
    {
        try
        {
            var requests = await _requestService.GetPendingRequestsAsync();
            return Ok(requests);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("/create")]
    public async Task<ActionResult<UserEquipmentRequest>> CreateRequest([FromBody] UserEquipmentRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdRequest = await _requestService.CreateRequestAsync(request);
            return CreatedAtAction(nameof(GetRequest), new { id = createdRequest.Id }, createdRequest);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserEquipmentRequest>> UpdateRequest(Guid id, [FromBody] UserEquipmentRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.Id = id;
            var updatedRequest = await _requestService.UpdateRequestAsync(request);
            return Ok(updatedRequest);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRequest(Guid id)
    {
        try
        {
            await _requestService.DeleteRequestAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
