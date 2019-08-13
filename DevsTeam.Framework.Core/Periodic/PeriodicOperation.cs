using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevsTeam.Framework.Core.Async;
using ReactiveUI;

namespace DevsTeam.Framework.Core.Periodic
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PeriodicOperation
    {
        private int _isPerforming;

        public IDisposable Start(TimeSpan interval, Func<Task> action, bool performOnMainThread, Func<bool> isSuspended)
        {
            var observable = Observable.Interval(interval).Where(_ => !isSuspended());
            if (performOnMainThread) observable = observable.ObserveOn(RxApp.MainThreadScheduler);
            return observable.Subscribe(_ => Perform(action).DoNotWait());
        }

        private async Task Perform(Func<Task> action)
        {
            if (Interlocked.CompareExchange(ref _isPerforming, 1, 0) == 1) return;

            try
            {
                await action();
            }
            finally
            {
                Interlocked.Exchange(ref _isPerforming, 0);
            }
        }
    }
}