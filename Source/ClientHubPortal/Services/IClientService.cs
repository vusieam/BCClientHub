using ClientHubPortal.Models;

namespace ClientHubPortal.Services;

public interface IClientService
{

    #region ----------------- Clients Section -----------------

    Task<GenericResponse> CreateClientAsync(ClientViewModel model);
    Task<GenericResponse<List<ClientViewModel>>> GetClientsAsync();

    #endregion



    #region ----------------- Contacts Section -----------------

    Task<GenericResponse> CreateContactAsync(ContactViewModel model);
    Task<GenericResponse<List<ContactViewModel>>> GetContactsAsync();
    Task<GenericResponse<List<ContactViewModel>>> GetClientContactsAsync(Guid clientId);
    Task<GenericResponse<List<ContactViewModel>>> GetUnlinkedContactsAsync(Guid clientId);
    Task<GenericResponse> LinkContactAsync(Guid clientId, Guid contactId);
    Task<GenericResponse> DeLinkContactAsync(Guid clientId, Guid contactId);

    #endregion
}
