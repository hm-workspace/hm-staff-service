using StaffService.InternalModels.DTOs;

namespace StaffService.Services;

public interface IStaffService
{
    Task<IEnumerable<StaffDto>> GetAllAsync(string? staffType = null);
    Task<StaffDto?> GetByIdAsync(int id);
    Task<IEnumerable<StaffDto>> GetByDepartmentAsync(int departmentId);
    Task<StaffDto> CreateAsync(CreateStaffDto dto);
    Task<StaffDto?> UpdateAsync(int id, UpdateStaffDto dto);
    Task<bool> DeleteAsync(int id);
}
