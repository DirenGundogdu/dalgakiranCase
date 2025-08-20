using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.Entities;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentsController : ControllerBase
{
    private readonly IEquipmentService _equipmentService;

    public EquipmentsController(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Equipment>>> GetAllEquipments()
    {
        try
        {
            var equipments = await _equipmentService.GetAllEquipmentsAsync();
            return Ok(equipments);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
    [HttpGet("/api/UnassignedEquipments")]
    public async Task<IActionResult> GetAllUnassignedEquipment() {
        try
        {
            var equipments = await _equipmentService.GetAllUnassignedEquipmentAsync();
            return Ok(equipments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    
    // [HttpGet("{id}")]
    // public async Task<ActionResult<Equipment>> GetEquipment(Guid id)
    // {
    //     try
    //     {
    //         var equipment = await _equipmentService.GetEquipmentByIdAsync(id);
    //         if (equipment == null)
    //             return NotFound($"Equipment with ID {id} not found");
    //
    //         return Ok(equipment);
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, $"Internal server error: {ex.Message}");
    //     }
    // }
    

    // [HttpPost]
    // public async Task<ActionResult<Equipment>> CreateEquipment([FromBody] Equipment equipment)
    // {
    //     try
    //     {
    //         if (!ModelState.IsValid)
    //             return BadRequest(ModelState);
    //
    //         var createdEquipment = await _equipmentService.CreateEquipmentAsync(equipment);
    //         return CreatedAtAction(nameof(GetEquipment), new { id = createdEquipment.Id }, createdEquipment);
    //     }
    //     catch (ArgumentException ex)
    //     {
    //         return BadRequest(ex.Message);
    //     }
    //     catch (InvalidOperationException ex)
    //     {
    //         return Conflict(ex.Message);
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, $"Internal server error: {ex.Message}");
    //     }
    // }
    //
    // [HttpPut("{id}")]
    // public async Task<ActionResult<Equipment>> UpdateEquipment(Guid id, [FromBody] Equipment equipment)
    // {
    //     try
    //     {
    //         if (!ModelState.IsValid)
    //             return BadRequest(ModelState);
    //
    //         equipment.Id = id;
    //         var updatedEquipment = await _equipmentService.UpdateEquipmentAsync(equipment);
    //         return Ok(updatedEquipment);
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
    // public async Task<ActionResult> DeleteEquipment(Guid id)
    // {
    //     try
    //     {
    //         await _equipmentService.DeleteEquipmentAsync(id);
    //         return NoContent();
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, $"Internal server error: {ex.Message}");
    //     }
    // }
    
}
