using System;
using System.Linq;
using System.Threading.Tasks;
using MoviesMobileApp.Helpers;
using MoviesMobileApp.Service.Contracts;
using MoviesMobileApp.Service.Models;
using MvvmHelpers;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MoviesMobileApp.ViewModel
{
    public class MoviesMobileAppViewModel : BaseViewModel
    {
        readonly MoviesClient _moviesClient = new MoviesClient();
        readonly ConfigClient _configClient = new ConfigClient();

        bool _isSearchBarVisible;

        public MoviesMobileAppViewModel()
        {
            Title = "The Movie DB";
            Feeds = new ObservableRangeCollection<FeedViewModel>();
            SearchIconCommand = new Command(InvokeSearchIconCommand);
            RequestMoreData();
        }

        public int CurrentPage { get; private set; } = 0;

        public bool AllItemsLoaded { get; private set; }

        public ObservableRangeCollection<FeedViewModel> Feeds { get; }

        public Command SearchIconCommand { get; }

        public bool IsSearchBarVisible
        {
            get => _isSearchBarVisible;
            set => SetProperty(ref _isSearchBarVisible, value);
        }

        public Action FocusSearchBar { get; set; }

        public Action UnFocusSearchBar { get; set; }

        public async void RequestMoreData()
        {
            await LoadNextPageAsync();
        }

        void InvokeSearchIconCommand()
        {
            IsSearchBarVisible = !IsSearchBarVisible;
            if (IsSearchBarVisible)
            {
                FocusSearchBar?.Invoke();
            }
            else
            {
                UnFocusSearchBar?.Invoke();
            }
        }

        async Task LoadNextPageAsync()
        {
            if (!IsBusy && !AllItemsLoaded)
            {
                IsBusy = true;
                CurrentPage++;
                await DownloadConfigIfNeededAsync();
                var response = await _moviesClient.GetUpcomingFeedsAsync(CurrentPage);
                if (response.Result != null)
                {
                    Feeds.AddRange(response.Result.Results.Select(feed => new FeedViewModel(feed)));
                    if (CurrentPage == response.Result.TotalPages)
                    {
                        AllItemsLoaded = true;
                    }
                }
                else
                {
                    CurrentPage--;
                }
                IsBusy = false;
            }
        }

        async Task DownloadConfigIfNeededAsync()
        {
            if (Settings.Configuration == null)
            {
                var response = await _configClient.GetServiceConfigAsync();
                if (response.Result != null)
                {
                    Settings.Configuration = response.Result;
                }
            }
            if (Settings.GenresData == null)
            {
                var response = await _configClient.GetGenresAsync();
                if (response.Result != null)
                {
                    Settings.GenresData = response.Result;
                }
            }
        }
    }
}
