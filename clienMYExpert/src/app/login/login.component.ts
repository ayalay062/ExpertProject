import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { User } from '../classes/user';
import { UsersService } from '../services/users.service';
import { FormGroup, FormBuilder, ReactiveFormsModule, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ExpertsService } from '../services/experts.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  details: any;
  user: User;
  somethingWrong: boolean = false;
  @Output() private onChange: EventEmitter<string> = new EventEmitter<string>();
  constructor(private userService: UsersService, private formBuilder: FormBuilder, private route: Router, private expertService: ExpertsService) {
    this.loginForm = formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      userPassword : ['', [Validators.required, Validators.minLength(4)]]
    })
  }
  log_in() {
    this.userService.login(this.loginForm.value).subscribe((res: User) => {
      if (res == null) {
        this.somethingWrong = true;
      }
      else {
        this.user = res;
        this.userService.getLoggedInName.emit(this.user);
        localStorage.setItem("user", JSON.stringify(this.user));
        if(this.user.userType == 2){
            this.route.navigateByUrl("/expertInfo/"+this.user.id);
          }
        if(this.user.userType==3){
          this.route.navigateByUrl("/manager-settings");
        }
        else
            this.route.navigateByUrl("/experts");
      }
    }, err => console.log(err))
}
ngOnInit(): void {
}
check(): void {
  console.log(this.loginForm.value.email, ", ", this.loginForm.value.email.valid)
}
get email() {
  return this.loginForm.get("email");
}
get userPassword() {
  return this.loginForm.get("userPassword");
}
getPasswordErrorMessage() {
  if (this.userPassword.hasError('required')) {
    return 'שדה חובה';
  }
  else if (this.userPassword.hasError('minlength'))
    return ' סיסמא חייבת להיות בת 5 תוים לפחות'
  else return 'ערך לא חוקי'
}
getEmailErrorMessage() {
  if (this.email.hasError('required')) {
    return 'שדה חובה';
  }
  else if (this.email.hasError('email'))
    return 'כתובת מייל לא תקינה'
  else return 'ערך לא חוקי'
}
}