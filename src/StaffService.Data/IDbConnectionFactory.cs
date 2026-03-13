using System.Data;

namespace StaffService.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection(string connectionName = "DefaultConnection");
}


