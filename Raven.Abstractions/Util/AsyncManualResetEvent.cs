// -----------------------------------------------------------------------
//  <copyright file="AsyncManualResetEvent.cs" company="Hibernating Rhinos LTD">
//      Copyright (c) Hibernating Rhinos LTD. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
using System.Threading;
using System.Threading.Tasks;


namespace Raven.Abstractions.Util
{
    public class AsyncManualResetEvent
    {
        private TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

        public Task WaitAsync() { return tcs.Task; }

        public async Task<bool> WaitAsync(int timeout)
        {
            var task = tcs.Task;
            
            return await Task.WhenAny(task, Task.Delay(timeout)).ConfigureAwait(false) == task;
        }

        public async Task<bool> WaitAsync(int timeout, CancellationToken cancellationToken)
        {
            var task = tcs.Task;

            return await Task.WhenAny(task, Task.Delay(timeout, cancellationToken)).ConfigureAwait(false) == task;
        }

        public void Set() { tcs.TrySetResult(true); }

        public void Reset()
        {
            while (true)
            {
                var current = tcs;
                if (!current.Task.IsCompleted ||
                    Interlocked.CompareExchange(ref tcs, new TaskCompletionSource<bool>(), current) == current)
                    return;
            }
        }
    }
}
