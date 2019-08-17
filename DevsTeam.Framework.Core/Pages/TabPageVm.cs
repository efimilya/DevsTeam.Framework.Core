using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace DevsTeam.Framework.Core.Pages
{
    public class TabPageVm : ReactiveObject, IPage
    {
        public ObservableCollection<IPage> Pages { get; }

        [Reactive]
        public IPage SelectedPage { get; set; }

        [Reactive]
        public INavigation Navigation { get; set; }

        public TPage SelectPage<TPage>()
            where TPage : class, IPage
        {
            var page = Pages.FirstOrDefault(p => p.GetType() == typeof(TPage));
            return (TPage) page;
        }

        public void AddPages(params IPage[] pages)
        {
            pages.Foreach(p => Pages.Add(p));
        }

        public bool IsSelected<TPage>()
            where TPage : IPage
        {
            return SelectedPage.GetType() == typeof(TPage);
        }
        
        public bool IsSelected(IPage page)
        {
            return SelectedPage == page;
        }
        
        public void SelectPage(IPage page)
        {
            SelectedPage = page;
        }
    }
}