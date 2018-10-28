import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MovieDetails } from '../interface/models';

@Component({
  selector: 'app-cheapestmovies',
  templateUrl: './cheapestmovies.component.html',
})
export class CheapestMoviesComponent {
  public cheapestMovies: MovieDetails[] = [];

  constructor(public http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<MovieDetails[]>(baseUrl + 'api/Movies/CheapestMovies').subscribe(result => {
      this.cheapestMovies = result;
      console.log(result);
    }, error => console.error(error));
  }
}
