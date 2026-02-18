namespace ClientHubDatabase.Models;

public class Contacts
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Fullname { get; set; }
    public string EmailAddress { get; set; }
    public int NoOfClients { get; set; }
    public Guid? ClientId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

}
