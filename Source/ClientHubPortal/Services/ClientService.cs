namespace ClientHubPortal.Services;

public class ClientService : IClientService
{
    #region -- protected properties --
    protected readonly ILogger<ClientService> logger;
    protected readonly IClientRepository clientRepository;
    #endregion -- protected properties --
    public ClientService(ILogger<ClientService> logger, IClientRepository clientRepository)
    {
        this.logger = logger;
        this.clientRepository = clientRepository;
    }


    #region ----------------- Clients Section -----------------

    public async Task<GenericResponse> CreateClientAsync(ClientViewModel model)
    {
        var client = new Clients()
        {
            Name = model.Name
        };
        return await clientRepository.CreateClientAsync(client);
    }


    public async Task<GenericResponse> DeleteClientAsync(Guid clientId)
    {
        return await clientRepository.DeleteClientAsync(clientId);
    }



    public async Task<GenericResponse<List<ClientViewModel>>> GetContactClientsAsync(Guid contactId)
    {
        var response = await clientRepository.GetContactClientsAsync(contactId);
        return new GenericResponse<List<ClientViewModel>>
        {
            Status = response.Status,
            StatusCode = response.StatusCode,
            StatusMessage = response.StatusMessage,
            Data = !response.Data.Any() ? new List<ClientViewModel>() : response.Data.Select(s => new ClientViewModel()
            {
                Id = s.Id,
                Name = s.Name,
                NameCode = s.NameCode,
                NoOfContacts = s.NoOfContacts,
                CreatedAt = s.CreatedAt,
                DeletedAt = s.DeletedAt,
            }).ToList(),
        };
    }


    public async Task<GenericResponse<List<ClientViewModel>>> GetUnlinkedClientssAsync(Guid contactId)
    {
        var response = await clientRepository.GetUnlinkedClientssAsync(contactId);
        return new GenericResponse<List<ClientViewModel>>
        {
            Status = response.Status,
            StatusCode = response.StatusCode,
            StatusMessage = response.StatusMessage,
            Data = !response.Data.Any() ? new List<ClientViewModel>() : response.Data.Select(s => new ClientViewModel()
            {
                Id = s.Id,
                Name = s.Name,
                NameCode = s.NameCode,
                NoOfContacts = s.NoOfContacts,
                CreatedAt = s.CreatedAt,
                DeletedAt = s.DeletedAt,
            }).ToList(),
        };
    }


    public async Task<GenericResponse<List<ClientViewModel>>> GetClientsAsync()
    {
        var response = await clientRepository.GetClientsAsync();
        return new GenericResponse<List<ClientViewModel>>
        {
            Status = response.Status,
            StatusCode = response.StatusCode,
            StatusMessage = response.StatusMessage,
            Data = !response.Data.Any() ? new List<ClientViewModel>() : response.Data.Select(s => new ClientViewModel()
            {
                Id = s.Id,
                Name = s.Name,
                NameCode = s.NameCode,
                NoOfContacts = s.NoOfContacts,
                CreatedAt = s.CreatedAt,
                DeletedAt = s.DeletedAt,
            }).ToList(),
        };
    }

    #endregion



    #region ----------------- Contacts Section -----------------


    public async Task<GenericResponse> CreateContactAsync(ContactViewModel model)
    {
        var contact = new Contacts()
        {
            Name = model.Name,
            Surname = model.Surname,
            EmailAddress = model.EmailAddress,
        };
        return await clientRepository.CreateContactAsync(contact);
    }

    public async Task<GenericResponse> DeleteContactAsync(Guid contactId)
    {
        return await clientRepository.DeleteContactAsync(contactId);
    }



    public async Task<GenericResponse<List<ContactViewModel>>> GetContactsAsync()
    {
        var response = await clientRepository.GetContactsAsync();
        return new GenericResponse<List<ContactViewModel>>
        {
            Status = response.Status,
            StatusCode = response.StatusCode,
            StatusMessage = response.StatusMessage,
            Data = !response.Data.Any() ? new List<ContactViewModel>() : response.Data.Select(s => new ContactViewModel()
            {
                Id = s.Id,
                Name = s.Name,
                Surname = s.Surname,
                EmailAddress = s.EmailAddress,
                NoOfClients = s.NoOfClients,
                CreatedAt = s.CreatedAt,
                DeletedAt = s.DeletedAt,
            }).ToList(),
        };
    }


    public async Task<GenericResponse<List<ContactViewModel>>> GetClientContactsAsync(Guid clientId)
    {
        var response = await clientRepository.GetClientContactsAsync(clientId);
        return new GenericResponse<List<ContactViewModel>>
        {
            Status = response.Status,
            StatusCode = response.StatusCode,
            StatusMessage = response.StatusMessage,
            Data = !response.Data.Any() ? new List<ContactViewModel>() : response.Data.Select(s => new ContactViewModel()
            {
                Id = s.Id,
                Name = s.Name,
                Surname = s.Surname,
                Fullname = s.Fullname,
                EmailAddress = s.EmailAddress,
                NoOfClients = s.NoOfClients,
                CreatedAt = s.CreatedAt,
                DeletedAt = s.DeletedAt,
                ClientId = s.ClientId
            }).ToList(),
        };
    }


    public async Task<GenericResponse<List<ContactViewModel>>> GetUnlinkedContactsAsync(Guid clientId)
    {
        var response = await clientRepository.GetUnlinkedContactsAsync(clientId);
        return new GenericResponse<List<ContactViewModel>>
        {
            Status = response.Status,
            StatusCode = response.StatusCode,
            StatusMessage = response.StatusMessage,
            Data = !response.Data.Any() ? new List<ContactViewModel>() : response.Data.Select(s => new ContactViewModel()
            {
                Id = s.Id,
                Name = s.Name,
                Surname = s.Surname,
                Fullname = s.Fullname,
                EmailAddress = s.EmailAddress,
                NoOfClients = s.NoOfClients,
                CreatedAt = s.CreatedAt,
                DeletedAt = s.DeletedAt,
            }).ToList(),
        };
    }


    public async Task<GenericResponse> LinkContactAsync(Guid clientId, Guid contactId)
    {
        var response = await clientRepository.LinkContactAsync(clientId, contactId);
        return new GenericResponse()
        {
            Status = response.Status,
            StatusCode = response.StatusCode,
            StatusMessage = response.StatusMessage
        };
    }


    public async Task<GenericResponse> DeLinkContactAsync(Guid clientId, Guid contactId)
    {
        var response = await clientRepository.DeLinkContactAsync(clientId, contactId);
        return new GenericResponse()
        {
            Status = response.Status,
            StatusCode = response.StatusCode,
            StatusMessage = response.StatusMessage
        };
    }

    #endregion
}
