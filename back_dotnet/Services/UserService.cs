using Domain;

namespace Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly uint initialCompanyMoney = 0;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Login(string username, string password)
    {
        if (username == "" || password == "")
        {
            throw new Exception("Empty data");
        }
        return await _userRepository.Login(username, password);
    }

    public async Task Signup(string username, string password)
    {
        if (username == "" || password == "")
        {
            throw new Exception("Empty data");
        }

        var user = new User(0, username, "", initialCompanyMoney);

        await _userRepository.Signup(user, password);
    }

    public async Task<User> GetUserById(int id)
    {
        return await _userRepository.GetById(id);
    }

    public async Task UpdateCompanyName(int userId, string companyName)
    {
        if (companyName == "")
        {
            throw new Exception("Company name is empty");
        }
        await _userRepository.UpdateCompanyName(userId, companyName);
    }

    public async Task UpdateCompanyMoney(int userId, int money)
    {
        if (money < 0)
        {
            throw new Exception("Money cannot be negative");
        }
        await _userRepository.UpdateCompanyMoney(userId, money);
    }
}