import { Component, Inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormControl,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RecommendsService } from '../services/recommends.service';
import { Recommend } from '../classes/recommend';
import { User } from '../classes/user';
import Swal from 'sweetalert2';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-add-recommend',
  templateUrl: './add-recommend.component.html',
  styleUrls: ['./add-recommend.component.scss'],
})
export class AddRecommendComponent implements OnInit {
  recommendForm: FormGroup;
  starColor: string = 'warn';
  starCount: number = 5;
  rating: number = 4;
  idprof: number;
  iduser: number;
  cont: string = 'פירוט';
  submitted: boolean = false;
  msg: string = '';
  user: User;
  rec: Recommend;
  //2 is for okay, 1 is not okay because a recommend had been added, and 0 is not okay because a meeting never had accoured.
  isRecommended: any = 10;
  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private recommendService: RecommendsService,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.recommendForm = formBuilder.group({
      id: [0],
      content: ['', [Validators.required]],
      title: ['', [Validators.required]],
      stars: [''],
      profId: [''],
      userId: [''],
      date_posted: [''],
    });
    this.user = <User>JSON.parse(localStorage.getItem('user'));
    this.idprof = this.data.id;
  }
  get content() {
    return this.recommendForm.get('content');
  }

  get title() {
    return this.recommendForm.get('title');
  }
  ngOnInit(): void {
    this.recommendService
      .checkValidation(this.user.id, this.idprof)
      .subscribe((res) => {
        console.log(res);
        this.isRecommended = res;
      });
    this.recommendService
      .GetRecommendByUserId(this.user.id, this.idprof)
      .subscribe((res) => {
        console.log(res);
        this.rec = res;
        if (this.rec) {
          this.recommendForm = this.formBuilder.group({
            id: [this.rec.id],
            content: [this.rec.content, [Validators.required]],
            title: [this.rec.title, [Validators.required]],
            stars: [this.rec.stars + ''],
            profId: [this.rec.profId + ''],
            userId: [this.rec.userId + ''],
            date_posted: [
              new Date(new Date(this.rec.date_posted).setHours(3))
                .toISOString()
                .substring(0, 10),
            ],
          });
          this.onRatingChanged(this.rec.stars);
        }
      });
  }
  onRatingChanged(stars: number): void {
    this.rating = stars;
  }
  initSubmitted() {
    this.submitted = false;
  }
  save_recommend() {
    let user: any = JSON.parse(localStorage.getItem('user'));

    this.submitted = true;
    if (this.title.hasError('required') || this.content.hasError('required')) {
      Swal.fire('Oooops', 'לא ניתן לשלוח המלצה ריקה', 'error');

      return;
    }
    if (this.content.value.length > 500) {
      Swal.fire('Oooops', 'לא ניתן לשלוח מעל 500 תווים בתוכן', 'error');

      return;
    }
    if (!user) {
      Swal.fire('Oooops', 'עליך להרשם כדי להוסיף המלצות', 'error');

      return;
    }
    this.iduser = JSON.parse(localStorage.getItem('user')).id;

    this.recommendService.getRecommend.emit(
      new Recommend(
        this.idprof,
        this.iduser,
        this.title.value,
        this.content.value,
        this.rating,
        new Date()
      )
    );
    this.recommendForm.patchValue({
      stars: this.rating,
      userId: this.iduser,
      profId: this.idprof,
      date_posted:  new Date().toISOString().substring(0, 10),
    });
    //  date: new Date(this.date.value).toISOString().substring(0, 10),
    if (this.recommendForm.value.id && this.recommendForm.value.id != 0) {
      this.recommendService
        .UpdateRecommend(this.recommendForm.value)
        .subscribe((res) => {
          if (res) {
            Swal.fire(
              'הי',
              'ההמלצה נשלחה בהצלחה, וממתינה לאישור מערכת',
              'success'
            );
          }
          console.log(res);
          if (res == null) this.msg = 'לא ניתן להמליץ פעמיים';
        });
    } else {
      this.recommendService
        .AddRecommend(this.recommendForm.value)
        .subscribe((res) => {
          if (res) {
            Swal.fire(
              'הי',
              'ההמלצה נשלחה בהצלחה, וממתינה לאישור מערכת',
              'success'
            );
          }
          console.log(res);
          if (res == null) this.msg = 'לא ניתן להמליץ פעמיים';
        });
    }
  }
  //this.msg = 'ההמלצה נשלחה בהצלחה, וממתינה לאישור מערכת';
}
