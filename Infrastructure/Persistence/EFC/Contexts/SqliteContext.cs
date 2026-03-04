using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EFC.Contexts;

public sealed class SqliteContext(DbContextOptions<SqliteContext> options) : DbContext(options)
{
}
