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


    #endregion
}
