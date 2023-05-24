namespace Domain;

public interface IUserService
{
    Task Signup(string username, string password);

    Task<User> Login(string username, string password);

    Task<User> GetUserById(int id);
}