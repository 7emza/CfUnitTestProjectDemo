using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfUnitTestProjectDemo.Common.Extention
{
    public static class TaskExtention
    {
        public static async void FireAndForgetAsync(this Task task , Action<Exception> exceptionHandler = null )
        {
            try
            {
                await task;
            }
            catch (Exception e)
            {
                exceptionHandler?.Invoke(e);
            }
        }
    }
}
