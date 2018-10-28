import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MovieDetails } from '../interface/models';

@Component({
  selector: 'app-filmworld',
  templateUrl: './filmworld.component.html',
})
export class FilmWorldComponent {
  public filmworldMovies: MovieDetails[];

  constructor(public http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<MovieDetails[]>(baseUrl + 'api/Movies/FilmWorldMovies').subscribe(result => {
      this.filmworldMovies = result;
      console.log(result);
    }, error => console.error(error));
  }
}
