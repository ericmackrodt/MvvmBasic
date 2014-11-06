﻿using System;
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
                       Predicate<object> canExecute)
            : base(asyncExecute, canExecute)
        {
        }
    }

    public class RelayCommandAsync<T> : ICommand
    {
        protected readonly Predicate<object> _canExecute;
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
                       Predicate<object> canExecute)
        {
            _asyncExecute = asyncExecute;
            _canExecute = canExecute;
            IsEnabled = true;
        }

        public bool CanExecute(object parameter)
        {
            return IsEnabled;
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync((T)parameter);
        }

        protected virtual async Task ExecuteAsync(T parameter)
        {
            await _asyncExecute(parameter);
        }
    }
}