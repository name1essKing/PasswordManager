using Microsoft.EntityFrameworkCore;
using PasswordManager.Server.Data;
using PasswordManager.Server.Models;
using PasswordManager.Server.Services.API.Interfaces;

namespace PasswordManager.Server.Services.API;

/// <summary>
/// Реализация репозитория для работы с сущностями <see cref="PasswordItem"/> через EF Core.
/// </summary>
public class PasswordsRepository : IPasswordsRepository
{
    private readonly AppDbContext _dbContext;

    public PasswordsRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public Task<List<PasswordItem>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _dbContext.Passwords
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    /// <inheritdoc />
    public Task<PasswordItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _dbContext.Passwords
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    /// <inheritdoc />
    public Task AddAsync(PasswordItem item, CancellationToken cancellationToken = default) =>
        _dbContext.Passwords.AddAsync(item, cancellationToken).AsTask();

    /// <inheritdoc />
    public Task AddRangeAsync(IEnumerable<PasswordItem> items, CancellationToken cancellationToken = default) =>
        _dbContext.Passwords.AddRangeAsync(items, cancellationToken);

    /// <inheritdoc />
    public void Update(PasswordItem item) =>
        _dbContext.Passwords.Update(item);

    /// <inheritdoc />
    public void Delete(PasswordItem item) =>
        _dbContext.Passwords.Remove(item);
}