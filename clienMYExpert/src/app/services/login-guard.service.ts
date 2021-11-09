import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot, UrlSegment } from '@angular/router';
import { MeetingService } from './meeting.service';
import { Meeting } from '../classes/meeting';

@Injectable({
  providedIn: 'root'
})
export class LoginGuardService {


  public constructor(private router: Router, private meetingService: MeetingService) { }

  public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    console.log("Can Activate")
    let user = JSON.parse(localStorage.getItem("user"));
    if (user === null) {
      console.log("first condition")
      this.router.navigate(["/login"]);
      return false;
    }

    else if (route.url[0].path.includes("add-recommend")) {
      console.log("second condition",user)
      let meeting: Meeting;
      let userId = user.id;
      let expertId = route.paramMap.get("id");
      console.log(expertId)
      this.meetingService.findUserMeeting(Number(userId), Number(expertId)).subscribe(
        (res: Meeting) => {
          console.log(res)
          if (!res)
          {
            console.log("1")
            return false;
          }
          meeting = res;
          console.log(meeting);
          if (new Date(meeting.date) < new Date()){
            console.log("2");
             return true;
          }
          else
          { 
            console.log("3")
            return false;
          }
        }, err => console.log(err))
    }

    else {
      console.log("returning true :(");
      return true;
    }

  }
  logOut(): void {

  }
}