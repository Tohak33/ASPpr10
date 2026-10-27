using para.Models;

namespace para.Services;

public interface IAccountService
{
    Task<User?> LoginAsync(string login, string password);
    Task<User> RegisterAsync(string login, string password, string fullName);
}
