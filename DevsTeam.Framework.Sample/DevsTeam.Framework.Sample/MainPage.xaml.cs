using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevsTeam.Framework.Core.Async;
using Xamarin.Forms;

namespace DevsTeam.Framework.Sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            T().DoNotWait();
        }

        private async Task T()
        {
            throw new NotSupportedException("s;kla;sldkfj");
        }
    }
}