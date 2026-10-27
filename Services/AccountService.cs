using para.Models;
using para.Repositories;

namespace para.Services;

public class AccountService(IRepository<User> users, IRepository<Role> roles, IStoreRules rules) : IAccountService
{
    public async Task<User?> LoginAsync(string login, string password)
    {
        var user = await users.FirstAsync(u => u.Login == login && u.IsActive);
        if (user == null) return null;
        try
        {
            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash) ? user : null;
        }
        catch (BCrypt.Net.SaltParseException)
        {
            return null;
        }
    }

    public async Task<User> RegisterAsync(string login, string password, string fullName)
    {
        var role = await roles.FirstAsync(r => r.RoleName == "User")
            ?? throw new StoreException(StoreError.NotFound, "Роль пользователя не найдена.");
        var user = new User
        {
            Login = login, PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            FullName = fullName, RoleID = role.RoleID, Role = role, IsActive = true
        };
        await rules.ValidateAsync(user);
        users.Add(user);
        await users.SaveChangesAsync();
        return user;
    }
}
