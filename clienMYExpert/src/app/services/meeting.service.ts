import { Injectable, EventEmitter, Output } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Meeting } from '../classes/meeting';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class MeetingService {
  url = environment.api + 'meeting/';
  @Output() public getExistedMeeting: EventEmitter<Meeting> =
    new EventEmitter();
  constructor(private http: HttpClient) {}
  addMeeting(meet: Meeting): Observable<any> {
    return this.http.post(this.url + 'InsertMeeting', meet);
  }
  updateMeeting(meet: Meeting): Observable<any> {
    return this.http.post(this.url + 'UpdateMeeting', meet);
  }
  findUserMeeting(userId: number, expId: number): Observable<Meeting> {
    return this.http.get<Meeting>(this.url + 'getMeetingForUser/'+expId+'/' + userId);
  }
  deleteMeeting(meeting: Meeting): Observable<boolean> {
    return this.http.delete<boolean>(this.url + 'deleteMeeting/' + meeting.id);
  }
  getMeetingsForExpert(id: number): Observable<Meeting[]> {
    return this.http.get<Meeting[]>(this.url + 'GetAllMeetingsForExpert/' + id);
  }

  approveMeeting(meet: Meeting): Observable<Meeting> {
    return this.http.post<Meeting>(this.url + 'ApproveMeeting', meet);
  }

  cancelMeeting(meet: Meeting): Observable<Meeting> {
    return this.http.post<Meeting>(this.url + 'CancelMeeting', meet);
  }
}
