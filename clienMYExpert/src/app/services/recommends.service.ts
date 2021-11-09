import { Injectable, Output, EventEmitter } from '@angular/core';
import { Recommend } from '../classes/recommend';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class RecommendsService {
  @Output() public getRecommend: EventEmitter<Recommend> = new EventEmitter();

  getPerPage(pageSize: number, pageIndex: number) {
    return this.http.get(this.url + 'all/page/' + pageIndex, {
      params: new HttpParams()
        .set('npp', pageSize.toString())
        .set('page', pageIndex.toString()),
    });
  }

  url: string = environment.api + 'recommend/';
  constructor(private http: HttpClient) {}
  checkValidation(user: number, profid: number) {
    return this.http.get(
      this.url + 'GetRecommendExistsByUserId/' + user + '/' + profid
    );
  }

  GetRecommendByUserId(user: number, profid: number) {
    return this.http.get<Recommend>(
      this.url + 'GetRecommendByUserId/' + user + '/' + profid
    );
  }
  AddRecommend(rec: any) {
    return this.http.post<Recommend>(this.url + 'AddRecommend', rec);
  }
  UpdateRecommend(rec: any) {
    return this.http.post<Recommend>(this.url + 'UpdateRecommend', rec);
  }
  getAllRecommends(): Observable<Recommend[]> {
    return this.http.get<Recommend[]>(this.url + 'GetAllRecommends');
  }

  changeStatus(r: Recommend): Observable<Recommend> {
    return this.http.post<Recommend>(this.url + 'changeStatus', r);
  }

  getAllApprovedRecommends(id: number): Observable<Recommend[]> {
    return this.http.get<Recommend[]>(
      this.url + 'GetAllRecommendsForExpert/' + id
    );
  }
}
