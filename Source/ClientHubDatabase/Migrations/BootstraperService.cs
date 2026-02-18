using Dapper;
using Microsoft.Extensions.Logging;

namespace ClientHubDatabase.Migrations;

public class BootstraperService
{

    #region -- protected properties --
    protected readonly ILogger<BootstraperService> logger;
    protected readonly ClientHubDbContext database;
    protected readonly SqlFileProvider sqlFileProvider;
    #endregion -- protected properties --


    public BootstraperService(ILogger<BootstraperService> logger, ClientHubDbContext database, SqlFileProvider sqlFileProvider)
    {
        this.logger = logger;
        this.database = database;
        this.sqlFileProvider = sqlFileProvider;
    }


    #region -- public function members --

    public async Task Migrations()
    {
        await database.CreateDatabaseAsync();
        await database.EnableSnapshotIsolation();
        await database.EnableReadCommittedSnapshot();
        await MigrateAllTablesAsync();
        await MigrateAllTriggersAsync();
        await MigrateAllSeedersAsync();
        await MigrateAllViewsAsync();
        await MigrateAllStoredProceduresAsync();
    }

    #endregion



    #region -- protected function members --

    private async Task MigrateAllTablesAsync()
    {
        var tables = sqlFileProvider.GetAll("tables");
        logger.LogInformation($"Starting tables migration... Found {tables.Count} file(s).");
        if (!tables.Any())
            return;
        using var db = database.CreateSqlConnection();
        await db.OpenAsync();
        tables = tables.OrderBy(o => o.Key).ToDictionary();
        foreach (var kv in tables)
        {
            var fileName = kv.Key;
            var sqlQuery = kv.Value;

            if (string.IsNullOrWhiteSpace(sqlQuery) || sqlQuery.TrimStart().StartsWith("--"))
            {
                logger.LogInformation($"[SKIP] Skipping empty or comment-only file: {fileName}");
                continue;
            }

            try
            {
                logger.LogInformation($"[EXEC] Executing {fileName}...");
                await db.ExecuteAsync(sql: sqlQuery, commandTimeout: 0, commandType: System.Data.CommandType.Text);
                logger.LogInformation($"[DONE] Successfully executed {fileName}");
            }
            catch (Exception fileEx)
            {
                logger.LogError($"[ERROR] Failed to execute {fileName}: {fileEx.Message}");
                throw new Exception($"Error executing SQL file: {fileName}", fileEx);
            }
        }
        await db.CloseAsync();
        logger.LogInformation("[SUCCESS] tables migration completed.");
    }


    private async Task MigrateAllTriggersAsync()
    {
        var triggers = sqlFileProvider.GetAll("triggers");
        logger.LogInformation($"Starting triggers migration... Found {triggers.Count} file(s).");
        if (!triggers.Any())
            return;
        using var db = database.CreateSqlConnection();
        await db.OpenAsync();
        foreach (var kv in triggers)
        {
            var fileName = kv.Key;
            var sqlQuery = kv.Value;

            if (string.IsNullOrWhiteSpace(sqlQuery) || sqlQuery.TrimStart().StartsWith("--"))
            {
                logger.LogInformation($"[SKIP] Skipping empty or comment-only file: {fileName}");
                continue;
            }

            try
            {
                logger.LogInformation($"[EXEC] Executing {fileName}...");
                await db.ExecuteAsync(sql: sqlQuery, commandTimeout: 0, commandType: System.Data.CommandType.Text);
                logger.LogInformation($"[DONE] Successfully executed {fileName}");
            }
            catch (Exception fileEx)
            {
                logger.LogError($"[ERROR] Failed to execute {fileName}: {fileEx.Message}");
                throw new Exception($"Error executing SQL file: {fileName}", fileEx);
            }
        }
        await db.CloseAsync();
        logger.LogInformation("[SUCCESS] triggers migration completed.");
    }
    

