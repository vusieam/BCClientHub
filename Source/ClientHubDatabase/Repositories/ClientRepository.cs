using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Net;

namespace ClientHubDatabase.Repositories;

public class ClientRepository : IClientRepository
{
    #region -- protected properties --
    protected readonly ILogger<ClientRepository> logger;
    protected readonly ClientHubDbContext database;
    #endregion -- protected properties --

    public ClientRepository(ILogger<ClientRepository> logger, ClientHubDbContext database)
    {
        this.logger = logger;
        this.database = database;
    }



    #region ----------------- Public Database Operations -----------------

    public async Task<GenericResponse> CreateClientAsync(Clients model)
    {
        GenericResponse? response = new GenericResponse();
        try
        {
            using (var db = database.CreateSqlConnection())
            {
                await db.OpenAsync();
                using (SqlTransaction sqltrans = db.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        string query = "dbo.sp_CreateClient";
                        var parameters = new
                        {
                            name = model.Name
                        };
                        response = await db.QueryFirstOrDefaultAsync<GenericResponse>(sql: query,
                            param: parameters,
                            commandType: CommandType.StoredProcedure,
                            commandTimeout: 360,
                            transaction: sqltrans);
                        await sqltrans.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await sqltrans.RollbackAsync();
                        await db.CloseAsync();
                        throw ex;
                    }
                }
                await db.CloseAsync();
            }
        }
        catch (Exception ex)
        {
            response.Status = false;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.StatusMessage = $"{ex.Message} <br/> {ex.StackTrace}";
        }
        return response;
    }




    public async Task<GenericResponse> DeleteClientAsync(Guid clientId)
    {
        GenericResponse? response = new GenericResponse()
        {
            Status = true,
            StatusCode = (int)HttpStatusCode.OK,
            StatusMessage = "Client deleted successfully"
        };
        try
        {
            using (var db = database.CreateSqlConnection())
            {
                await db.OpenAsync();
                using (SqlTransaction sqltrans = db.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        string query = "UPDATE T SET T.DeletedAt = GETDATE() FROM dbo.[Clients] T WITH(NOLOCK) WHERE T.Id = @Id";
                        var parameters = new
                        {
                            Id = clientId
                        };
                        await db.QueryFirstOrDefaultAsync<GenericResponse>(sql: query,
                            param: parameters,
                            commandType: CommandType.Text,
                            commandTimeout: 360,
                            transaction: sqltrans);
                        await sqltrans.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await sqltrans.RollbackAsync();
                        await db.CloseAsync();
                        throw ex;
                    }
                }
                await db.CloseAsync();
            }
        }
        catch (Exception ex)
        {
            response.Status = false;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.StatusMessage = $"{ex.Message} <br/> {ex.StackTrace}";
        }
        return response;
    }

    public async Task<GenericResponse<IEnumerable<Clients>>> GetClientsAsync()
    {
        GenericResponse<IEnumerable<Clients>> response = new GenericResponse<IEnumerable<Clients>>();
        try
        {
            using (var db = database.CreateSqlConnection())
            {
                await db.OpenAsync();
                using (SqlTransaction sqltrans = db.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        string query = "dbo.sp_GetClients";
                        var clients = await db.QueryAsync<Clients>(sql: query,
                            commandType: CommandType.StoredProcedure,
                            commandTimeout: 360,
                            transaction: sqltrans);
                        await sqltrans.CommitAsync();

                        response.Status = (clients.Any());
                        response.StatusCode = (clients.Any()) ? 200 : 404;
                        response.Data = clients;

                    }
                    catch (Exception ex)
                    {
                        await sqltrans.RollbackAsync();
                        await db.CloseAsync();
                        throw ex;
                    }
                }
                await db.CloseAsync();
            }
        }
        catch (Exception ex)
        {
            response.Status = false;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.StatusMessage = $"{ex.Message} <br/> {ex.StackTrace}";
        }
        return response;
    }





    public async Task<GenericResponse> CreateContactAsync(Contacts model)
    {
        GenericResponse? response = new GenericResponse();
        try
        {
            using (var db = database.CreateSqlConnection())
            {
                await db.OpenAsync();
                using (SqlTransaction sqltrans = db.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        string query = "dbo.sp_CreateContact";
                        var parameters = new
                        {
                            id = model.Id == Guid.Empty ? Guid.NewGuid() : model.Id,
                            name = model.Name,
                            surname = model.Surname,
                            emailAddress = model.EmailAddress,
                        };
                        response = await db.QueryFirstOrDefaultAsync<GenericResponse>(sql: query,
                            param: parameters,
                            commandType: CommandType.StoredProcedure,
                            commandTimeout: 360,
                            transaction: sqltrans);
                        await sqltrans.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await sqltrans.RollbackAsync();
                        await db.CloseAsync();
                        throw ex;
                    }
                }
                await db.CloseAsync();
            }
        }
        catch (Exception ex)
        {
            response.Status = false;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.StatusMessage = $"{ex.Message} <br/> {ex.StackTrace}";
        }
        return response;
    }


    public async Task<GenericResponse<IEnumerable<Contacts>>> GetContactsAsync()
    {
        GenericResponse<IEnumerable<Contacts>> response = new GenericResponse<IEnumerable<Contacts>>();
        try
        {
            using (var db = database.CreateSqlConnection())
            {
                await db.OpenAsync();
                using (SqlTransaction sqltrans = db.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        string query = "dbo.sp_GetContacts";
                        var contacts = await db.QueryAsync<Contacts>(sql: query,
                            commandType: CommandType.StoredProcedure,
                            commandTimeout: 360,
                            transaction: sqltrans);
                        await sqltrans.CommitAsync();

                        response.Status = (contacts.Any());
                        response.StatusCode = (contacts.Any()) ? 200 : 404;
                        response.Data = contacts;
                    }
                    catch (Exception ex)
                    {
                        await sqltrans.RollbackAsync();
                        await db.CloseAsync();
                        throw ex;
                    }
                }
                await db.CloseAsync();
            }
        }
        catch (Exception ex)
        {
            response.Status = false;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.StatusMessage = $"{ex.Message} <br/> {ex.StackTrace}";
        }
        return response;
    }

    public async Task<GenericResponse<IEnumerable<Contacts>>> GetClientContactsAsync(Guid clientId)
    {
        GenericResponse<IEnumerable<Contacts>> response = new GenericResponse<IEnumerable<Contacts>>();
        try
        {
            using (var db = database.CreateSqlConnection())
            {
                await db.OpenAsync();
                using (SqlTransaction sqltrans = db.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        string query = "dbo.sp_GetClientContacts";
                        var parameters = new
                        {
                            clientId = clientId
                        };
                        var contacts = await db.QueryAsync<Contacts>(sql: query,
                            param: parameters,
                            commandType: CommandType.StoredProcedure,
                            commandTimeout: 360,
                            transaction: sqltrans);
                        await sqltrans.CommitAsync();

                        response.Status = (contacts.Any());
                        response.StatusCode = (contacts.Any()) ? 200 : 404;
                        response.Data = contacts;
                    }
                    catch (Exception ex)
                    {
                        await sqltrans.RollbackAsync();
                        await db.CloseAsync();
                        throw ex;
                    }
                }
                await db.CloseAsync();
            }
        }
        catch (Exception ex)
        {
            response.Status = false;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.StatusMessage = $"{ex.Message} <br/> {ex.StackTrace}";
        }
        return response;
    }




    public async Task<GenericResponse<IEnumerable<Contacts>>> GetUnlinkedContactsAsync(Guid clientId)
    {
        GenericResponse<IEnumerable<Contacts>> response = new GenericResponse<IEnumerable<Contacts>>();
        try
        {
            using (var db = database.CreateSqlConnection())
            {
                await db.OpenAsync();
                using (SqlTransaction sqltrans = db.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        string query = "dbo.sp_GetUnlinkedContacts";
                        var parameters = new
                        {
                            clientId = clientId
                        };
                        var contacts = await db.QueryAsync<Contacts>(sql: query,
                            param: parameters,
                            commandType: CommandType.StoredProcedure,
                            commandTimeout: 360,
                            transaction: sqltrans);
                        await sqltrans.CommitAsync();

                        response.Status = (contacts.Any());
                        response.StatusCode = (contacts.Any()) ? 200 : 404;
                        response.Data = contacts;
                    }
                    catch (Exception ex)
                    {
                        await sqltrans.RollbackAsync();
                        await db.CloseAsync();
                        throw ex;
                    }
                }
                await db.CloseAsync();
            }
        }
        catch (Exception ex)
        {
            response.Status = false;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.StatusMessage = $"{ex.Message} <br/> {ex.StackTrace}";
        }
        return response;
    }



    public async Task<GenericResponse> LinkContactAsync(Guid clientId, Guid contactId)
    {
        GenericResponse response = new GenericResponse();
        try
        {
            using (var db = database.CreateSqlConnection())
            {
                await db.OpenAsync();
                using (SqlTransaction sqltrans = db.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        string query = "dbo.sp_LinkContact";
                        var parameters = new
                        {
                            clientId = clientId,
                            contactId = contactId
                        };

                        response = await db.QueryFirstOrDefaultAsync<GenericResponse>(sql: query,
                            param: parameters,
                            commandType: CommandType.StoredProcedure,
                            commandTimeout: 360,
                            transaction: sqltrans);
                        await sqltrans.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await sqltrans.RollbackAsync();
                        await db.CloseAsync();
                        throw ex;
                    }
                }
                await db.CloseAsync();
            }
        }
        catch (Exception ex)
        {
            response.Status = false;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.StatusMessage = $"{ex.Message} <br/> {ex.StackTrace}";
        }
        return response;
    }



    public async Task<GenericResponse> DeLinkContactAsync(Guid clientId, Guid contactId)
    {
        GenericResponse response = new GenericResponse();
        try
        {
            using (var db = database.CreateSqlConnection())
            {
                await db.OpenAsync();
                using (SqlTransaction sqltrans = db.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        string query = "dbo.sp_DeLinkContact";
                        var parameters = new
                        {
                            clientId = clientId,
                            contactId = contactId
                        };

                        response = await db.QueryFirstOrDefaultAsync<GenericResponse>(sql: query,
                            param: parameters,
                            commandType: CommandType.StoredProcedure,
                            commandTimeout: 360,
                            transaction: sqltrans);
                        await sqltrans.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await sqltrans.RollbackAsync();
                        await db.CloseAsync();
                        throw ex;
                    }
                }
                await db.CloseAsync();
            }
        }
        catch (Exception ex)
        {
            response.Status = false;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.StatusMessage = $"{ex.Message} <br/> {ex.StackTrace}";
        }
        return response;
    }


    #endregion
}
