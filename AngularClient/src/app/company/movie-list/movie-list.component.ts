import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RepositoryService } from 'src/app/shared/services/repository.service';
import { Movie } from 'src/app/_interfaces/movie.model';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css']
})
export class MovieListComponent implements OnInit {

  public movies: Movie[];

  constructor(private repository: RepositoryService) { }

  ngOnInit(): void {
    this.getMovies();
  }

  getMovies = () => {
    const apiAddress: string = "api/movies";
    this.repository.getData(apiAddress)
    .subscribe({
      next: (res: Movie[]) => this.movies = res,
      error: (err: HttpErrorResponse) => console.log(err)
    })
  }

  addToCart(event, movieId) {
    console.log(event);
    console.log(movieId);
  }

}
