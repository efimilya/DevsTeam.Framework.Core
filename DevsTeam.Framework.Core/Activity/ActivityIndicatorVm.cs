using System;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace DevsTeam.Framework.Core.Activity
{
    public class ActivityIndicatorVm : ReactiveObject, IDisposable
    {
        [Reactive]
        public bool IsRunning { get; private set; }

        [Reactive]
        public string Description { get; private set; }

        public void Dispose()
        {
            Description = "";
            IsRunning = false;
        }

        public IDisposable StartActivity(string description = "")
        {
            Description = description;
            IsRunning = true;
            return this;
        }
    }
}