namespace PasswordManager.Server.Models;

public class PasswordItem
{
    /// <summary>
    /// EF Core автоматически распознает поле Id как первичный ключ
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название сервиса или сайта (например, "GitHub", "Yandex", "VK")
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Логин или Email
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Зашифрованный/сохраненный пароль
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Дата и время создания записи
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}