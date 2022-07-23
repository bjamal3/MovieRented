import { Movie } from '../../_interfaces/movie.model';
import { RepositoryService } from '../../shared/services/repository.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { AddEditMovieComponent } from '../add-edit-movie/add-edit-movie.component';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MovieComponent implements OnInit {
  public movies: Movie[];
  message?: string;
  id: string;
  bsModalRefDel: any;
  constructor(private repository: RepositoryService, private modalService: BsModalService) { 

  }

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

  openModalWithComponent() {
    const initialState = {
      closeBtnName: 'Close',
      yesBtnName: 'Add Movie',
      title: 'Add new movie',
      id: '0',
      movie: null
    };
    const bsModalRef = this.modalService.show(AddEditMovieComponent, {initialState});
    bsModalRef.content.isAddOrUpdated.subscribe((res) => {
      this.getMovies();
    })
  }

  openEditModalWithComponent(movie: Movie) {
    const initialState = {
      closeBtnName: 'Close',
      yesBtnName: 'Update Movie',
      title: 'Update movie',
      id: movie.id,
      movie: movie
    };
    const bsModalRef = this.modalService.show(AddEditMovieComponent, {initialState});
    bsModalRef.content.isAddOrUpdated.subscribe((res) => {
      this.getMovies();
    })
  }

  openModal(template: TemplateRef<any>, id: string) {
    this.id = id;
    this.bsModalRefDel = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(id: string): void {
    if(id) {
      this.repository.deleteData(`api/movies/${id}`)
      .subscribe({
        next: (res: any) => this.getMovies(),
        error: (err: HttpErrorResponse) => console.log(err)
      });
    }
    this.bsModalRefDel?.hide();
  }
 
  decline(): void {
    this.bsModalRefDel?.hide();
  }
  
}
