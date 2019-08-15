using System;

namespace DevsTeam.Framework.Autofac
{
    public class Disposable<T> : IDisposable
    {
        private readonly Action _disposeLifetime;

        public Disposable(T value, Action disposeLifetime)
        {
            Value = value;
            _disposeLifetime = disposeLifetime;
        }

        public T Value { get; }

        public void Dispose() => _disposeLifetime();
    }
}