import { Component, OnInit } from '@angular/core';
import { Expert } from 'src/app/classes/expert';
import { ActivatedRoute } from '@angular/router';
import { ExpertsService } from 'src/app/services/experts.service';
import { User } from 'src/app/classes/user';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-expert-settings',
  templateUrl: './expert-settings.component.html',
  styleUrls: ['./expert-settings.component.scss']
})
export class ExpertSettingsComponent implements OnInit {
expert:Expert;
asUser:User;
  constructor(private experts: ExpertsService, private users: UsersService, private activatedRoute: ActivatedRoute,) { 
    this.activatedRoute.paramMap.subscribe(res => {
      this.experts.getById(Number(res.get("id"))).subscribe((res: Expert) => {
        this.expert = res[0];
      },
        err => {
          console.log(err)
        })
    });
    this.activatedRoute.paramMap.subscribe(res=>{
      this.users.getUserById(Number(res.get("id"))).subscribe( (res:User) =>{
        this.asUser = res[0];
      }, err => {
        console.log(err);
      } )
    });
    
    
  }

  ngOnInit(): void {

    // this.expert=new Expert(1,"נחמן שימעוני","543211","nanachnachma@gmail.com",16,20,"","העסק הטוב ביותר","תבואו, תציצו, תיפגעו",3)
  }

}