    private async Task MigrateAllSeedersAsync()
    {
        var seeders = sqlFileProvider.GetAll("seeders");
        logger.LogInformation($"Starting seeders migration... Found {seeders.Count} file(s).");
        if (!seeders.Any())
            return;
        using var db = database.CreateSqlConnection();
        await db.OpenAsync();
        foreach (var kv in seeders)
        {
            var fileName = kv.Key;
            var sqlQuery = kv.Value;

            if (string.IsNullOrWhiteSpace(sqlQuery) || sqlQuery.TrimStart().StartsWith("--"))
            {
                logger.LogInformation($"[SKIP] Skipping empty or comment-only file: {fileName}");
                continue;
            }

            try
            {
                logger.LogInformation($"[EXEC] Executing {fileName}...");
                await db.ExecuteAsync(sql: sqlQuery, commandTimeout: 0, commandType: System.Data.CommandType.Text);
                logger.LogInformation($"[DONE] Successfully executed {fileName}");
            }
            catch (Exception fileEx)
            {
                logger.LogError($"[ERROR] Failed to execute {fileName}: {fileEx.Message}");
                throw new Exception($"Error executing SQL file: {fileName}", fileEx);
            }
        }
        await db.CloseAsync();
        logger.LogInformation("[SUCCESS] seeders migration completed.");
    }



    private async Task MigrateAllViewsAsync()
    {
        var Views = sqlFileProvider.GetAll("Views");
        logger.LogInformation($"Starting Views migration... Found {Views.Count} file(s).");
        if (!Views.Any())
            return;
        using var db = database.CreateSqlConnection();
        await db.OpenAsync();
        foreach (var kv in Views)
        {
            var fileName = kv.Key;
            var sqlQuery = kv.Value;

            if (string.IsNullOrWhiteSpace(sqlQuery) || sqlQuery.TrimStart().StartsWith("--"))
            {
                logger.LogInformation($"[SKIP] Skipping empty or comment-only file: {fileName}");
                continue;
            }

            try
            {
                logger.LogInformation($"[EXEC] Executing {fileName}...");
                await db.ExecuteAsync(sql: sqlQuery, commandTimeout: 0, commandType: System.Data.CommandType.Text);
                logger.LogInformation($"[DONE] Successfully executed {fileName}");
            }
            catch (Exception fileEx)
            {
                logger.LogError($"[ERROR] Failed to execute {fileName}: {fileEx.Message}");
                throw new Exception($"Error executing SQL file: {fileName}", fileEx);
            }
        }
        await db.CloseAsync();
        logger.LogInformation("[SUCCESS] Views migration completed.");
    }


    private async Task MigrateAllStoredProceduresAsync()
    {
        var storedProcedures = sqlFileProvider.GetAll("storedProcedures");
        logger.LogInformation($"Starting storedProcedures migration... Found {storedProcedures.Count} file(s).");
        if (!storedProcedures.Any())
            return;
        using var db = database.CreateSqlConnection();
        await db.OpenAsync();
        foreach (var kv in storedProcedures)
        {
            var fileName = kv.Key;
            var sqlQuery = kv.Value;

            if (string.IsNullOrWhiteSpace(sqlQuery) || sqlQuery.TrimStart().StartsWith("--"))
            {
                logger.LogInformation($"[SKIP] Skipping empty or comment-only file: {fileName}");
                continue;
            }

            try
            {
                logger.LogInformation($"[EXEC] Executing {fileName}...");
                await db.ExecuteAsync(sql: sqlQuery, commandTimeout: 0, commandType: System.Data.CommandType.Text);
                logger.LogInformation($"[DONE] Successfully executed {fileName}");
            }
            catch (Exception fileEx)
            {
                logger.LogError($"[ERROR] Failed to execute {fileName}: {fileEx.Message}");
                throw new Exception($"Error executing SQL file: {fileName}", fileEx);
            }
        }
        await db.CloseAsync();
        logger.LogInformation("[SUCCESS] storedProcedures migration completed.");
    }


    #endregion
}
