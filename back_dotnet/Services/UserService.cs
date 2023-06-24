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

    public User Login(string username, string password)
    {
        if (username == "" || password == "")
        {
            throw new Exception("Empty data");
        }
        return _userRepository.Login(username, password);
    }

    public void Signup(string username, string password)
    {
        if (username == "" || password == "")
        {
            throw new Exception("Empty data");
        }

        var user = new User(0, username, "", initialCompanyMoney);

        _userRepository.Signup(user, password);
    }

    public User GetUserById(int id)
    {
        return _userRepository.GetById(id);
    }

    public void UpdateCompanyName(int userId, string companyName)
    {
        if (companyName == "")
        {
            throw new Exception("Company name is empty");
        }
        _userRepository.UpdateCompanyName(userId, companyName);
    }

    public void UpdateCompanyMoney(int userId, int money)
    {
        if (money < 0)
        {
            throw new Exception("Money cannot be negative");
        }
        _userRepository.UpdateCompanyMoney(userId, money);
    }

}