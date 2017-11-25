using System;
using System.Linq;
using System.Threading.Tasks;
using MoviesMobileApp.Helpers;
using MoviesMobileApp.Service.Contracts;
using MoviesMobileApp.Service.Models;
using MvvmHelpers;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MoviesMobileApp.ViewModel
{
    public class MoviesMobileAppViewModel : BaseViewModel
    {
        readonly MoviesClient _moviesClient = new MoviesClient();
        readonly ConfigClient _configClient = new ConfigClient();

        bool _isSearchBarVisible;
        bool _isConnected;
        string _searchText;
        string _currentQuery;

        public MoviesMobileAppViewModel()
        {
            Title = "The Movie DB";
            Feeds = new ObservableRangeCollection<FeedViewModel>();
            SearchIconCommand = new Command(InvokeSearchIconCommand);
            SearchCommand = new Command(InvokeSearchCommand);
            IsConnected = CrossConnectivity.Current.IsConnected;
            RequestMoreData();
        }

        public int CurrentPage { get; private set; } = 0;

        public bool AllItemsLoaded { get; private set; }

        public ObservableRangeCollection<FeedViewModel> Feeds { get; }

        public Command SearchIconCommand { get; }

        public Command SearchCommand { get; }

        public bool IsSearchBarVisible
        {
            get => _isSearchBarVisible;
            set => SetProperty(ref _isSearchBarVisible, value);
        }

        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value, onChanged: () => { OnPropertyChanged(nameof(IsNotConnected)); });
        }

        public bool IsNotConnected => !IsConnected;

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public Action FocusSearchBar { get; set; }

        public Action UnFocusSearchBar { get; set; }

        public async void RequestMoreData()
        {
            await LoadNextPageAsync();
        }

        public void ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (IsConnected = e.IsConnected)
            {
                Feeds.Clear();
                CurrentPage = 0;
                AllItemsLoaded = false;
                RequestMoreData();
            }
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
                SearchText = _currentQuery = string.Empty;
                CurrentPage = 0;
                AllItemsLoaded = false;
                RequestMoreData();
            }
        }

        void InvokeSearchCommand()
        {
            CurrentPage = 0;
            AllItemsLoaded = false;
            _currentQuery = SearchText;
            RequestMoreData();
        }

        async Task LoadNextPageAsync()
        {
            if (IsConnected)
            {
                if (!IsBusy && !AllItemsLoaded)
                {
                    try
                    {
                        IsBusy = true;
                        CurrentPage++;
                        await DownloadConfigIfNeededAsync();
                        Response<FeedCollectionData> response = null;
                        if (string.IsNullOrWhiteSpace(_currentQuery))
                        {
                            response = await _moviesClient.GetUpcomingFeedsAsync(CurrentPage);
                        }
                        else
                        {
                            response = await _moviesClient.GetFeedsByQueryAsync(_currentQuery, CurrentPage);
                        }
                        if (response.Result != null)
                        {
                            if (CurrentPage == 1)
                            {
                                Feeds.Clear();
                            }
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
                    }
                    catch
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Semething went wrong", "Ok");
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                }
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
