using PasswordManager.Dto.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasswordManager.Client.Services;

/// <summary>
/// интерфейс для взаимодействия с API сервера для управления паролями.
/// </summary>
public interface IPasswordApiClient
{
    /// <summary>
    /// метод для получения списка паролей с сервера.
    /// </summary>
    Task<List<PasswordDto>> GetPasswordsAsync();

    /// <summary>
    /// Получает запись пароля по идентификатору или <c>null</c>, если она не найдена или произошла ошибка.
    /// </summary>
    Task<PasswordDto?> GetPasswordByIdAsync(int id);

    /// <summary>
    /// метод для создания нового пароля на сервере.
    /// </summary>
    Task<PasswordDto?> CreatePasswordAsync(CreatePasswordDto dto);

    /// <summary>
    /// метод для пакетного создания паролей на сервере.
    /// </summary>
    Task<bool> CreatePasswordsBatchAsync(List<CreatePasswordDto> dtos);

    /// <summary>
    /// Удаляет запись пароля по идентификатору.
    /// </summary>
    Task<bool> DeletePasswordAsync(int id);
}