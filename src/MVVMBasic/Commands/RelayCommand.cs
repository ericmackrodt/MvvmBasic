using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMBasic.Commands
{
    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action<object> handler)
            : base(handler)
        {
        }

        public RelayCommand(Action<object> handler,
                            Action<Exception> exceptionWrapper)
            : base(handler, exceptionWrapper)

        {
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private Action<T> _handler;
        private Action<Exception> _exceptionWrapper;

        public RelayCommand(Action<T> handler)
            : this (handler, null)
        {
        }

        public RelayCommand(Action<T> handler,
                            Action<Exception> exceptionWrapper)
        {
            _handler = handler;
            _exceptionWrapper = exceptionWrapper;
            IsEnabled = true;
        }

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

        public bool CanExecute(object parameter)
        {
            return IsEnabled;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (_exceptionWrapper != null)
            {
                try
                {
                    _handler((T)parameter);
                }
                catch (Exception ex)
                {
                    _exceptionWrapper(ex);
                }

                return;
            }

            _handler((T)parameter);
        }
    }
}
