using System;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace ActimaUAT.Common
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PeriodicContainer : IDisposable
    {
        private readonly CompositeDisposable _lifetime;
        private readonly Func<PeriodicOperation> _periodicOperationFactory;
        private volatile bool _isSuspended;

        public PeriodicContainer(CompositeDisposable lifetime, Func<PeriodicOperation> periodicOperationFactory)
        {
            _lifetime = lifetime;
            _periodicOperationFactory = periodicOperationFactory;
        }

        public void Dispose() => _lifetime.Dispose();

        public IDisposable Register(TimeSpan interval, Func<Task> action, bool performOnMainThread)
        {
            var subscription = _periodicOperationFactory().Start(interval, action, performOnMainThread, () => _isSuspended);
            _lifetime.Add(subscription);
            return subscription;
        }

        public void Resume() => _isSuspended = false;

        public void Suspend() => _isSuspended = true;
    }
}