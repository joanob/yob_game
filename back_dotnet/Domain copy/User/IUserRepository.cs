namespace Domain;

public interface IUserRepository
{
    void Signup(User user, string password);

    Task<User> Login(string username, string password);

    Task<User> GetById(int id);

    Task UpdateCompanyName(int userId, string companyName);

    Task UpdateCompanyMoney(int userId, int money);
}