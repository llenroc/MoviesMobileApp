using System;
using System.Linq;
using System.Threading.Tasks;
using MoviesMobileApp.Helpers;
using MoviesMobileApp.Pages;
using MoviesMobileApp.Service.Contracts;
using MoviesMobileApp.Service.Models;
using MvvmHelpers;
using Xamarin.Forms;

namespace MoviesMobileApp.ViewModel
{
    public class FeedViewModel : BaseViewModel
    {
        readonly MoviesClient _moviesClient = new MoviesClient();

        ObservableRangeCollection<FeedViewModel> _recommendations;
        
        public FeedViewModel(Feed feed)
        {
            Feed = feed;
            _recommendations = new ObservableRangeCollection<FeedViewModel>();
            LoadFeedData();
            Title = "Details";
        }

        public ObservableRangeCollection<FeedViewModel> Recommendations
        {
            get => _recommendations;
            set => SetProperty(ref _recommendations, value);
        }

        public Feed Feed { get; }

        public string PosterThumbnail { get; private set; }

        public string Backdrop { get; private set; }

        public string Poster { get; private set; }

        public string GenresDetailed { get; private set; }

        public string FeedTitle => Feed.Title;

        public double VoteAverage => Feed.VoteAverage;

        public string ReleaseDate => Feed.ReleaseDate;

        public string Overview => Feed.Overview;

        public async void SendItemTapped()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                await Application.Current.MainPage.Navigation.PushAsync(new FeedDetailsPage(this));
                IsBusy = false;
                RequestRecommendationsIfNeeded();
            }
        }

        public async void RequestRecommendationsIfNeeded()
        {
            if (!Recommendations.Any())
            {
                await RecommendationsAsync();
            }
        }

        void LoadFeedData()
        {
            PosterThumbnail = $"{Settings.Configuration.Images.SecureBaseUrl}{Settings.Configuration.Images.PosterSizes.First()}{Feed.PosterPath}";
            Backdrop = $"{Settings.Configuration.Images.SecureBaseUrl}{Settings.Configuration.Images.BackdropSizes.Last()}{Feed.BackdropPath}";
            Poster = $"{Settings.Configuration.Images.SecureBaseUrl}{Settings.Configuration.Images.PosterSizes.Last()}{Feed.PosterPath}";
            GenresDetailed = string.Empty;
            if (Feed.GenreIds != null)
            {
                for (int i = 0; i < Feed.GenreIds.Count; i++)
                {
                    var genreId = Feed.GenreIds[i];
                    var genreData = Settings.GenresData?.Genres?.FirstOrDefault(genre => genre.Id == genreId);
                    if (genreData != null)
                    {
                        if (i == 0)
                        {
                            GenresDetailed += genreData.Name;
                        }
                        else if (i == Feed.GenreIds.Count - 1)
                        {
                            GenresDetailed += $" & {genreData.Name}";
                        }
                        else
                        {
                            GenresDetailed += $", {genreData.Name}";
                        }
                    }
                }
            }
        }

        async Task RecommendationsAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    var response = await _moviesClient.GetRecommendationsFeedsAsync(Feed.Id);
                    Recommendations = new ObservableRangeCollection<FeedViewModel>(response.Result.Results.Select(feed => new FeedViewModel(feed)));
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
}
