using PasswordManager.ui.Extensions;
using ReactiveUI;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PasswordManager.ui
{
    public class ViewModelBase : ReactiveObject, IDisposable
    {
        private bool _isDisposed;

        protected IList<IDisposable> _disposables = new List<IDisposable>();
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _disposables.DisposeAll();
                }
                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}