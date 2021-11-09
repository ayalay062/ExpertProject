import { Injectable } from '@angular/core';
import { Expert } from '../classes/expert';
import { SubjectsService } from './subjects.service';
import { UsersService } from './users.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PageEvent } from '@angular/material/paginator';
import { Meeting } from '../classes/meeting';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ExpertsService {


  uploadImg(id: number, fd: FormData) {
    return this.http.post<boolean>(this.url + 'uploadImg/'+id, fd);
  }


  allExperts: Expert[] = [];
  url = environment.api + 'Expert/';
  constructor(
    private subjects: SubjectsService,
    private users: UsersService,
    private http: HttpClient
  ) {}

  signup(expert: Expert): Observable<any> {
    return this.http.post<any>(this.url + 'InsertExpert', expert);
  }

  getAllExperts(): Observable<Expert[]> {
    return this.http.get<Expert[]>(this.url + 'GetExperts');
  }
  getAllExpertsAdmin(): Observable<Expert[]> {
    return this.http.get<Expert[]>(this.url + 'GetAllExperts');
  }
  
  getById(id: number) {
    return this.http.get(this.url + 'GetExpertById/' + id);
  }
  getPerPage(pageSize: number, pageIndex: number) {
    return this.http.get(this.url + 'all/page/' + pageIndex, {
      params: new HttpParams()
        .set('npp', pageSize.toString())
        .set('page', pageIndex.toString()),
    });
  }

  filterExperts(
    currentCategory: string,
    currentSubject: string,
    currentCity: string,
    name: string
  ): Observable<Expert[]> {
    return this.http.get<Expert[]>(
      this.url +
        'GetFilteredExperts/' +
        (currentCategory || 0) +
        '/' +
        (currentSubject || 0) +
        '/' +
        (currentCity  || 0)+
        '/' +
       (name || "-")
    );
  }
  changeStatus(id, status) {
    return this.http.post(this.url + 'ChangeStatus', { id: id, enable: status });
  }
  putExpert(exp: Expert) {
    console.log('HHHHHHHHH');
    return this.http.post(this.url + 'PutExpert', exp);
  }
  insertImg(fd: FormData) {
    return this.http.post(this.url + 'change-image', fd);
  }
}
