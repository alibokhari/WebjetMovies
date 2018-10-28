using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using WebJetMovies.Domains.Movie.Models.Entities;
using WebJetMovies.Domains.Movie.Models.Enums;
using WebJetMovies.Domains.Movie.Models.ValueObjects;
using WebJetMovies.Domains.Movie.Proxies;
using System.Linq;

namespace WebJetMovies.Domains.Movie.Services {
    public class MovieService : IMovieService {
        readonly HttpClient HttpClient;
        readonly AppSettings AppSettings;

        public MovieService(IOptions<AppSettings> appsettings, HttpClient httpClient) {
            AppSettings = appsettings.Value;
            HttpClient = httpClient;
        }

        public MovieDetails GetCinemaWorldMovie(string movieId) {
            return GetMovie(MovieProvider.CinemaWorld, movieId);
        }
        public IList<MoviePoster> GetCinemaWorldMovies() {
            return GetMovies(MovieProvider.CinemaWorld);
        }
        public IList<MovieDetails> GetCinemaWorldMoviesWithDetails() {
            var movies = new List<MovieDetails>();
            var cinemaworldMovies = GetMovies(MovieProvider.CinemaWorld);
            foreach (var cinemamovie in cinemaworldMovies) {
                var movie = GetMovie(MovieProvider.CinemaWorld, cinemamovie.ID);
                if (movie != null) movies.Add(movie);
            }
            return movies;
        }

        public MovieDetails GetFilmWorldMovie(string movieId) {
            return GetMovie(MovieProvider.FilmWorld, movieId);
        }
        public IList<MoviePoster> GetFilmWorldMovies() {
            return GetMovies(MovieProvider.FilmWorld);
        }
        public IList<MovieDetails> GetFilmWorldMoviesWithDetails() {
            var movies = new List<MovieDetails>();
            var filmworldMovies = GetMovies(MovieProvider.FilmWorld);
            foreach (var filmmovie in filmworldMovies) {
                var movie = GetMovie(MovieProvider.FilmWorld, filmmovie.ID);
                if (movie != null) movies.Add(movie);
            }
            return movies;
        }

        public IEnumerable<MovieDetails> GetCheapestMovies() {
            var movies = new List<MovieDetails>();
            var filmworldMovies = GetFilmWorldMoviesWithDetails();
            var cinemaworldMovies = GetCinemaWorldMoviesWithDetails();

            foreach (var filmMovie in filmworldMovies) {
                var cinemamovie = cinemaworldMovies.Where(m => m.Title == filmMovie.Title).FirstOrDefault();
                if (cinemamovie != null && Convert.ToDecimal(cinemamovie.Price) < Convert.ToDecimal(filmMovie.Price)) {
                    cinemamovie.Provider = MovieProvider.CinemaWorld.ToString();
                    movies.Add(cinemamovie);
                }
                else {
                    filmMovie.Provider = MovieProvider.FilmWorld.ToString();
                    movies.Add(filmMovie);
                }
            }

            foreach (var cinemaMovie in cinemaworldMovies) {
                if (movies.Where(m => m.Title == cinemaMovie.Title).FirstOrDefault() == null) {
                    cinemaMovie.Provider = MovieProvider.CinemaWorld.ToString();
                    movies.Add(cinemaMovie);
                }
            }

           return movies;
        }

        IList<MoviePoster> GetMovies(MovieProvider movieSource) {
            IList<MoviePoster> movies = new List<MoviePoster>();

            try {
                var proxy = new MovieProviderProxy(AppSettings.WebJetMoviesToken, HttpClient);
                var response = movieSource == MovieProvider.FilmWorld ? proxy.GetFilmWorldMovies().Result :
                    proxy.GetCinemaWorldMovies().Result;
                if (response != string.Empty) {
                    var responseObject = JObject.Parse(response);
                    var jsonMovies = (JArray)responseObject["Movies"];
                    movies = jsonMovies.ToObject<IList<MoviePoster>>();
                }
                return movies;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        MovieDetails GetMovie(MovieProvider movieSource, string movieId) {
            MovieDetails movieDetails = null;
            try {
                var proxy = new MovieProviderProxy(AppSettings.WebJetMoviesToken, HttpClient);
                var response = movieSource == MovieProvider.FilmWorld ? proxy.GetFilmWorldMovie(movieId).Result :
                    proxy.GetCinemaWorldMovie(movieId).Result;
                if (response != string.Empty) {
                    var responseObject = JObject.Parse(response);
                     movieDetails = responseObject.ToObject<MovieDetails>();
                }
                return movieDetails;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
