using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaffService.Utils.Common;
using StaffService.InternalModels.DTOs;
using StaffService.InternalModels.Entities;
using StaffService.Services;

namespace StaffService.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/staff")]
public class StaffController : ControllerBase
{
    [HttpGet]
    public ActionResult<ApiResponse<IEnumerable<StaffDto>>> GetAll([FromQuery] string? staffType)
    {
        var query = StaffStore.Staff.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(staffType))
        {
            query = query.Where(x => x.StaffType.Equals(staffType, StringComparison.OrdinalIgnoreCase));
        }

        return Ok(ApiResponse<IEnumerable<StaffDto>>.Ok(query.Select(StaffDto.FromEntity).ToList()));
    }

    [HttpGet("{id:int}")]
    public ActionResult<ApiResponse<StaffDto>> GetById(int id)
    {
        var staff = StaffStore.Staff.FirstOrDefault(x => x.Id == id);
        if (staff is null)
        {
            return NotFound(ApiResponse<StaffDto>.Fail("Staff not found"));
        }

        return Ok(ApiResponse<StaffDto>.Ok(StaffDto.FromEntity(staff)));
    }

    [HttpGet("department/{departmentId:int}")]
    public ActionResult<ApiResponse<IEnumerable<StaffDto>>> GetByDepartment(int departmentId)
    {
        var staff = StaffStore.Staff.Where(x => x.DepartmentId == departmentId).Select(StaffDto.FromEntity).ToList();
        return Ok(ApiResponse<IEnumerable<StaffDto>>.Ok(staff));
    }

    [HttpPost]
    public ActionResult<ApiResponse<StaffDto>> Create([FromBody] CreateStaffDto dto)
    {
        var id = Interlocked.Increment(ref StaffStore.StaffSeed);
        var staff = new StaffEntity
        {
            Id = id,
            StaffCode = dto.StaffCode,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            StaffType = dto.StaffType,
            DepartmentId = dto.DepartmentId,
            Email = dto.Email,
            Phone = dto.Phone,
            Shift = dto.Shift,
            IsActive = true
        };

        StaffStore.Staff.Add(staff);
        return CreatedAtAction(nameof(GetById), new { id }, ApiResponse<StaffDto>.Ok(StaffDto.FromEntity(staff), "Staff created"));
    }

    [HttpPut("{id:int}")]
    public ActionResult<ApiResponse<StaffDto>> Update(int id, [FromBody] UpdateStaffDto dto)
    {
        var staff = StaffStore.Staff.FirstOrDefault(x => x.Id == id);
        if (staff is null)
        {
            return NotFound(ApiResponse<StaffDto>.Fail("Staff not found"));
        }

        staff.FirstName = dto.FirstName;
        staff.LastName = dto.LastName;
        staff.StaffType = dto.StaffType;
        staff.DepartmentId = dto.DepartmentId;
        staff.Email = dto.Email;
        staff.Phone = dto.Phone;
        staff.Shift = dto.Shift;
        staff.IsActive = dto.IsActive;

        return Ok(ApiResponse<StaffDto>.Ok(StaffDto.FromEntity(staff), "Staff updated"));
    }

    [HttpDelete("{id:int}")]
    public ActionResult<ApiResponse<string>> Delete(int id)
    {
        var staff = StaffStore.Staff.FirstOrDefault(x => x.Id == id);
        if (staff is null)
        {
            return NotFound(ApiResponse<string>.Fail("Staff not found"));
        }

        StaffStore.Staff.Remove(staff);
        return Ok(ApiResponse<string>.Ok("Staff deleted"));
    }
}


