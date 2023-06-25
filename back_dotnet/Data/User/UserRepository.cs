using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Signup(User user, string password)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        UserDTO userdto = new UserDTO(user.Id, user.Username, passwordHash, user.CompanyName, user.CompanyMoney);

        try
        {
            _context.Users.Add(userdto);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<User> Login(string username, string password)
    {
        var user = await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();

        if (user == null)
        {
            throw new Exception("Not user found with username: " + username);
        }

        if (BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return new User(user.Id, user.Username, user.CompanyName, user.CompanyMoney);
        }

        throw new Exception("Incorrect password for username: " + username);
    }

    public async Task<User> GetById(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        return new User(user.Id, user.Username, user.CompanyName, user.CompanyMoney);
    }

    public async Task UpdateCompanyName(int userId, string companyName)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        user.CompanyName = companyName;
    }

    public async Task UpdateCompanyMoney(int userId, int money)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        user.CompanyMoney = (uint)money;
    }
}