namespace Domain;

public class User
{
    public User(int Id, string Username)
    {
        this.Id = Id;
        this.Username = Username;
    }

    public int Id { get; set; }
    public string Username { get; set; }
}