import { Component, OnInit } from '@angular/core';
import { Expert } from '../classes/expert';
import { User } from '../classes/user';
import { ExpertsService } from '../services/experts.service';
import { UsersService } from '../services/users.service';
import { Subject } from '../classes/subject';
import { City } from '../classes/city';
import { SubjectsService } from '../services/subjects.service';
import { CitiesService } from '../services/cities.service';
import { FormControl } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-disp-experts',
  templateUrl: './disp-experts.component.html',
  styleUrls: ['./disp-experts.component.scss']
})
export class DispExpertsComponent implements OnInit {


  alternativePicture:string = "http://turag.co.il/wp-content/uploads/2018/06/man.jpg";
  allexperts: Expert[];
  allCities: City[];
  allSubjects: Subject[];
  allParentsSubjects: Subject[];
  allChildrenSubjects: Subject[];
  currentParentSubject: string;//לא צריך
  currentSubject: string;//לא צריך
  searchName: string;
  param: Subject;
  notFound:boolean = false;
  envApi = environment.apiBase;
  citySelect = new FormControl();
  subjectSelect = new FormControl();
  categorySelect = new FormControl();
  filteredCities: Observable<string[]>;
  filteredSubjects: Observable<string[]>;
  filteredCategories: Observable<string[]>;

  constructor(private experts: ExpertsService, private users: UsersService,
    private cityService: CitiesService, private subjectService: SubjectsService) {
  
  }

  ngOnInit(): void {
    this.experts.getAllExperts().subscribe(
      (res: Expert[]) => {
        this.allexperts = res;
      },
      err => {
        console.log("some error:", err)
      })
    this.cityService.getAllCities().subscribe((res: City[]) => {
      this.allCities = res;
      this.filteredCities = this.citySelect.valueChanges.pipe(
        startWith(''),
        map(value => this._filterCity(value))
      );
    }, err => console.log(err))
    this.subjectService.getAllParentsSubjects().subscribe((res: Subject[]) => {
      this.allParentsSubjects = res;
      this.filteredCategories = this.categorySelect.valueChanges.pipe(
        startWith(''),
        map(value => this._filterCategory(value))
      );
    }, err => console.log(err))

  }
  fullStars(scores): number[] {
    let arr: number[] = [];
    let full, empty, i: number;
    full = Math.floor(scores);
    empty = Math.floor(5 - scores);
    for (i = 0; i < full; i++) arr.push(1);
    if (full + empty < 5) arr.push(0.5);
    for (i = 0; i < empty; i++) arr.push(0);
    return arr;
  }
  filter() {

    const s = this.categorySelect.value && this.categorySelect.value != '' ? this.subjectService.getSubjectByName(this.categorySelect.value).toString() : '';
    const s1 = this.currentSubject && this.currentSubject != '' ? this.subjectService.getSubjectByName(this.currentSubject).toString() : '';
    const c = this.citySelect.value && this.citySelect.value != "" ? this.cityService.getCityByName(this.citySelect.value).toString() : '';
    const n = this.searchName && this.searchName != '' ? this.searchName : '';
    console.log(s, s1, c, n);
    this.experts.filterExperts(s, s1, c, n).subscribe(
      (res: Expert[]) => {
        this.allexperts = res;
        if(!(res.length>0))
              this.notFound = true;
        else
              this.notFound = false;
        console.log(this.allexperts)
      },
      err => {
        console.log("some error:", err)
      })
  }

  clear() {
    this.citySelect.setValue("");
    this.subjectSelect.setValue("");
    this.categorySelect.setValue("");
    this.searchName = "";
    this.experts.getAllExperts().subscribe(
      (res: Expert[]) => {
        this.allexperts = res;
        console.log(this.allexperts)
      },
      err => {
        console.log("some error:", err)
      })
  }

  getChildren(): void {
    let s: number;
    s = this.subjectService.getSubjectByName(this.categorySelect.value);
    this.subjectService.getChildrenSubjects(s.toString()).subscribe(
      (res: Subject[]) => {
        this.allChildrenSubjects = res;
        this.filteredSubjects = this.subjectSelect.valueChanges.pipe(
          startWith(''),
          map(value => this._filterSubject(value)))
      }, err => console.log(err)
    )


    // this.filteredCategories = this.categorySelect.valueChanges.pipe(
    //   startWith(''),
    //   map(value => this._filterCategory(value))
    // );

  }

  getSubjectService(): SubjectsService {
    return this.subjectService;
  }

  clearParent(): void {
    this.currentParentSubject = "";
  }

  clearChild(): void {
    this.currentSubject = "";
  }

  clearCity(): void {
    this.citySelect.setValue("");
  }

  clearName(): void {
    this.searchName = "";
  }

  getSubjectName(id: number): string {
    return this.subjectService.getSubjectById(id);
  }

  private _filterCity(value: string): string[] {
    const filterValue = value.toLowerCase();
    let array: string[] = this.allCities.map(city => { return city.name })
    let filtered = array.filter(option => { return option.toLowerCase().includes(filterValue) });
    return filtered
  }
  private _filterCategory(value: string): string[] {
    const filterValue = value.toLowerCase();
    let array: string[] = this.allParentsSubjects.map(cate => { return cate.subName })
    let filtered = array.filter(option => { return option.toLowerCase().includes(filterValue) });
    return filtered;
  }
  private _filterSubject(value: string): string[] {
    const filterValue = value.toLowerCase();
    let array: string[] = this.allChildrenSubjects.map(sub => { return sub.subName })
    let filtered = array.filter(option => { return option.toLowerCase().includes(filterValue) });
    return filtered
  }
}
