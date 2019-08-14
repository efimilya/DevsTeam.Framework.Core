using System;
using System.Reactive.Disposables;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace DevsTeam.Framework.Core.Activity
{
    public class AppActivity
    {
        private readonly Func<ContentView> _indicatorViewFactory;
        private bool _isLoading;
        private AppActivityIndicator _indicator;

        public AppActivity(Func<ContentView> indicatorViewFactory)
        {
            _indicatorViewFactory = indicatorViewFactory;
        }

        public IDisposable Show()
        {
            _isLoading = true;
            _indicator = new AppActivityIndicator(){Content = _indicatorViewFactory()};
            ShowLoading(_indicator);
            return Disposable.Create(HideLoading);
        }

        private async void ShowLoading(AppActivityIndicator indicator)
        {
            if (!_isLoading) return;
            await PopupNavigation.Instance.PushAsync(indicator);
        }

        private async void HideLoading()
        {
            _isLoading = false;
            await PopupNavigation.Instance.RemovePageAsync(_indicator);
        }
    }
}