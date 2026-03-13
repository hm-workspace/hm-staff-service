using StaffService.InternalModels.Entities;

namespace StaffService.Repository;

public interface IStaffRepository
{
    Task<IReadOnlyCollection<StaffEntity>> GetAllAsync();
}

