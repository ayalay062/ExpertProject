import { Component, Inject, OnInit } from '@angular/core';
import { Recommend } from 'src/app/classes/recommend';
import { RecommendsService } from '../services/recommends.service';
import { NumberValueAccessor } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-disp-recommends',
  templateUrl: './disp-recommends.component.html',
  styleUrls: ['./disp-recommends.component.scss']
})
export class DispRecommendsComponent implements OnInit {
  recommends:Recommend[] = [];

  notFound:boolean = false;
  constructor(private recommendService:RecommendsService, 
    private activatedRoute: ActivatedRoute,
    @Inject(MAT_DIALOG_DATA) public data: any)
  {

    this.recommendService.getAllApprovedRecommends(Number(this.data.id)).subscribe( (res:Recommend[]) => {this.recommends = res; if (!(res.length>0)) this.notFound = true; else this.notFound = false;} );
  }

  fullStars(r:Recommend): number[] {
    let arr: number[] = [];
    let full, empty, i: number;
    full=Math.floor(r.stars);
    empty=Math.floor(5-r.stars );
    for (i = 0; i < full; i++)
      arr.push(1);
    if (full + empty < 5)
      arr.push(0.5);
    for (i = 0; i < empty; i++)
      arr.push(0)
    return arr
  }

  ngOnInit(): void {
  }

}
