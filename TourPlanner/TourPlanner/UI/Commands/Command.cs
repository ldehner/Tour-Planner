using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tour_planner.UI.Commands
{
    public class Command : ICommand
    {
        private Action _action;
        private bool _canExecute;

        public Command(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
           return _canExecute;
        }

        public void Execute(object? parameter)
        {
            _action();
        }
    }
}
