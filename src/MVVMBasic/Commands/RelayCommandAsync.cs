using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMBasic.Commands
{
    public class RelayCommandAsync : RelayCommandAsync<object>
    {
        public RelayCommandAsync(Func<object, Task> execute)
            : this(execute, null)
        {
        }

        public RelayCommandAsync(Func<object, Task> asyncExecute,
                                Action<Exception> exceptionWrapper)
            : base(asyncExecute, exceptionWrapper)
        {
        }
    }

    public class RelayCommandAsync<T> : ICommand
    {
        private Action<Exception> _exceptionWrapper;
        protected Func<T, Task> _asyncExecute;

        public event EventHandler CanExecuteChanged;

        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public RelayCommandAsync(Func<T, Task> execute)
            : this(execute, null)
        {
        }

        public RelayCommandAsync(Func<T, Task> asyncExecute,
                            Action<Exception> exceptionWrapper)
        {
            _asyncExecute = asyncExecute;
            _exceptionWrapper = exceptionWrapper;
            IsEnabled = true;
        }

        public bool CanExecute(object parameter)
        {
            return IsEnabled;
        }

        public async void Execute(object parameter)
        {
            if (_exceptionWrapper != null)
            {
                try
                {
                    await ExecuteAsync((T)parameter);
                }
                catch (Exception ex)
                {
                    _exceptionWrapper(ex);
                }

                return;
            }

            await ExecuteAsync((T)parameter);
        }

        protected virtual async Task ExecuteAsync(T parameter)
        {
            await _asyncExecute(parameter);
        }
    }
}
