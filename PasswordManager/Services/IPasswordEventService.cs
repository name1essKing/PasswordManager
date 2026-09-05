using System;
using System.Collections.Generic;

namespace PasswordManager.Client.Services
{
    /// <summary>
    /// Интерфейс сервиса передачи событий сгенерированных паролей.
    /// </summary>
    public interface IPasswordEventService
    {
        /// <summary>
        /// Событие, возникающее при успешной генерации нового списка паролей.
        /// </summary>
        event Action<List<string>>? PasswordsGenerated;
        
        /// <summary>
        /// Уведомляет подписчиков о сгенерированных паролях.
        /// </summary>
        /// <param name="passwords">Список новых паролей.</param>
        void NotifyPasswordsGenerated(List<string> passwords);
    }
}