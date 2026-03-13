using StaffService.InternalModels.Entities;

namespace StaffService.Services;

public static class StaffStore
{
    public static int StaffSeed = 1;
    public static readonly List<StaffEntity> Staff = new()
    {
        new StaffEntity
        {
            Id = 1,
            StaffCode = "STF001",
            FirstName = "Meena",
            LastName = "Reddy",
            StaffType = "Nurse",
            DepartmentId = 1,
            Email = "meena.reddy@hm.local",
            Phone = "9000002001",
            Shift = "Day",
            IsActive = true
        }
    };
}


