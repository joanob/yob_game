using Domain;

namespace Data;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<User> Login(string username, string password)
    {
        var user = _context.Users.Where(u => u.Username == username).FirstOrDefault();

        if (user == null)
        {
            throw new Exception("Not user found with username: " + username);
        }

        if (BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return Task.FromResult(new User(user.Id, user.Username));
        }

        throw new Exception("Incorrect password for username: " + username);
    }

    public async Task Signup(string username, string password)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        UserDTO user = new UserDTO(0, username, passwordHash);

        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<User> GetById(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            throw new Exception("Username not found");
        }

        return new User(user.Id, user.Username);
    }
}