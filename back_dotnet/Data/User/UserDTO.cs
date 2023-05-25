using Domain;

namespace Data;

public class UserDTO : User
{
    public UserDTO(int Id, string Username, string Password, string CompanyName, uint CompanyMoney) : base(Id, Username, CompanyName, CompanyMoney)
    {
        this.Password = Password;
    }

    public string Password { get; set; }
}