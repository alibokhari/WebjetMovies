using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebJetMovies.Domains.Movie;
using WebJetMovies.Domains.Movie.Models.Entities;
using WebJetMovies.Domains.Movie.Models.Enums;
using WebJetMovies.Domains.Movie.Models.ValueObjects;

namespace WebJetMovies.Controllers {
    [Route("api/[controller]")]
    public class MoviesController : Controller {
        readonly IMovieService MovieService;
        readonly AppSettings AppSettings;

        public MoviesController(IMovieService movieService) {
            MovieService = movieService;
        }

        [HttpGet("[action]")]
        public IEnumerable<MovieDetails> FilmWorldMovies() {
            return MovieService.GetFilmWorldMoviesWithDetails();
        }

        [HttpGet("[action]")]
        public IEnumerable<MovieDetails> CinemaWorldMovies() {
            return MovieService.GetCinemaWorldMoviesWithDetails();
        }

        [HttpGet("[action]")]
        public IEnumerable<MovieDetails> CheapestMovies() {
            return MovieService.GetCheapestMovies();
        }
    }
}
