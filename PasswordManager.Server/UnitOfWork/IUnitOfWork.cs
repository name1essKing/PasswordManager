namespace PasswordManager.Server.UnitOfWork;

/// <summary>
/// Координирует сохранение изменений в базе данных в рамках единой транзакции.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Асинхронно фиксирует все накопленные изменения в базе данных.
    /// </summary>
    /// <returns>Количество затронутых записей в базе данных.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}