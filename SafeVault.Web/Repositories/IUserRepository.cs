using SafeVault.Web.Models;

namespace SafeVault.Web.Repositories
{
    public interface IUserRepository
    {
        Task CreateUserAsync(string username, string email);
        Task CreateUserWithAuthAsync(string username, string email, string passwordHash, string role);
        Task<AuthUser?> GetUserForLoginAsync(string username);
    }
}