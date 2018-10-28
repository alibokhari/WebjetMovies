import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FilmWorldComponent } from './filmworld/filmworld.component';
import { CinemaWorldComponent } from './cinemaworld/cinemaworld.component';
import { CheapestMoviesComponent } from './cheapestmovies/cheapestmovies.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FilmWorldComponent,
    CinemaWorldComponent,
    CheapestMoviesComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'filmworld', component: FilmWorldComponent },
      { path: 'cinemaworld', component: CinemaWorldComponent },
      { path: 'cheapestmovies', component: CheapestMoviesComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
