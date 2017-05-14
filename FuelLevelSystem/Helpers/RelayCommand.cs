using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FuelLevelSystem.Helpers
{
    //public class RelayCommand : ICommand
    //{
    //    Action _TargetExecuteMethod;
    //    Func<bool> _TargetCanExecuteMethod;

    //    public RelayCommand(Action executeMethod)
    //    {
    //        _TargetExecuteMethod = executeMethod;
    //    }

    //    public RelayCommand(Action executeMethod, Func<bool> canExecuteMethod)
    //    {
    //        _TargetExecuteMethod = executeMethod;
    //        _TargetCanExecuteMethod = canExecuteMethod;
    //    }

    //    public void RaiseCanExecuteChanged()
    //    {
    //        CanExecuteChanged(this, EventArgs.Empty);
    //    }
    //    #region ICommand Members

    //    bool ICommand.CanExecute(object parameter)
    //    {
    //        if (_TargetCanExecuteMethod != null)
    //        {
    //            return _TargetCanExecuteMethod();
    //        }
    //        if (_TargetExecuteMethod != null)
    //        {
    //            return true;
    //        }
    //        return false;
    //    }

    //    public event EventHandler CanExecuteChanged = delegate { };

    //    void ICommand.Execute(object parameter)
    //    {
    //        if (_TargetExecuteMethod != null)
    //        {
    //            _TargetExecuteMethod();
    //        }
    //    }
    //    #endregion
    //}

    //public class RelayCommand<T> : ICommand
    //{
    //    Action<T> _TargetExecuteMethod;
    //    Func<T, bool> _TargetCanExecuteMethod;

    //    public RelayCommand(Action<T> executeMethod)
    //    {
    //        _TargetExecuteMethod = executeMethod;
    //    }

    //    public RelayCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
    //    {
    //        _TargetExecuteMethod = executeMethod;
    //        _TargetCanExecuteMethod = canExecuteMethod;
    //    }

    //    public void RaiseCanExecuteChanged()
    //    {
    //        CanExecuteChanged(this, EventArgs.Empty);
    //    }
    //    #region ICommand Members

    //    bool ICommand.CanExecute(object parameter)
    //    {
    //        if (_TargetCanExecuteMethod != null)
    //        {
    //            T tparm = (T)parameter;
    //            return _TargetCanExecuteMethod(tparm);
    //        }
    //        if (_TargetExecuteMethod != null)
    //        {
    //            return true;
    //        }
    //        return false;
    //    }

    //    public event EventHandler CanExecuteChanged = delegate { };

    //    void ICommand.Execute(object parameter)
    //    {
    //        if (_TargetExecuteMethod != null)
    //        {
    //            _TargetExecuteMethod((T)parameter);
    //        }
    //    }
    //    #endregion
    //}

    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }
    public class RelayCommand<T> : ICommand
    {
        #region Fields

        readonly Action<T> _execute = null;
        readonly Predicate<T> _canExecute = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateCommand{T}"/>.
        /// </summary>
        /// <param name="execute">Delegate to execute when Execute is called on the command.  This can be null to just hook up a CanExecute delegate.</param>
        /// <remarks><seealso cref="CanExecute"/> will always return true.</remarks>
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand Members

        ///<summary>
        ///Defines the method that determines whether the command can execute in its current state.
        ///</summary>
        ///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        ///<returns>
        ///true if this command can be executed; otherwise, false.
        ///</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

        ///<summary>
        ///Occurs when changes occur that affect whether or not the command should execute.
        ///</summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        ///<summary>
        ///Defines the method to be called when the command is invoked.
        ///</summary>
        ///<param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        #endregion
    }

}
