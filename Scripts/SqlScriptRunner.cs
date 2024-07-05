using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

public class SqlScriptRunner : IHostedService
{
    private readonly IDbConnection _dbConnection;
    private readonly string _scriptsPath;

    public SqlScriptRunner(IDbConnection dbConnection, IConfiguration configuration)
    {
        _dbConnection = dbConnection;
        _scriptsPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "SQL");
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var carrinhosScript = await File.ReadAllTextAsync(Path.Combine(_scriptsPath, "CreateCarrinhosTable.sql"));
        var carrinhoItensScript = await File.ReadAllTextAsync(Path.Combine(_scriptsPath, "CreateCarrinhoItensTable.sql"));

        await _dbConnection.ExecuteAsync(carrinhosScript);
        await _dbConnection.ExecuteAsync(carrinhoItensScript);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
