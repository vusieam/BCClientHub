using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ClientHubDatabase;

public class ClientHubDbContext
{

    #region -- protected readonly properties --
    protected readonly IConfiguration config;
    #endregion

    public ClientHubDbContext(IConfiguration config) => this.config = config;


    #region -- protected readonly properties --

    public SqlConnection CreateSqlConnection()
    {
        var connectionString = config.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Database connection string not configured in the configuration file.");
        }
        return new SqlConnection(connectionString);
    }


    /// <summary>
    /// Creating database if it doesnot exists.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task CreateDatabaseAsync()
    {
        var connectionString = config.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Database connection string not configured in the configuration file.");
        }

        var builder = new SqlConnectionStringBuilder(connectionString);
        var databaseName = builder.InitialCatalog;

        // Build connection to master
        var masterConnectionString = connectionString.Replace(databaseName, "master");
        using (var connection = new SqlConnection(masterConnectionString))
        {
            await connection.OpenAsync();
            var checkDbExistsCmd = $@"
            IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{databaseName}')
            BEGIN
                CREATE DATABASE [{databaseName}]
            END";

            using (var command = new SqlCommand(checkDbExistsCmd, connection))
            {
                command.ExecuteNonQuery();
            }
            await connection.CloseAsync();
        }
    }


    /// <summary>
    /// Enabling database snapshot isolation
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task EnableSnapshotIsolation()
    {
        var connectionString = config.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Database connection string not configured in the configuration file.");
        }

        var builder = new SqlConnectionStringBuilder(connectionString);
        var databaseName = builder.InitialCatalog;

        // Build connection to master
        var masterConnectionString = connectionString.Replace(databaseName, "master");
        using (var connection = new SqlConnection(masterConnectionString))
        {
            await connection.OpenAsync();
            var checkDbExistsCmd = $@"
            IF NOT EXISTS (SELECT 1 name FROM sys.databases WHERE name = N'{databaseName}' AND snapshot_isolation_state_desc = 'ON')
            BEGIN
                PRINT('Enabling ALLOW_SNAPSHOT_ISOLATION...');
                ALTER DATABASE [{databaseName}] SET ALLOW_SNAPSHOT_ISOLATION ON;
            END";

            using (var command = new SqlCommand(checkDbExistsCmd, connection))
            {
                command.ExecuteNonQuery();
            }
            await connection.CloseAsync();
        }
    }


    /// <summary>
    /// Enabling readonly commited snapshot to prevent dirty reads.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task EnableReadCommittedSnapshot()
    {
        var connectionString = config.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Database connection string not configured in the configuration file.");
        }

        var builder = new SqlConnectionStringBuilder(connectionString);
        var databaseName = builder.InitialCatalog;

        // Build connection to master
        var masterConnectionString = connectionString.Replace(databaseName, "master");
        using (var connection = new SqlConnection(masterConnectionString))
        {
            await connection.OpenAsync();
            var checkDbExistsCmd = $@"
            IF NOT EXISTS (SELECT 1 name FROM sys.databases WHERE name = N'{databaseName}' AND is_read_committed_snapshot_on =1)
            BEGIN
                PRINT('Enabling READ_COMMITTED_SNAPSHOT...');
                ALTER DATABASE [{databaseName}] SET READ_COMMITTED_SNAPSHOT ON;
            END";

            using (var command = new SqlCommand(checkDbExistsCmd, connection))
            {
                command.ExecuteNonQuery();
            }
            await connection.CloseAsync();
        }
    }

    #endregion


}
