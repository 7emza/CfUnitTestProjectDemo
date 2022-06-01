using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfUnitTestProjectDemo.Command
{
    public class DelegateCommand : CommandBase
    {
        private Action executeMethod;

        private bool canExecute;
        private DelegateCommand addMember;

        public Action ExecuteMethod { get => executeMethod; set => executeMethod = value; }
        public bool CanSubmited { get => canExecute; set => canExecute = value; }

        public DelegateCommand(Action executeMethod)
        {
            this.ExecuteMethod = executeMethod;
            this.CanSubmited = canExecute;
        }


        public override void Execute(object parameter)
        {
            ExecuteMethod();
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }


    }
}