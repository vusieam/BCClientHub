using ClientHubPortal.Models;

namespace ClientHubPortal.Services;

public interface IClientService
{
    Task<GenericResponse> CreateClientAsync(ClientViewModel model);
    Task<GenericResponse<List<ClientViewModel>>> GetClientsAsync();
}
