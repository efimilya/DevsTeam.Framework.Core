using System.Threading.Tasks;
using DevsTeam.Framework.Core.Async;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevsTeam.Framework.Core.Activity
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityIndicatorSampleView
    {
        public ActivityIndicatorSampleView()
        {
            InitializeComponent();
            RotateImageContinously().DoNotWait();
        }

        private async Task RotateImageContinously()
        {
            while (true)
            {
                await Logo.RotateTo(360, 1200, Easing.CubicOut);
                Logo.Rotation = 0;
            }
        }
    }
}