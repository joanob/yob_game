namespace Domain;

public interface IUserRepository
{
    Task Signup(string username, string password);

    Task<User> Login(string username, string password);

    Task<User> GetById(int id);
}