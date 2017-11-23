using System;
using System.Linq;
using MoviesMobileApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MoviesMobileApp.Pages
{
    public partial class MoviesMobileAppPage : ContentPage
    {
        public MoviesMobileAppPage()
        {
            InitializeComponent();
            ViewModel.FocusSearchBar = FocusSearchBar;
            ViewModel.UnFocusSearchBar = UnFocusSearchBar;
        }

        public MoviesMobileAppViewModel ViewModel => BindingContext as MoviesMobileAppViewModel;

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
            if(feedVM != null)
            {
                feedVM.SendItemTapped();
            }
        }
    }
}
