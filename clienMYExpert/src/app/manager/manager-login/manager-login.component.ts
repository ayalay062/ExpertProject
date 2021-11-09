import { Component, OnInit } from '@angular/core';
import { User } from '../../classes/user';
import { UsersService } from '../../services/users.service';
import { FormGroup, FormBuilder, ReactiveFormsModule, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
@Component({
  selector: 'app-manager-login',
  templateUrl: './manager-login.component.html',
  styleUrls: ['./manager-login.component.scss']
})
export class ManagerLoginComponent implements OnInit {
  loginManagerForm: FormGroup;
  details: any;
  user: User;
  somethingWrong: boolean = false;
  constructor(private userService: UsersService, private formBuilder: FormBuilder,private route: Router) {
    this.loginManagerForm = formBuilder.group({
      name: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}')]]
    })
  }
  log_in() {
    this.userService.loginManager(this.loginManagerForm.value).subscribe((res: User) => {
      if (res[0].id==undefined) {
        this.somethingWrong = true
      }
      else {
        this.somethingWrong = false;
        this.user = res[0];
        this.userService.getLoggedInName.emit(this.user);
        localStorage.setItem("user", JSON.stringify(this.user));
        this.route.navigateByUrl("/manager-settings");
      }
    }, err => console.log(err))
  }
  ngOnInit(): void {
  }
  get name() {
    return this.loginManagerForm.get("name");
  }

  get password() {
    return this.loginManagerForm.get("password");
  }
  getPasswordErrorMessage() {
    if (this.password.hasError('required')) {
      return 'שדה חובה';
    }
    else if (this.password.hasError('pattern'))
      return 'סיסמת מנהל חייבת להיות בת 8 תוים לפחות ולכלול אותיות גדולות, קטנות, מספרים ותוים'
    else return 'ערך לא חוקי'

  }
  getNameErrorMessage() {
    if (this.name.hasError('required')) {
      return 'שדה חובה';
    }
    else return 'ערך לא חוקי'

  }
}
