using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SafeVault.Web.Models;

namespace SafeVault.Web.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly string _connectionString;
        
        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string missing.");
        }

        public async Task CreateUserAsync(string username, string email)
        {
            const string sql = @"
                INSERT INTO Users (Username, Email)
                VALUES (@Username, @Email);
            ";

            await using var conn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 100).Value = username;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = email;

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task CreateUserWithAuthAsync(string username, string email, string passwordHash, string role)
{
    const string sql = @"
        INSERT INTO Users (Username, Email, PasswordHash, Role)
        VALUES (@Username, @Email, @PasswordHash, @Role);";

    await using var conn = new SqlConnection(_connectionString);
    await using var cmd = new SqlCommand(sql, conn);

    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 100).Value = username;
    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = email;
    cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 255).Value = passwordHash;
    cmd.Parameters.Add("@Role", SqlDbType.NVarChar, 50).Value = role;

    await conn.OpenAsync();
    await cmd.ExecuteNonQueryAsync();
}

public async Task<AuthUser?> GetUserForLoginAsync(string username)
{
    const string sql = @"
        SELECT UserID, Username, Email, PasswordHash, Role
        FROM Users
        WHERE Username = @Username;";

    await using var conn = new SqlConnection(_connectionString);
    await using var cmd = new SqlCommand(sql, conn);
    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 100).Value = username;

    await conn.OpenAsync();
    await using var reader = await cmd.ExecuteReaderAsync();

    if (await reader.ReadAsync())
    {
        return new AuthUser
        {
            UserID = reader.GetInt32(0),
            Username = reader.GetString(1),
            Email = reader.GetString(2),
            PasswordHash = reader.IsDBNull(3) ? "" : reader.GetString(3),
            Role = reader.IsDBNull(4) ? "User" : reader.GetString(4)
        };
    }

    return null;
}
    }
}