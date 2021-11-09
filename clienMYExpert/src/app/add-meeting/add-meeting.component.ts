import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MeetingService } from '../services/meeting.service';
import { Meeting } from '../classes/meeting';
import { ActivatedRoute } from '@angular/router';
import { Expert } from '../classes/expert';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { User } from '../classes/user';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-add-meeting',
  templateUrl: './add-meeting.component.html',
  styleUrls: ['./add-meeting.component.scss'],
})
export class AddMeetingComponent implements OnInit {
  addMeetingForm: FormGroup =  this.formBuilder.group({ id: ['0']});
  expertId: number;
  userId: number;
  existedMeeting: Meeting = null;
  dateSent = new Date();
  constructor(
    private formBuilder: FormBuilder,
    private meetingService: MeetingService,
    private activatedRoute: ActivatedRoute,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}
  get time() {
    return this.addMeetingForm.get('time');
  }
  get date() {
    return this.addMeetingForm.get('date');
  }
  get title() {
    return this.addMeetingForm.get('title');
  }
  get content() {
    return this.addMeetingForm.get('content');
  }
  ngOnInit(): void {
    let user = localStorage.getItem('user');
    let userData = JSON.parse(user);
    this.userId = userData.id;
    this.expertId = this.data.id;
    if (!this.data.updating) {
      this.addMeetingForm = this.formBuilder.group({
        id: [''],
        time: [
          this.existedMeeting ? this.existedMeeting.time : '',
          [Validators.required],
        ],
        date: [
          this.existedMeeting
       ?   new Date(new Date(this.existedMeeting.date).setHours(3)).toISOString().substring(0, 10)
           
            : new Date(new Date().setHours(3)).toISOString().substring(0, 10),
          [Validators.required],
        ],
        title: [
          this.existedMeeting ? this.existedMeeting.title : '',
          [Validators.required],
        ],
        content: [
          this.existedMeeting ? this.existedMeeting.content : '',
          [Validators.required],
        ],
        idProf: [this.expertId + '', [Validators.required]],
        idUser: [this.userId + '', [Validators.required]],
      });
    }
    if (this.data.updating) {
      this.meetingService.findUserMeeting(this.userId, this.expertId).subscribe(
        (meet) => {
          this.existedMeeting = meet;
          this.addMeetingForm = this.formBuilder.group({
            id: [''],
            time: [
              this.existedMeeting ? this.existedMeeting.time : '',
              [Validators.required],
            ],
            date: [
              this.existedMeeting
              ?   new Date(new Date(this.existedMeeting.date).setHours(3)).toISOString().substring(0, 10)
           
              : new Date(new Date().setHours(3)).toISOString().substring(0, 10),
              [Validators.required],
            ],
            title: [
              this.existedMeeting ? this.existedMeeting.title : '',
              [Validators.required],
            ],
            content: [
              this.existedMeeting ? this.existedMeeting.content : '',
              [Validators.required],
            ],
            idProf: [this.expertId + '', [Validators.required]],
            idUser: [this.userId + '', [Validators.required]],
          });
        },
        (err) => console.log(err)
      );
    }
    if (this.data.isExpert) {
      this.meetingService
        .findUserMeeting(this.data.userId, this.expertId)
        .subscribe(
          (meet) => {
            this.existedMeeting = meet;
            this.addMeetingForm = this.formBuilder.group({
              id: [''],
              time: [
                this.existedMeeting ? this.existedMeeting.time : '',
                [Validators.required],
              ],
              date: [
                this.existedMeeting
                ?   new Date(new Date(this.existedMeeting.date).setHours(3)).toISOString().substring(0, 10)
           
                : new Date(new Date().setHours(3)).toISOString().substring(0, 10),
                [Validators.required],
              ],
              title: [
                this.existedMeeting ? this.existedMeeting.title : '',
                [Validators.required],
              ],
              content: [
                this.existedMeeting ? this.existedMeeting.content : '',
                [Validators.required],
              ],
              idProf: [this.expertId + '', [Validators.required]],
              idUser: [this.userId + '', [Validators.required]],
            });
          },
          (err) => console.log(err)
        );
    }
  }
  saveMeeting(): void {
    let meet = new Meeting();

    meet.content = this.content.value;
    meet.title = this.title.value;
    // meet.date = new Date(this.date.value).toISOString().substring(0, 10);
    meet.time = this.time.value + ':00';
    meet.idProf = this.expertId;
    meet.idUser = this.userId;

    if (this.existedMeeting) {
      this.addMeetingForm.patchValue({
        date: new Date(this.date.value).toISOString().substring(0, 10),
        id: this.existedMeeting.id,
        idProf: this.expertId,
        idUser: this.userId,
      });
      meet.id = this.existedMeeting.id;
      this.meetingService.updateMeeting(this.addMeetingForm.value).subscribe(
        (res) => {
          if (res) {
            Swal.fire('הי', 'השמירה בוצעה בהצלחה', 'success');
            this.meetingService.getExistedMeeting.emit(meet);
          }
        },
        (err) => console.log(err)
      );
    } else {
      meet.isApproved = false;
      meet.id = 0;
      this.addMeetingForm.patchValue({
        date: new Date(this.date.value).toISOString().substring(0, 10),
        id: 0,
        idProf: this.expertId,
        idUser: this.userId,
      });
      this.meetingService.addMeeting(this.addMeetingForm.value).subscribe(
        (res) => {
          if (res) {
            Swal.fire('הי', 'השמירה בוצעה בהצלחה', 'success');
            this.meetingService.getExistedMeeting.emit(meet);
          }
        },
        (err) => console.log(err)
      );
    }
  }
}
