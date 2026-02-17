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


    public async Task<GenericResponse> CreateClientAsync(ClientViewModel model)
    {
        var client = new Clients()
        {
            Name = model.Name
        };
        return await clientRepository.CreateClientAsync(client);
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
                Name = s.Name,
                Id = s.Id,
                NameCode = s.NameCode,
                NoOfContacts = s.NoOfContacts,
                CreatedAt = s.CreatedAt,
                DeletedAt = s.DeletedAt,
            }).ToList(),
        };
    }
}
