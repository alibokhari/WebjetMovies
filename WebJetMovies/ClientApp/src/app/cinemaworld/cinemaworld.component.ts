import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MovieDetails } from '../interface/models';

@Component({
  selector: 'app-cinemaworld',
  templateUrl: './cinemaworld.component.html',
})
export class CinemaWorldComponent {
  public cinemaworldMovies: MovieDetails[];

  constructor(public http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<MovieDetails[]>(baseUrl + 'api/Movies/CinemaWorldMovies').subscribe(result => {
      this.cinemaworldMovies = result;
      console.log(result);
    }, error => console.error(error));
  }
}
