import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Expert } from 'src/app/classes/expert';
import { Meeting } from 'src/app/classes/meeting';
import { MeetingService } from 'src/app/services/meeting.service';
import { ExpertsService } from 'src/app/services/experts.service';
import { ActivatedRoute } from '@angular/router';
import { Time } from '@angular/common';
import { AddMeetingComponent } from 'src/app/add-meeting/add-meeting.component';
import { MatDialog } from '@angular/material/dialog';
import { User } from 'src/app/classes/user';
import { MatTableDataSource } from '@angular/material/table';
@Component({
  selector: 'app-expert-meetings',
  templateUrl: './expert-meetings.component.html',
  styleUrls: ['./expert-meetings.component.scss'],
})
export class ExpertMeetingsComponent implements OnInit {
  expert: Expert;
  detailsForm: FormGroup;
  approvedMeetigns: Meeting[] = [];
  unapprovedMeetings: Meeting[] = [];
  buttonContent: string = ' ';
  canceling: boolean = false;
  exId = 0;

  ListData: MatTableDataSource<Meeting>;
  lData: Meeting[];
  displayedColumns: string[] = [
    'Options',
'isApproved',
    'content',
    'time',
    'date',
    'title',
    'user',
  ];

  constructor(
    private formBuilder: FormBuilder,
    private meetingService: MeetingService,
    private experts: ExpertsService,
    private activatedRoute: ActivatedRoute,
    public dialog: MatDialog
  ) {
    // this.approvedMeetigns.push(new Meeting(1,27,23,"abcd",new Date("12/08/2020")));
    // this.unapprovedMeetings.push(new Meeting(2,27,24,"ABCD",new Date("01/09/2020")))
    this.activatedRoute.paramMap.subscribe((res) => {
      var exp = localStorage.getItem('user');
      if (exp != null && exp != 'null') {
        this.exId = (<User>JSON.parse(exp)).id;
      }

      this.loadMeetings();

      this.experts.getById(this.exId).subscribe(
        (res: Expert) => {
          this.expert = res[0];
        },
        (err) => console.log(err)
      );
    });
  }
  getButtonColor(isApproved: boolean) {
    let color: string = isApproved ?   'red':'green';
    return color;
  }

  loadMeetings() {
    this.meetingService.getMeetingsForExpert(this.exId).subscribe(
      (res: Meeting[]) => {
        this.ListData = new MatTableDataSource(res);
        // this.approvedMeetigns = res.filter(m => m.isApproved == true);
        // this.unapprovedMeetings = res.filter(m => m.isApproved == false);
        console.log(res);
      },
      (err) => console.log(err)
    );
  }
  ngOnInit(): void {}
  unDisabledFoem(): void {}

  changeStatusOfMeeting(meet: Meeting) {
    if (!meet.isApproved) {
      meet.isApproved = true;
      this.meetingService.approveMeeting(meet).subscribe((x)=>{
        this.loadMeetings();
      });
    } else {
      this.deleteMeeting(meet);
    }
  }
  deleteMeeting(meet: Meeting) {
    this.meetingService
      .cancelMeeting(meet)
      .subscribe((res) => (   this.loadMeetings()));
  }
  cancelMeeting(meet: Meeting) {
    meet.isApproved = false;
  }

  openDialog(m: Meeting): void {
    const dialogRef = this.dialog.open(AddMeetingComponent, {
      data: { id: m.idProf, updating: false, isExpert: true, userId: m.idUser },
    });
    dialogRef.afterClosed().subscribe((result) => {
      this.loadMeetings();
    });
  }
}
