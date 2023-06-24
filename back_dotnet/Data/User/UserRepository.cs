using Domain;

namespace Data;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public User Login(string username, string password)
    {
        var user = _context.Users.Where(u => u.Username == username).FirstOrDefault();

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

    public User GetById(int id)
    {
        var user = _context.Users.Find(id);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        return new User(user.Id, user.Username, user.CompanyName, user.CompanyMoney);
    }

    public void UpdateCompanyName(int userId, string companyName)
    {
        var user = _context.Users.Find(userId);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        user.CompanyName = companyName;
    }

    public void UpdateCompanyMoney(int userId, int money)
    {
        var user = _context.Users.Find(userId);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        user.CompanyMoney = (uint)money;
    }
}