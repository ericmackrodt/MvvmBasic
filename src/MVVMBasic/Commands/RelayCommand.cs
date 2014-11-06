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
    }

    public class RelayCommand<T> : ICommand
    {
        private Action<T> _handler;
        public RelayCommand(Action<T> handler)
        {
            _handler = handler;
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
            _handler((T)parameter);
        }
    }
}
