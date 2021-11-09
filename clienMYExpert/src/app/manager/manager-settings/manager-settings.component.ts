import { Component, OnInit } from '@angular/core';
import { Expert } from 'src/app/classes/expert';
import { ExpertsService } from 'src/app/services/experts.service';
import { SubjectsService } from 'src/app/services/subjects.service';

@Component({
  selector: 'app-manager-settings',
  templateUrl: './manager-settings.component.html',
  styleUrls: ['./manager-settings.component.scss']
})
export class ManagerSettingsComponent implements OnInit {
  constructor() { }

  ngOnInit(): void {
  }

}
