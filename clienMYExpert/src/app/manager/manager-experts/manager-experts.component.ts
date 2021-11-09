import { Component, OnInit, ViewChild } from '@angular/core';
import { Expert } from 'src/app/classes/expert';
import { ExpertsService } from 'src/app/services/experts.service';
import { SubjectsService } from 'src/app/services/subjects.service';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-manager-experts',
  templateUrl: './manager-experts.component.html',
  styleUrls: ['./manager-experts.component.scss'],
})
export class ManagerExpertsComponent implements OnInit {
  pageEvent: PageEvent;
  pageIndex: number;
  length: number;
  isChecked = true;
  allExperts: Expert[];

  public array: any;

  public pageSize = 10;
  public currentPage = 0;
  public totalSize = 0;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  constructor(
    private expertsService: ExpertsService,
    private subjectService: SubjectsService
  ) {}
  handlePage(e: any) {
    this.currentPage = e.pageIndex;
    this.pageSize = e.pageSize;
    this.iterator();
  }

  // getArray() {
  //   this.app.getDeliveries().subscribe((response) => {
  //     this.dataSource = new MatTableDataSource<Element>(response);
  //     this.dataSource.paginator = this.paginator;
     
  //   });
  // }

  iterator() {
    const end = (this.currentPage + 1) * this.pageSize;
    const start = this.currentPage * this.pageSize;
    const part = this.array.slice(start, end);
    this.allExperts = part;
  }

  toggleText(): string {
    if (this.isChecked) return 'לחץ להשבתה';
    return 'לחץ להפעלה';
  }
  ngOnInit(): void {
  this.getExperts();
  }

  getExperts(){
    this.expertsService.getAllExpertsAdmin().subscribe(
      (res: any) => {
        this.allExperts = res;
        this.array = res;
        this.totalSize = this.array.length;
        this.iterator();
      },
      (err) => {
        console.log(err);
      }
    );
  }
  getSubjectName(id: number): string {
    return this.subjectService.getSubjectById(id);
  }
  changeStatus(id: number, newStatus: boolean) {
    this.expertsService.changeStatus(id, newStatus).subscribe(
      (res) => {
        this.getExperts();     
      },
      (err) => {
        console.log(err);
      }
    );
  }
  // public getServerData(event?: PageEvent) {
  //   this.expertsService.getPerPage(event.pageSize, event.pageIndex).subscribe(
  //     (res: any) => {
  //       this.allExperts = res.results;
  //       this.pageIndex = res.pagination.current;
  //       this.pageSize = res.pagination.perPage;
  //       this.length = res.pagination.length;
  //     },
  //     (err) => {
  //       console.log(err);
  //     }
  //   );
  //   return event;
  // }
}
