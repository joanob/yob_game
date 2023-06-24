namespace Domain;

public interface IUserRepository
{
    void Signup(User user, string password);

    User Login(string username, string password);

    User GetById(int id);

    void UpdateCompanyName(int userId, string companyName);

    void UpdateCompanyMoney(int userId, int money);
}