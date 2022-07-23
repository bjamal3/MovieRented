import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MovieComponent } from './movies/movies.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddEditMovieComponent } from './add-edit-movie/add-edit-movie.component';
import { BsModalService } from 'ngx-bootstrap/modal';
import { MovieListComponent } from './movie-list/movie-list.component';
import { AuthGuard } from '../shared/guards/auth.guard';
import { AdminGuard } from '../shared/guards/admin.guard';



@NgModule({
  declarations: [MovieComponent, AddEditMovieComponent, MovieListComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: 'list', component: MovieComponent, canActivate: [AuthGuard, AdminGuard]  },
      { path: 'list-grid', component: MovieListComponent, canActivate: [AuthGuard]  }
    ])
  ],
  exports:[],
  providers: [BsModalService]
})
export class CompanyModule { }
