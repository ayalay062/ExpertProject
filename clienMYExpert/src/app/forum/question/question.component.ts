import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'src/app/classes/subject';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.scss']
})
export class QuestionComponent implements OnInit {
  submitted:boolean = false;
  questionForm:FormGroup;
  msg:string = "";
  categories:Observable<string[]>;
  constructor(private formBuilder: FormBuilder, private activatedRoute: ActivatedRoute) { 
    this.questionForm = formBuilder.group({ 
      content: ['', Validators.required],
      title: ['', Validators.required],
      userId: [''],
      categorySelect:['', Validators.required]
    })
  }

  ngOnInit(): void {
  }
  save_question(){
  }
  
  get content() {
    return this.questionForm.get("content");
  }

  get title() {
    return this.questionForm.get("title");
  }

  get categorySelect(){
    return this.questionForm.get("categorySelect");
  }

  initSubmitted(){
    this.submitted = false;
  }
}
