using Infrastructure.Persistence.EFC.Contexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Tests.Fixtures;

public sealed class SqlitePersistenceFixture : IAsyncLifetime
{
    private SqliteConnection? _conn;
    public DbContextOptions<SqliteContext> Options { get; private set; } = default!;
    public SqliteContext CreateContext() => new(Options);

    public async Task DisposeAsync()
    {
        if (_conn is not null)
        {
            await _conn.CloseAsync();
            await _conn.DisposeAsync();
        }
    }

    public async Task InitializeAsync()
    {
        _conn = new SqliteConnection("Data Source=:memory:;");
        await _conn.OpenAsync();

        Options = new DbContextOptionsBuilder<SqliteContext>()
            .UseSqlite(_conn)
            .Options;

        await using var context = new SqliteContext(Options);
        await context.Database.EnsureCreatedAsync();    
    }
}

public sealed class SqlitePersistenceCollection : ICollectionFixture<SqlitePersistenceFixture>
{
    public const string Name = "SqlitePersistence";
}