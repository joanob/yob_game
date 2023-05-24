using Domain;

namespace Data;

public class UserDTO : User
{
    public UserDTO(int Id, string Username, string Password) : base(Id, Username)
    {
        this.Password = Password;
    }

    public string Password { get; set; }
}