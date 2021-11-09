import { Component, OnInit } from '@angular/core';
import { CitiesService } from '../services/cities.service';
import { SubjectsService } from '../services/subjects.service';
import { Expert } from '../classes/expert';
import { ExpertsService } from '../services/experts.service';
import { User } from '../classes/user';
import { Subject } from '../classes/subject';
import { City } from '../classes/city';
import { Router } from '@angular/router';
import { UsersService } from '../services/users.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-sign-up-expert',
  templateUrl: './sign-up-expert.component.html',
  styleUrls: ['./sign-up-expert.component.scss'],
})
export class SignUpExpertComponent implements OnInit {
  allCities: City[];
  allSubjects: Subject[];
  expert: Expert = new Expert();
  fname: string;
  lname: string;
  currentSubject: string;
  currentCity: string;
  companyName: string;
  description: string;
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

  signup() {
    if (!this.isValid()) return;
    this.expert.proSubject = this.subjectService.getSubjectByName(
      this.currentSubject
    );
    this.expert.cityId = this.cityService.getCityByName(this.currentCity);
    this.expertService.signup(this.expert).subscribe(
      (res) => {
        Swal.fire('הי','נרשמת בהצלחה','success');
        console.log(res);
        this.expert.id = res.insertId;
        localStorage.setItem('user', JSON.stringify(this.expert));
        this.router.navigate(['/expertInfo/' + this.expert.id + '/profile']);
        this.userService.getLoggedInName.emit(
          new User(
            this.expert.id,
            this.expert.userName,
            this.expert.userPassword,
            this.expert.email,
            this.expert.cityId,
            2,
            this.expert.imgUrl
          )
        );
      },
      (err) => console.log(err)
    );
  }

  isValid(): boolean {
    var inputs = document.getElementsByTagName('input');
    let isValid: boolean;
    isValid = true;
    for (let i = 0; i < inputs.length; i++)
      if (this.validationArr[i]) isValid = false;
    for (let i = 0; i < inputs.length; i++) {
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
      if (
        (<HTMLInputElement>event.target).value.length !== 10) {
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
}
