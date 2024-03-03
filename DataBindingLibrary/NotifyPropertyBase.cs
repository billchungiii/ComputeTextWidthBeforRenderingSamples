﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DataBindingLibrary
{
    public abstract class NotifyPropertyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(storage, value))
            {
                storage = value;
                OnPropertyChanged(propertyName);
            }
        }
    }

    public class RelayCommand : ICommand
    {
        private Action<object> _executeHandler;
        private Func<object, bool> _canExecuteHandler;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> executeHandler, Func<object, bool> canExecuteHandler)
        {
            _executeHandler = executeHandler ?? throw new ArgumentNullException("execute handler can not be null");
            _canExecuteHandler = canExecuteHandler ?? throw new ArgumentNullException("canExecute handler can not be null");
        }

        public RelayCommand(Action<object> execute) : this(execute, (x) => true)
        { }

        public bool CanExecute(object parameter)
        {
            return _canExecuteHandler(parameter);
        }

        public void Execute(object parameter)
        {
            _executeHandler(parameter);
        }


    }
}