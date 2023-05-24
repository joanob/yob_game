using Domain;

namespace Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

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
        await _userRepository.Signup(username, password);
    }

    public async Task<User> GetUserById(int id)
    {
        return await _userRepository.GetById(id);
    }
}