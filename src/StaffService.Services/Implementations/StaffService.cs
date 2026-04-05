using StaffService.InternalModels.DTOs;
using StaffService.InternalModels.Entities;
using StaffService.Repository;

namespace StaffService.Services;

public class StaffService : IStaffService
{
    private readonly IStaffRepository _staffRepository;

    public StaffService(IStaffRepository staffRepository)
    {
        _staffRepository = staffRepository;
    }

    public async Task<IEnumerable<StaffDto>> GetAllAsync(string? staffType = null)
    {
        IReadOnlyCollection<StaffEntity> staff;

        if (!string.IsNullOrWhiteSpace(staffType))
        {
            staff = await _staffRepository.GetByStaffTypeAsync(staffType);
        }
        else
        {
            staff = await _staffRepository.GetAllAsync();
        }

        return staff.Select(StaffDto.FromEntity).ToList();
    }

    public async Task<StaffDto?> GetByIdAsync(int id)
    {
        var staff = await _staffRepository.GetByIdAsync(id);
        return staff is not null ? StaffDto.FromEntity(staff) : null;
    }

    public async Task<IEnumerable<StaffDto>> GetByDepartmentAsync(int departmentId)
    {
        var staff = await _staffRepository.GetByDepartmentAsync(departmentId);
        return staff.Select(StaffDto.FromEntity).ToList();
    }

    public async Task<StaffDto> CreateAsync(CreateStaffDto dto)
    {
        var staff = new StaffEntity
        {
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

        var createdStaff = await _staffRepository.CreateAsync(staff);
        return StaffDto.FromEntity(createdStaff);
    }

    public async Task<StaffDto?> UpdateAsync(int id, UpdateStaffDto dto)
    {
        var existingStaff = await _staffRepository.GetByIdAsync(id);
        if (existingStaff is null)
        {
            return null;
        }

        existingStaff.FirstName = dto.FirstName;
        existingStaff.LastName = dto.LastName;
        existingStaff.StaffType = dto.StaffType;
        existingStaff.DepartmentId = dto.DepartmentId;
        existingStaff.Email = dto.Email;
        existingStaff.Phone = dto.Phone;
        existingStaff.Shift = dto.Shift;
        existingStaff.IsActive = dto.IsActive;

        var updatedStaff = await _staffRepository.UpdateAsync(existingStaff);
        return updatedStaff is not null ? StaffDto.FromEntity(updatedStaff) : null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _staffRepository.DeleteAsync(id);
    }
}
