namespace ClientHubPortal.Models.Clients;

public class ClientDetailViewModel
{
    public ClientViewModel ClientDetail { get; set; }
    public IEnumerable<ContactViewModel> ClientContacts { get; set; }
}
