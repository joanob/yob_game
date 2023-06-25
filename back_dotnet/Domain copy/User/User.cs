namespace Domain;

public class User
{
    public User(int Id, string Username, string CompanyName, uint CompanyMoney)
    {
        this.Id = Id;
        this.Username = Username;
        this.CompanyName = CompanyName;
        this.CompanyMoney = CompanyMoney;
    }

    public int Id { get; set; }
    public string Username { get; set; }
    public string CompanyName { get; set; }
    public uint CompanyMoney { get; set; }
}