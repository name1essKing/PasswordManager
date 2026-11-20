using PasswordManager.Server.Data;

namespace PasswordManager.Server.UnitOfWork;

/// <summary>
/// Реализация паттерна Unit of Work поверх <see cref="AppDbContext"/>.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _dbContext.SaveChangesAsync(cancellationToken);
}