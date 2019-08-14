using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevsTeam.Framework.Core.Async;
using ReactiveUI;

namespace DevsTeam.Framework.Core.Periodic
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class PeriodicContainer : IDisposable
    {
        private readonly CompositeDisposable _lifetime;
        private volatile bool _isSuspended;

        public PeriodicContainer() => _lifetime = new CompositeDisposable();

        public void Dispose() => _lifetime.Dispose();

        public IDisposable Register(TimeSpan interval, Func<Task> action, bool performOnMainThread)
        {
            var subscription = new PeriodicOperation().Start(interval, action, performOnMainThread, () => _isSuspended);
            _lifetime.Add(subscription);
            return subscription;
        }

        public void Resume() => _isSuspended = false;

        public void Suspend() => _isSuspended = true;

        private class PeriodicOperation
        {
            private int _isPerforming;

            public IDisposable Start(TimeSpan interval, Func<Task> action, bool performOnMainThread, Func<bool> isSuspended)
            {
                var observable = Observable.Interval(interval).Where(_ => !isSuspended());
                if (performOnMainThread) observable = observable.ObserveOn(RxApp.MainThreadScheduler);
                return observable.Subscribe(_ => Perform(action));
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
}