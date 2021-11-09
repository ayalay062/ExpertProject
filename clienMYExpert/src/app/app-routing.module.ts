import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DispExpertsComponent } from './disp-experts/disp-experts.component';
import { DispExpertComponent } from './disp-expert/disp-expert.component';
import { LoginComponent } from './login/login.component';
import { AboutComponent } from './about/about.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { SignUpExpertComponent } from './sign-up-expert/sign-up-expert.component';
import { SignUpUserComponent } from './sign-up-user/sign-up-user.component';
import { AddRecommendComponent } from './add-recommend/add-recommend.component';
import { LoginGuardService } from './services/login-guard.service';
import { ManagerLoginComponent } from './manager/manager-login/manager-login.component';
import { ManagerExpertsComponent } from './manager/manager-experts/manager-experts.component';
import { ManagerRecommendsComponent } from './manager/manager-recommends/manager-recommends.component';
import { ManagerSettingsComponent } from './manager/manager-settings/manager-settings.component';
import { ManagerloginGuardService } from './services/managerlogin-guard.service';
import { RecommendsService } from './services/recommends.service';
import { DispRecommendsComponent } from './disp-recommends/disp-recommends.component';
import { ExpertSettingsComponent } from './expert/expert-settings/expert-settings.component';
import { AddMeetingComponent } from './add-meeting/add-meeting.component';
import { ExpertProfileComponent } from './expert/expert-profile/expert-profile.component';
import { ExpertMeetingsComponent } from './expert/expert-meetings/expert-meetings.component';
import { ExpertloginGuardService } from './services/expertlogin-guard.service';
import { QuestionComponent } from './forum/question/question.component';
import { QuestionsComponent } from './forum/questions/questions.component';






const routes: Routes = [{
  path: '', pathMatch: 'full', redirectTo: '/about' },

 {
  path: "login",
  component: LoginComponent
},
{
  path: "ask-question",
  canActivate: [LoginGuardService],
  component:QuestionComponent,
},
{
  path: "about",
  component: AboutComponent
}, {
  path: "manager-login",
  component: ManagerLoginComponent
}, {
  path: "manager-settings",
  canActivate: [ManagerloginGuardService],
  component: ManagerSettingsComponent,
  children: [{
    path: "manage-experts",
    canActivate: [ManagerloginGuardService],
    component: ManagerExpertsComponent,
  }, {
    path: "manage-recommends",
    canActivate: [ManagerloginGuardService],
    component: ManagerRecommendsComponent
  }, {
    path: "",
    canActivate: [ManagerloginGuardService],
    component: ManagerRecommendsComponent
  }]
}, {
  path: "signup",

  component: SignUpComponent,
  children: [{
    path: "sign-up-expert",
    component: SignUpExpertComponent,
  }, {
    path: "sign-up-user",
    component: SignUpUserComponent
  },
   {
    path: "",
    component: SignUpUserComponent
  }]
}, {
  path: "experts",
  component: DispExpertsComponent,
}, {
  path: "expert/:id",
  component: DispExpertComponent,
  children: [{
    path: "add-recommend",
    // canActivate: [LoginGuardService],
    component: AddRecommendComponent
  },
  {
    path: "recommends",
    component: DispRecommendsComponent
  }]
},
{
  path:"expertInfo/:id",
  component: ExpertSettingsComponent
}
,
{
  path: "expertInfo/:id/profile",
  component: ExpertProfileComponent,
  canActivate: [ExpertloginGuardService],
}, {
    path: "expertInfo/:id/meetings",
    component: ExpertMeetingsComponent,
    canActivate: [ExpertloginGuardService],
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
