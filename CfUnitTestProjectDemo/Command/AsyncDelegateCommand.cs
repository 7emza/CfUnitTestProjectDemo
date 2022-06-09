using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CfUnitTestProjectDemoUI.Command
{
    public class AsyncDelegateCommand : DelegateCommand
    {
        private readonly Func<Task> _executeAction;

        public AsyncDelegateCommand(Func<Task> executeAction) : base(async () => { await executeAction(); })
        {
            this._executeAction = executeAction;
        }

        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    IsActive = true;
                    await _executeAction();
                }
                finally
                {
                    IsActive = false;
                }
            }

            RaiseCanExecuteChanged();
        }
    }
}
