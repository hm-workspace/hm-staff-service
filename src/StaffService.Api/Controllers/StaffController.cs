using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaffService.Utils.Common;
using StaffService.InternalModels.DTOs;
using StaffService.Services;

namespace StaffService.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/staff")]
public class StaffController : ControllerBase
{
    private readonly IStaffService _staffService;

    public StaffController(IStaffService staffService)
    {
        _staffService = staffService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<StaffDto>>>> GetAll([FromQuery] string? staffType)
    {
        var staff = await _staffService.GetAllAsync(staffType);
        return Ok(ApiResponse<IEnumerable<StaffDto>>.Ok(staff));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<StaffDto>>> GetById(int id)
    {
        var staff = await _staffService.GetByIdAsync(id);
        if (staff is null)
        {
            return NotFound(ApiResponse<StaffDto>.Fail("Staff not found"));
        }

        return Ok(ApiResponse<StaffDto>.Ok(staff));
    }

    [HttpGet("department/{departmentId:int}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<StaffDto>>>> GetByDepartment(int departmentId)
    {
        var staff = await _staffService.GetByDepartmentAsync(departmentId);
        return Ok(ApiResponse<IEnumerable<StaffDto>>.Ok(staff));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<StaffDto>>> Create([FromBody] CreateStaffDto dto)
    {
        var staff = await _staffService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = staff.Id }, ApiResponse<StaffDto>.Ok(staff, "Staff created"));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<StaffDto>>> Update(int id, [FromBody] UpdateStaffDto dto)
    {
        var staff = await _staffService.UpdateAsync(id, dto);
        if (staff is null)
        {
            return NotFound(ApiResponse<StaffDto>.Fail("Staff not found"));
        }

        return Ok(ApiResponse<StaffDto>.Ok(staff, "Staff updated"));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
    {
        var deleted = await _staffService.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(ApiResponse<string>.Fail("Staff not found"));
        }

        return Ok(ApiResponse<string>.Ok("Staff deleted"));
    }
}


