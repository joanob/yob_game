namespace Domain;

public interface IUserService
{
    void Signup(string username, string password);

    Task<User> Login(string username, string password);

    Task<User> GetUserById(int id);

    Task UpdateCompanyName(int userId, string companyName);

    Task UpdateCompanyMoney(int userId, int money);
}