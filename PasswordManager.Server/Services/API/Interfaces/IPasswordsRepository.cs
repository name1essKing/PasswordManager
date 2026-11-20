using PasswordManager.Server.Models;

namespace PasswordManager.Server.Services.API.Interfaces;

/// <summary>
/// Определяет контракт для доступа к данным записей паролей в хранилище.
/// </summary>
public interface IPasswordsRepository
{
    /// <summary>
    /// Возвращает все сохранённые записи паролей.
    /// </summary>
    Task<List<PasswordItem>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Находит запись пароля по её уникальному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор записи.</param>
    Task<PasswordItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавляет новую запись пароля в контекст отслеживания.
    /// </summary>
    Task AddAsync(PasswordItem item, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавляет набор записей паролей в контекст отслеживания.
    /// </summary>
    Task AddRangeAsync(IEnumerable<PasswordItem> items, CancellationToken cancellationToken = default);

    /// <summary>
    /// Помечает существующую запись пароля как изменённую.
    /// </summary>
    void Update(PasswordItem item);

    /// <summary>
    /// Помечает запись пароля на удаление из хранилища.
    /// </summary>
    void Delete(PasswordItem item);
}