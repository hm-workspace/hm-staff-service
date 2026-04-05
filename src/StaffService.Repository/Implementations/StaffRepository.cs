using Dapper;
using StaffService.Data;
using StaffService.InternalModels.Entities;

namespace StaffService.Repository;

public class StaffRepository : IStaffRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public StaffRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IReadOnlyCollection<StaffEntity>> GetAllAsync()
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM Staff";
            var rows = await connection.QueryAsync<StaffEntity>(sql);
            return rows.ToList();
        }
        catch
        {
            return new List<StaffEntity>();
        }
    }

    public async Task<StaffEntity?> GetByIdAsync(int id)
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM Staff WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<StaffEntity>(sql, new { Id = id });
        }
        catch
        {
            return null;
        }
    }

    public async Task<IReadOnlyCollection<StaffEntity>> GetByDepartmentAsync(int departmentId)
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM Staff WHERE DepartmentId = @DepartmentId";
            var rows = await connection.QueryAsync<StaffEntity>(sql, new { DepartmentId = departmentId });
            return rows.ToList();
        }
        catch
        {
            return new List<StaffEntity>();
        }
    }

    public async Task<IReadOnlyCollection<StaffEntity>> GetByStaffTypeAsync(string staffType)
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM Staff WHERE StaffType = @StaffType";
            var rows = await connection.QueryAsync<StaffEntity>(sql, new { StaffType = staffType });
            return rows.ToList();
        }
        catch
        {
            return new List<StaffEntity>();
        }
    }

    public async Task<StaffEntity> CreateAsync(StaffEntity staff)
    {
        using var connection = _connectionFactory.CreateConnection();
        const string sql = @"
            INSERT INTO Staff (StaffCode, FirstName, LastName, StaffType, DepartmentId, Email, Phone, Shift, IsActive)
            VALUES (@StaffCode, @FirstName, @LastName, @StaffType, @DepartmentId, @Email, @Phone, @Shift, @IsActive);
            SELECT CAST(SCOPE_IDENTITY() as int);";

        var id = await connection.ExecuteScalarAsync<int>(sql, staff);
        staff.Id = id;
        return staff;
    }

    public async Task<StaffEntity?> UpdateAsync(StaffEntity staff)
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                UPDATE Staff 
                SET StaffCode = @StaffCode, 
                    FirstName = @FirstName, 
                    LastName = @LastName, 
                    StaffType = @StaffType, 
                    DepartmentId = @DepartmentId, 
                    Email = @Email, 
                    Phone = @Phone, 
                    Shift = @Shift, 
                    IsActive = @IsActive
                WHERE Id = @Id";

            var rowsAffected = await connection.ExecuteAsync(sql, staff);
            return rowsAffected > 0 ? staff : null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = "DELETE FROM Staff WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }
        catch
        {
            return false;
        }
    }
}


