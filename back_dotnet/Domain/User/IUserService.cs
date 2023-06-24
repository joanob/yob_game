namespace Domain;

public interface IUserService
{
    void Signup(string username, string password);

    User Login(string username, string password);

    User GetUserById(int id);

    void UpdateCompanyName(int userId, string companyName);

    void UpdateCompanyMoney(int userId, int money);
}