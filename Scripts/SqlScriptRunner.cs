using System.Data;
using Dapper;

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
        var addCarrinhoScript = await File.ReadAllTextAsync(Path.Combine(_scriptsPath, "CreateCarrinho.sql"));

        await _dbConnection.ExecuteAsync(carrinhosScript);
        await _dbConnection.ExecuteAsync(carrinhoItensScript);
        await _dbConnection.ExecuteAsync(addCarrinhoScript);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
