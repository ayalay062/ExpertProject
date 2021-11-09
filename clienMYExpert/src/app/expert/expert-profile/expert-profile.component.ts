import { Component, OnInit, Input, ViewChild, ElementRef } from '@angular/core';
import { Expert } from 'src/app/classes/expert';
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormControl,
} from '@angular/forms';
import { CompileTypeSummary } from '@angular/compiler';
import { CitiesService } from 'src/app/services/cities.service';
import { City } from 'src/app/classes/city';
import { Observable, of } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { ExpertsService } from 'src/app/services/experts.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersService } from 'src/app/services/users.service';
import { User } from 'src/app/classes/user';
import Swal from 'sweetalert2';
import { SubjectsService } from 'src/app/services/subjects.service';
import { Subject } from 'src/app/classes/subject';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-expert-profile',
  templateUrl: './expert-profile.component.html',
  styleUrls: ['./expert-profile.component.scss'],
})
export class ExpertProfileComponent implements OnInit {
  fileName1: string;
  editedExpert: Expert = new Expert();
  filteredCities: Observable<string[]>;
  citySelect = new FormControl();
envApi = environment.apiBase;
  @ViewChild('FileSelectInputDialog') FileSelectInputDialog: ElementRef;
  // detailsForm = this.formBuilder.group({
  //   name: ['', [Validators.required]],
  //   password: ['', [Validators.required]],
  //   city: ['', [Validators.required]],
  //   businessName: ['', [Validators.required]],
  //   description: ['', [Validators.required]],
  //   imgUrl: ['']
  // });

  uploadImg(e) {
    try {
      this.fileName1 = e.target.files[0].name;
    } catch (e) {
      console.log('error', e);
    }
  }
  allCities: City[];
  allSubjects: Subject[];
  expert: Expert = new Expert();
  fname: string;
  lname: string;
  currentSubject: string;
  currentCity: string;
  companyName: string;
  description: string;
  exId = 0;
  validationArr: boolean[] = [];
  constructor(
    private userService: UsersService,
    private cityService: CitiesService,
    private subjectService: SubjectsService,
    private expertService: ExpertsService,
    private router: Router
  ) {
    for (let i = 0; i < 10; i++) {
      this.validationArr.push(false);
    }
  }

  ngOnInit(): void {
    var exp = localStorage.getItem('user');
    if (exp != null && exp != 'null') {
      this.exId = (<User>JSON.parse(exp)).id;
    }
    this.expertService.getById(this.exId).subscribe(
      (res: Expert) => {
        this.expert = res;
        this.currentSubject =  this.subjectService.getSubjectById( this.expert.proSubject);
        this.currentCity = this.cityService.getCityById( this.expert.cityId ).name;
      },
      (err) => console.log(err)
    );

    this.cityService.getAllCities().subscribe(
      (res: City[]) => {
        this.allCities = res;
      },
      (err) => console.log(err)
    );
    this.subjectService.getAllSubjects().subscribe(
      (res: Subject[]) => (this.allSubjects = res),
      (err) => console.log(err)
    );
  }

  save() {
    if (!this.isValid()) return;
    this.expert.proSubject = this.subjectService.getSubjectByName(
      this.currentSubject
    );
    this.expert.cityId = this.cityService.getCityByName(this.currentCity);
    this.expertService.putExpert(this.expert).subscribe(
      (res) => {
        Swal.fire('הי', 'השמירה בוצעה בהצלחה', 'success');
        if (this.selectedFile !== null) {
          this.expertService.uploadImg(this.expert.id, this.fd).subscribe(
            (res) => {},
            (err) => console.log(err)
          );
        }
        //  console.log(res);
        // this.expert.id = res.insertId;
        //localStorage.setItem('user', JSON.stringify(this.expert));
      },
      (err) => console.log(err)
    );
  }

  isValid(): boolean {
    var inputs = document.getElementsByTagName('input');
    let isValid: boolean;
    isValid = true;
    for (let i = 0; i < inputs.length - 2; i++)
      if (this.validationArr[i]) isValid = false;
    for (let i = 0; i < inputs.length - 2; i++) {
      if ((<HTMLInputElement>inputs[i]).value.length < 1) {
        this.validationArr[i] = true;
        isValid = false;
      } else this.validationArr[i] = false;
    }

    return isValid;
  }
  check() {
    if ((<HTMLInputElement>event.target).value.length <= 1) {
      this.validationArr[Number((<HTMLInputElement>event.target).id)] = true;
      return;
    }
    if ((<HTMLInputElement>event.target).name == 'email')
      if (
        !/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(
          (<HTMLInputElement>event.target).value
        )
      ) {
        this.validationArr[Number((<HTMLInputElement>event.target).id)] = true;
        return;
      }
    if ((<HTMLInputElement>event.target).name == 'password')
      if (
        (<HTMLInputElement>event.target).value.length > 10 ||
        (<HTMLInputElement>event.target).value.length < 5
      ) {
        this.validationArr[Number((<HTMLInputElement>event.target).id)] = true;
        return;
      }
    if ((<HTMLInputElement>event.target).name == 'phone')
      if ((<HTMLInputElement>event.target).value.length !== 10) {
        this.validationArr[Number((<HTMLInputElement>event.target).id)] = true;
        return;
      }
    if ((<HTMLInputElement>event.target).name == 'cityId')
      if (
        !this.cityService.getCityByName((<HTMLInputElement>event.target).value)
      ) {
        this.validationArr[Number((<HTMLInputElement>event.target).id)] = true;
        return;
      }
    if ((<HTMLInputElement>event.target).name == 'field')
      if (
        !this.subjectService.getSubjectByName(
          (<HTMLInputElement>event.target).value
        )
      ) {
        this.validationArr[Number((<HTMLInputElement>event.target).id)] = true;
        return;
      }
    this.validationArr[Number((<HTMLInputElement>event.target).id)] = false;
  }

  onSelectFile(event) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]);
      reader.onload = (event) => {
        // this.selectedFile = event.target;
        // called once readAsDataURL is completed
        //  this.detailsForm.get("imgUrl").setValue(event.target.result);
      };
    }
  }
  selectedFile: File = null;
  fd = new FormData();

  createFormData(event) {
    this.selectedFile = <File>event.target.files[0];
    this.fileName1 = this.selectedFile.name;
    this.fd.append('file', this.selectedFile, this.selectedFile.name);
    //   this.detailsForm.get("imgUrl").setValue( this.selectedFile.name);
  }
}
