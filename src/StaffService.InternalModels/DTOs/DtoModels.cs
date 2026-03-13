using StaffService.InternalModels.Entities;

namespace StaffService.InternalModels.DTOs;

public class CreateStaffDto
{
    public string StaffCode { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string StaffType { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
}

public class UpdateStaffDto : CreateStaffDto
{
    public bool IsActive { get; set; } = true;
}

public class StaffDto : UpdateStaffDto
{
    public int Id { get; set; }

    public static StaffDto FromEntity(StaffEntity entity) => new()
    {
        Id = entity.Id,
        StaffCode = entity.StaffCode,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        StaffType = entity.StaffType,
        DepartmentId = entity.DepartmentId,
        Email = entity.Email,
        Phone = entity.Phone,
        Shift = entity.Shift,
        IsActive = entity.IsActive
    };
}


