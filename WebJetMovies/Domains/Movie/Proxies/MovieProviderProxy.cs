using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebJetMovies.Domains.Movie.Proxies {
    public class MovieProviderProxy {
        const string c_x_access_token = "x-access-token";
        const string c_movieUrl = "http://webjetapitest.azurewebsites.net/api";
        const string c_cinemaworldMovies = "cinemaworld/movies";
        const string c_cinemaworldMovie = "cinemaworld/movie/";
        const string c_filmworldMovies = "filmworld/movies";
        const string c_filmworldMovie = "filmworld/movie/";
        const string c_appJson = "application/json";
        
        readonly HttpClient HttpClient;
        readonly string WebAccessToken;

        public MovieProviderProxy(string webAccessToken, HttpClient httpClient) {
            WebAccessToken = webAccessToken;
            HttpClient = httpClient;
        }
        
        public async Task<string> GetCinemaWorldMovies() {
            return await GetMoviesData(string.Format("{0}/{1}", c_movieUrl, c_cinemaworldMovies));
        }

        public async Task<string> GetCinemaWorldMovie(string movieId) {
            var movieUri = string.Format("{0}/{1}/{2}", c_movieUrl, c_cinemaworldMovie, movieId);
            return await GetMoviesData(movieUri);
        }

        public async Task<string> GetFilmWorldMovies() {
            return await GetMoviesData(string.Format("{0}/{1}", c_movieUrl, c_filmworldMovies));
        }

        public async Task<string> GetFilmWorldMovie(string movieId) {
            var movieUri = string.Format("{0}/{1}/{2}", c_movieUrl, c_filmworldMovie, movieId);
            return await GetMoviesData(movieUri);
        }

        async Task<string> GetMoviesData(string movieUri) {
            string result = string.Empty;
            try {
                using (HttpClient client = new HttpClient()) {
                    client.DefaultRequestHeaders.Add(c_x_access_token, WebAccessToken);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(c_appJson));
                    var response = await client.GetAsync(movieUri);
                    result = response.IsSuccessStatusCode ?
                        await response.Content.ReadAsStringAsync() : string.Empty;

                }
            }
            catch (TaskCanceledException ex) {
                return string.Empty;
            }
            catch (Exception ex) {
                throw ex;
            }
            return result;
        }
    }
}
