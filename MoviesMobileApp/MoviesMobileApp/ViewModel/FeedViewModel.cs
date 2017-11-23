using System;
using System.Linq;
using MoviesMobileApp.Helpers;
using MoviesMobileApp.Service.Models;
using MvvmHelpers;
using Xamarin.Forms;

namespace MoviesMobileApp.ViewModel
{
    public class FeedViewModel : BaseViewModel
    {
        public FeedViewModel(Feed feed)
        {
            Feed = feed;
            LoadFeedData();
        }

        public Feed Feed { get; }

        public string PosterThumbnail { get; private set; }

        public string GenresDetailed { get; private set; }

        public string FeedTitle => Feed.Title;

        public double VoteAverage => Feed.VoteAverage;

        public string ReleaseDate => Feed.ReleaseDate;

        void LoadFeedData()
        {
            PosterThumbnail = $"{Settings.Configuration.Images.SecureBaseUrl}{Settings.Configuration.Images.PosterSizes.First()}{Feed.PosterPath}";
            GenresDetailed = string.Empty;
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

        public void SendItemTapped()
        {
            
        }
    }
}
