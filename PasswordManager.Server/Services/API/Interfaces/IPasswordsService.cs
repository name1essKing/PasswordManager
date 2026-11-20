using PasswordManager.Dto.DTOs;

namespace PasswordManager.Server.Services.Interfaces;

/// <summary>
/// Определяет бизнес-операции для работы с записями паролей.
/// </summary>
public interface IPasswordsService
{
    /// <summary>
    /// Возвращает список всех записей паролей в виде DTO.
    /// </summary>
    Task<List<PasswordDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает запись пароля по идентификатору или <c>null</c>, если запись не найдена.
    /// </summary>
    /// <param name="id">Идентификатор записи.</param>
    Task<PasswordDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создаёт новую запись пароля и возвращает её DTO.
    /// </summary>
    /// <param name="dto">Данные для создания записи.</param>
    Task<PasswordDto> CreateAsync(CreatePasswordDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Пакетно создаёт несколько записей паролей за одну транзакцию.
    /// </summary>
    /// <param name="dtos">Список записей для добавления.</param>
    /// <returns><c>true</c>, если записи добавлены; <c>false</c>, если передан пустой список.</returns>
    Task<bool> CreateBatchAsync(IReadOnlyCollection<CreatePasswordDto> dtos, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет запись пароля по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемой записи.</param>
    /// <returns><c>true</c>, если запись найдена и удалена; <c>false</c>, если запись не найдена.</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}