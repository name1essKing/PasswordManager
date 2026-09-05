using System;
using System.Collections.Generic;
namespace PasswordManager.Client.Services
{
    /// <summary>
    /// Реализация сервиса передачи событий сгенерированных паролей.
    /// </summary>
    public class PasswordEventService : IPasswordEventService
    {
        /// <inheritdoc/>
        public event Action<List<string>>? PasswordsGenerated;
        
        /// <inheritdoc/>
        public void NotifyPasswordsGenerated(List<string> passwords)
        {
            PasswordsGenerated?.Invoke(passwords);
        }
    }
}
