namespace StaffService.InternalModels.Entities;

public class StaffEntity
{
    public int Id { get; set; }
    public string StaffCode { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string StaffType { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}


