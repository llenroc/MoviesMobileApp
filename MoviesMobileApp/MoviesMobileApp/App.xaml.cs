using MoviesMobileApp.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MoviesMobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Xamarin.Forms.NavigationPage(new MoviesMobileAppPage())
            {
                BarTextColor = GetAppResource<Color>("WhiteTextColor"),
                BarBackgroundColor = GetAppResource<Color>("NavigationBarBackgroundColor")
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static T GetAppResource<T>(string key)
        {
            return (T)Current.Resources[key];
        }
    }
}
