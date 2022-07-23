import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BsModalService, BsModalRef, ModalOptions } from 'ngx-bootstrap/modal';

import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { UserForRegistrationDto } from './../../_interfaces/user/userForRegistrationDto.model';
import { RepositoryService } from './../../shared/services/repository.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MovieDto } from 'src/app/_interfaces/movieForRegisterDto.model';
import { Movie } from 'src/app/_interfaces/movie.model';

@Component({
  selector: 'app-add-edit-movie',
  templateUrl: './add-edit-movie.component.html',
  styleUrls: ['./add-edit-movie.component.css']
})
export class AddEditMovieComponent implements OnInit {

  @Output() isAddOrUpdated= new EventEmitter<boolean>();
  @Input() title?: string;
  @Input() closeBtnName?: string;
  @Input() yesBtnName?: string;
  @Input() id: string;
  @Input() movie: Movie;

  movieForm: FormGroup;
  errorMessage: string = '';
  showError: boolean;

 
  constructor(public bsModalRef: BsModalRef,
              private repoService: RepositoryService,
              private router: Router) {
                
              }
 
  ngOnInit() {
    console.log(this.id);
    console.log(this.movie);
    if(this.id) {
      this.movieForm = new FormGroup({
        id: new FormControl(this.movie.id),
        title: new FormControl(this.movie.title,  [Validators.required]),
        description: new FormControl(this.movie.description, [Validators.required]),
        yearPublished: new FormControl(this.movie.yearPublished, [Validators.required]),
        //thumbnailUrl: new FormControl('', [Validators.required]),
        directors: new FormControl(this.movie.directors, [Validators.required]),
        stars: new FormControl(this.movie.stars, [Validators.required]),
        votes: new FormControl(this.movie.votes, [Validators.required]),
        gross: new FormControl(this.movie.gross, [Validators.required]),
      });
    } else {
      this.movieForm = new FormGroup({
        id: new FormControl(''),
        title: new FormControl('',  [Validators.required]),
        description: new FormControl('', [Validators.required]),
        yearPublished: new FormControl('', [Validators.required]),
        //thumbnailUrl: new FormControl('', [Validators.required]),
        directors: new FormControl('', [Validators.required]),
        stars: new FormControl('', [Validators.required]),
        votes: new FormControl('', [Validators.required]),
        gross: new FormControl('', [Validators.required]),
      });
    }
  }

  public validateControl = (controlName: string) => {
    return this.movieForm.get(controlName).invalid && this.movieForm.get(controlName).touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.movieForm.get(controlName).hasError(errorName)
  }

  public addOrUpdateMovie = (movieFormValue: any) => {
    debugger;
    this.showError = false;
    const formValues = { ...movieFormValue };

    const movie: MovieDto = {
      title: formValues.title,
      description: formValues.description,
      yearPublished: formValues.yearPublished.toString(),
      thumbnailUrl: '',
      directors: formValues.directors,
      stars: formValues.stars,
      votes: formValues.votes,
      gross: formValues.gross,
    };

    if(this.id) {
      let apiUrl = `api/movies/${this.id}`;
      this.repoService.putData(apiUrl, movie)
      .subscribe({
        next: (_) => this.redirectToMain(),
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.message;
          this.showError = true;
        }
      })
    } else {
      let apiUrl = `api/movies`;
      this.repoService.postData(apiUrl, movie)
      .subscribe({
        next: (_) => this.redirectToMain(),
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.message;
          this.showError = true;
        }
      })
    }

    
  }

  redirectToMain(): void {
    this.bsModalRef.hide();
    this.isAddOrUpdated.emit(true);
  }

}
