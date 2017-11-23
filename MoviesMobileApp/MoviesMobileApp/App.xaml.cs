using MoviesMobileApp.Pages;
using Xamarin.Forms;

namespace MoviesMobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MoviesMobileAppPage();
        }

        protected async override void OnStart()
        {
            // Handle when your app starts
            var result = await new Service.Contracts.MoviesClient().GetUpcomingFeedsAsync(1);
            var result1 = await new Service.Contracts.ConfigClient().GetServiceConfigAsync();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
