import { Movie } from '../../_interfaces/movie.model';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'; 
import { EnvironmentUrlService } from './environment-url.service';

@Injectable({
  providedIn: 'root'
})
export class RepositoryService {

  constructor(private http: HttpClient, private envUrl: EnvironmentUrlService) { }

  public getData = (route: string) => {
    return this.http.get<Movie[]>(this.createCompleteRoute(route, this.envUrl.urlAddress));
  }

  public postData = (route: string, body: any) => {
    return this.http.post<Movie>(this.createCompleteRoute(route, this.envUrl.urlAddress),body);
  }

  public putData = (route: string, body: any) => {
    return this.http.put<Movie>(this.createCompleteRoute(route, this.envUrl.urlAddress),body);
  }

  public deleteData = (route: string) => {
    return this.http.delete<any>(this.createCompleteRoute(route, this.envUrl.urlAddress));
  }

  public getClaims = (route: string) => {
    return this.http.get(this.createCompleteRoute(route, this.envUrl.urlAddress));
  }
 
  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}