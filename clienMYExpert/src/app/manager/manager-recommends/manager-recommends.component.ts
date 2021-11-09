import { Component, OnInit, ViewChild } from '@angular/core';
import { Recommend } from 'src/app/classes/recommend';
import { RecommendsService } from 'src/app/services/recommends.service';
import { UsersService } from 'src/app/services/users.service';
import { ExpertsService } from 'src/app/services/experts.service';
import { User } from 'src/app/classes/user';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-manager-recommends',
  templateUrl: './manager-recommends.component.html',
  styleUrls: ['./manager-recommends.component.scss'],
})
export class ManagerRecommendsComponent implements OnInit {
  pageEvent: PageEvent;
  pageIndex: number;
  length: number;
  recommends: Recommend[];
  user: User;
  expert: User;
  users: User[];
  experts: User[];
  public array: any;

  public pageSize = 10;
  public currentPage = 0;
  public totalSize = 0;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  constructor(
    private recommendService: RecommendsService,
    private userService: UsersService,
    private expertService: ExpertsService
  ) {
    // this.recommendService.getAllRecommends().subscribe( (res:Recommend[]) => this.recommends = res); --slashed today wednesday, return if not working
  }
  handlePage(e: any) {
    this.currentPage = e.pageIndex;
    this.pageSize = e.pageSize;
    this.iterator();
  }

  iterator() {
    const end = (this.currentPage + 1) * this.pageSize;
    const start = this.currentPage * this.pageSize;
    const part = this.array.slice(start, end);
    this.recommends = part;
  }
  ngOnInit(): void {
    this.getAllRecommends();
  }

  getAllRecommends() {
    this.recommendService.getAllRecommends().subscribe(
      (res: any) => {
        this.recommends = res;
        this.array = res;
        this.totalSize = this.array.length;
        this.iterator();
      },
      (err) => {
        console.log(err);
      }
    );
  }
  fullStars(scores): number[] {
    let arr: number[] = [];
    let full, empty, i: number;
    full = Math.floor(scores);
    empty = Math.floor(5 - scores);
    for (i = 0; i < full; i++) arr.push(1);
    if (full + empty < 5) arr.push(0.5);
    for (i = 0; i < empty; i++) arr.push(0);
    return arr;
  }
  changeStatus(r: Recommend) {
    r.isApproved = !r.isApproved;
    r.user = null;
    r.expert = null;
    this.recommendService.changeStatus(r).subscribe((x=>{
      this.getAllRecommends();
    }));
   
  }

  getButtonColor(isApproved: boolean) {
    let color: string = isApproved ? 'green' : 'red';
    return color;
  }

  // public getServerData(event?: PageEvent) {

  //   this.recommendService.getPerPage(event.pageSize, event.pageIndex).subscribe(
  //     (res: any) => {
  //       this.recommends = res.results;
  //       this.pageIndex = res.pagination.current;
  //       this.pageSize = res.pagination.perPage;
  //       this.length = res.pagination.length;
  //     },
  //     err => {
  //       console.log(err)
  //     }
  //   );
  //   return event;
  // }
  // #b53f83
}
