import { Injectable } from '@angular/core';
import { Subject } from '../classes/subject';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SubjectsService {
  allSubject: Subject[] = [];
  parentSubjects: Subject[] = [];
  childrenSubjects: Subject[] = [];
  url = environment.api + 'Subjects/';
  constructor(private http: HttpClient) {
    this.getAllSubjects().subscribe(
      (res: Subject[]) => {
        this.allSubject = res;
      },
      (err) => console.log(err)
    );

    this.getAllParentsSubjects().subscribe(
      (res: Subject[]) => {
        this.parentSubjects = res;
      },
      (err) => console.log(err)
    );
  }
  getSubjectById(id: number): string {
    let s: Subject;
    s = this.allSubject.find((subject) => subject.id == id);
    if (s != null) return s.subName;
    return '';
  }
  getSubjectByName(name: string): number {
    var subject = this.allSubject.filter((x) => x.subName == name);
    if (!subject || subject.length == 0) {
      return null;
    }
    return subject[0].id;
  }
  getAllSubjects(): Observable<Subject[]> {
    return this.http.get<Subject[]>(this.url + 'GetAll');
  }
  getChildrenSubjects(ID: string): Observable<Subject[]> {
    return this.http.get<Subject[]>(this.url + 'GetSubjectsByParentId/' + ID);
  }
  getAllParentsSubjects(): Observable<Subject[]> {
    return this.http.get<Subject[]>(this.url + 'GetAllParents');
  }
}
