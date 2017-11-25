using System;
using System.Linq;
using MoviesMobileApp.ViewModel;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace MoviesMobileApp.Pages
{
    public partial class MoviesMobileAppPage : ContentPage
    {
        public MoviesMobileAppPage()
        {
            InitializeComponent();
            ViewModel.FocusSearchBar = FocusSearchBar;
            ViewModel.UnFocusSearchBar = UnFocusSearchBar;
            NavigationPage.SetBackButtonTitle(this, string.Empty);
        }

        public MoviesMobileAppViewModel ViewModel => BindingContext as MoviesMobileAppViewModel;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CrossConnectivity.Current.ConnectivityChanged += ViewModel.ConnectivityChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CrossConnectivity.Current.ConnectivityChanged -= ViewModel.ConnectivityChanged;
        }

        void Handle_ItemAppearing(object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
        {
            if (e.Item == ViewModel.Feeds.LastOrDefault())
            {
                ViewModel.RequestMoreData();
            }
        }

        void FocusSearchBar()
        {
            _searchBar.Focus();
        }

        void UnFocusSearchBar()
        {
            _searchBar.Unfocus();
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
            var feedVM = e.Item as FeedViewModel;
            if (feedVM != null)
            {
                feedVM.SendItemTapped();
            }
        }
    }
}
