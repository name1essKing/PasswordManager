using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Client.Services
{
    public class PasswordSettingsService : IPasswordSettingsService
    {
        /// <inheritdoc/>
        public int PasswordLength { get; set; } = 8;
        /// <inheritdoc/>
        public int PasswordsCount { get; set; } = 1;
        /// <inheritdoc/>
        public bool UseDigit { get; set; } = true;
        /// <inheritdoc/>
        public bool UseSymbols { get; set; } = true;
        /// <inheritdoc/>
        public bool UseUpperCase { get; set; } = true;
    }
}
