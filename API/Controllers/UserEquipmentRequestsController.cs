using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Application.DTOs;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
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

    // [HttpGet("{id}")]
    // public async Task<ActionResult<UserEquipmentRequest>> GetRequest(Guid id)
    // {
    //     try
    //     {
    //         var request = await _requestService.GetRequestByIdAsync(id);
    //         if (request == null)
    //             return NotFound($"Request with ID {id} not found");
    //
    //         return Ok(request);
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, $"Internal server error: {ex.Message}");
    //     }
    // }



    // [HttpGet("user/{userId}")]
    // public async Task<ActionResult<IEnumerable<UserEquipmentRequest>>> GetRequestsByUserId(Guid userId)
    // {
    //     try
    //     {
    //         var requests = await _requestService.GetRequestsByUserIdAsync(userId);
    //         return Ok(requests);
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, $"Internal server error: {ex.Message}");
    //     }
    // }


    [HttpPost("create")]
    public async Task<ActionResult<CreateUserEquipmentRequestDTO>> CreateRequest([FromBody] CreateUserEquipmentRequestDTO  request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token");
            }

            request.UserId = Guid.Parse(userIdClaim.Value);
            
            var createdRequest = await _requestService.CreateRequestAsync(request);
            return Ok(request);
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

    [HttpPost("update-status")]
    public async Task<ActionResult> UpdateRequestStatus([FromBody]UpdateRequestStatusDTO request) {
        try
        {
             await _requestService.UpdateRequestStatusAsync(request.RequestId, request.Status, request.UserId);
             return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    // [HttpPut("{id}")]
    // public async Task<ActionResult<UserEquipmentRequest>> UpdateRequest(Guid id, [FromBody] UserEquipmentRequest request)
    // {
    //     try
    //     {
    //         if (!ModelState.IsValid)
    //             return BadRequest(ModelState);
    //
    //         request.Id = id;
    //         var updatedRequest = await _requestService.UpdateRequestAsync(request);
    //         return Ok(updatedRequest);
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

    // [HttpDelete("{id}")]
    // public async Task<ActionResult> DeleteRequest(Guid id)
    // {
    //     try
    //     {
    //         await _requestService.DeleteRequestAsync(id);
    //         return NoContent();
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, $"Internal server error: {ex.Message}");
    //     }
    // }
}
