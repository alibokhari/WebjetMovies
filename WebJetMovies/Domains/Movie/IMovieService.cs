using System.Collections.Generic;
using WebJetMovies.Domains.Movie.Models.Entities;
using WebJetMovies.Domains.Movie.Models.Enums;
using WebJetMovies.Domains.Movie.Models.ValueObjects;

namespace WebJetMovies.Domains.Movie {
    public interface IMovieService {
        MovieDetails GetCinemaWorldMovie(string movieId);
        IList<MoviePoster> GetCinemaWorldMovies();

        MovieDetails GetFilmWorldMovie(string movieId);
        IList<MoviePoster> GetFilmWorldMovies();

        IList<MovieDetails> GetCinemaWorldMoviesWithDetails();
        IList<MovieDetails> GetFilmWorldMoviesWithDetails();

        IEnumerable<MovieDetails> GetCheapestMovies();
    }
}
