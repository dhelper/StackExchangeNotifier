using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WindowsPhoneNotifierTests
{
    public static class DispatcherExtensions
    {
        public static Task<T> InvokeTaskAsync<T>(this Dispatcher dispatcher, Func<Task<T>> func)
        {
            var tcs = new TaskCompletionSource<T>();
            dispatcher.BeginInvoke(new Action(async () =>
            {
                try
                {
                    var result = await func();
                    tcs.SetResult(result);
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            }));

            return tcs.Task;
        }

        public static Task<T> InvokeAsync<T>(this Dispatcher dispatcher, Func<T> func)
        {
            var tcs = new TaskCompletionSource<T>();
            dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    var result = func();
                    tcs.SetResult(result);
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            }));

            return tcs.Task;
        }
    }
}
