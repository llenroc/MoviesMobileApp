using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoviesMobileApp.ViewModel;
using MoviesMobileApp.Views;
using Xamarin.Forms;

namespace MoviesMobileApp.Pages
{
    public partial class FeedDetailsPage : ContentPage
    {
        public FeedDetailsPage(FeedViewModel feedVM)
        {
            BindingContext = feedVM;
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, string.Empty);
        }

        void Handle_SelectedItemChanged(object sender, MoviesMobileApp.Views.SelectionTapItemEventArgs args)
        {
            var feedVM = args.SelectedItem as FeedViewModel;
            if (feedVM != null)
            {
                feedVM.SendItemTapped();
            }
        }
    }
}
