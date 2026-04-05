using StaffService.InternalModels.Entities;

namespace StaffService.Repository;

public interface IStaffRepository
{
    Task<IReadOnlyCollection<StaffEntity>> GetAllAsync();
    Task<StaffEntity?> GetByIdAsync(int id);
    Task<IReadOnlyCollection<StaffEntity>> GetByDepartmentAsync(int departmentId);
    Task<IReadOnlyCollection<StaffEntity>> GetByStaffTypeAsync(string staffType);
    Task<StaffEntity> CreateAsync(StaffEntity staff);
    Task<StaffEntity?> UpdateAsync(StaffEntity staff);
    Task<bool> DeleteAsync(int id);
}

