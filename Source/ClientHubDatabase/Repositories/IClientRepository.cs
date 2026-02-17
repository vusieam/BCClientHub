namespace ClientHubDatabase.Repositories;

public interface IClientRepository
{
    Task<GenericResponse> CreateClientAsync(Clients model);
    Task<GenericResponse<IEnumerable<Clients>>> GetClientsAsync();
}
