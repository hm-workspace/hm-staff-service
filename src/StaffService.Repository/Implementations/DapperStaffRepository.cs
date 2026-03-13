using Dapper;
using StaffService.Data;
using StaffService.InternalModels.Entities;

namespace StaffService.Repository;

public class DapperStaffRepository : IStaffRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DapperStaffRepository(IDbConnectionFactory connectionFactory)
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
}


