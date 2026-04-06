using Dapper;
using StaffService.Data;
using StaffService.InternalModels.Entities;
using System.Data;

namespace StaffService.Repository;

public class StaffRepository : BaseRepository, IStaffRepository
{
    public StaffRepository(IDbConnectionFactory connectionFactory)
        : base(connectionFactory)
    {
    }

    public async Task<IReadOnlyCollection<StaffEntity>> GetAllAsync()
    {
        var rows = await QueryAsync<StaffEntity>(
            StoredProcedureNames.GetStaff,
            commandType: CommandType.StoredProcedure);
        return rows.ToList();
    }

    public Task<StaffEntity?> GetByIdAsync(int id)
    {
        return QuerySingleOrDefaultAsync<StaffEntity>(
            StoredProcedureNames.GetStaffById,
            new { Id = id },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IReadOnlyCollection<StaffEntity>> GetByDepartmentAsync(int departmentId)
    {
        var rows = await QueryAsync<StaffEntity>(
            StoredProcedureNames.GetStaffByDepartmentId,
            new { DepartmentId = departmentId },
            commandType: CommandType.StoredProcedure);
        return rows.ToList();
    }

    public async Task<IReadOnlyCollection<StaffEntity>> GetByStaffTypeAsync(string staffType)
    {
        var rows = await QueryAsync<StaffEntity>(
            StoredProcedureNames.GetStaffByType,
            new { StaffType = staffType },
            commandType: CommandType.StoredProcedure);
        return rows.ToList();
    }

    public async Task<StaffEntity> CreateAsync(StaffEntity staff)
    {
        var id = await ExecuteScalarAsync<int>(
            StoredProcedureNames.CreateStaff,
            staff,
            commandType: CommandType.StoredProcedure);
        staff.Id = id;
        return staff;
    }

    public async Task<StaffEntity?> UpdateAsync(StaffEntity staff)
    {
        var rowsAffected = await ExecuteAsync(
            StoredProcedureNames.UpdateStaff,
            staff,
            commandType: CommandType.StoredProcedure);
        return rowsAffected > 0 ? staff : null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var rowsAffected = await ExecuteAsync(
            StoredProcedureNames.DeleteStaff,
            new { Id = id },
            commandType: CommandType.StoredProcedure);
        return rowsAffected > 0;
    }
}


