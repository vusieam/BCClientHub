namespace ClientHubDatabase.Repositories;

public interface IClientRepository
{
    #region ----------------- Clients Section -----------------

    Task<GenericResponse> CreateClientAsync(Clients model);
    Task<GenericResponse> DeleteClientAsync(Guid clientId);
    Task<GenericResponse<IEnumerable<Clients>>> GetClientsAsync();

    #endregion



    #region ----------------- Contacts Section -----------------
    Task<GenericResponse> CreateContactAsync(Contacts model);
    Task<GenericResponse<IEnumerable<Contacts>>> GetContactsAsync();
    Task<GenericResponse<IEnumerable<Contacts>>> GetClientContactsAsync(Guid clientId);
    Task<GenericResponse<IEnumerable<Contacts>>> GetUnlinkedContactsAsync(Guid clientId);
    Task<GenericResponse> LinkContactAsync(Guid clientId, Guid contactId);
    Task<GenericResponse> DeLinkContactAsync(Guid clientId, Guid contactId);
    #endregion
}
