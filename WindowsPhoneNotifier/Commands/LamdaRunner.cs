using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WindowsPhoneNotifier.Commands
{
    public class LamdaRunner : ICommand
    {
        Action _runMe;
        public LamdaRunner(Action runMe)
        {
            _runMe = runMe;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _runMe.Invoke();
        }
    }
}
