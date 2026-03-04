using Infrastructure.Persistence.EFC.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(IServiceProvider sp, IHostEnvironment env, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(sp);
        ArgumentNullException.ThrowIfNull(env);

        if (env.IsDevelopment())
        {
            using var scope = sp.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<SqliteContext>();
            await context.Database.EnsureCreatedAsync(ct);

            await SeedDefaultAsync(context, ct);
        }
        else
        {
            
        }
    }

    private static async Task SeedDefaultAsync<TContext>(TContext context, CancellationToken ct = default) where TContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(context);

        await Task.CompletedTask;
    }
}
