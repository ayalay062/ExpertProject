import { Injectable } from '@angular/core';
import { City } from '../classes/city';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CitiesService {
  allCities: City[] = [];
  cityCnt: number = 0;
  url = environment.api + 'City/';
  constructor(private http: HttpClient) {
    this.getAllCities().subscribe(
      (res: City[]) => {
        this.allCities = res;
      },
      (err) => err
    );
  }
  getCityByName(name: string): number {
    var city = this.allCities.filter((x) => x.name == name);
    if (!city || city.length == 0) {
      return null;
    }
    return city[0].id;
  }
  getCityById(id: number): City {
    var city = this.allCities.filter((x) => x.id == id);
    if (!city || city.length == 0) return null;
    return city[0];
  }
  getAllCities(): Observable<City[]> {
    return this.http.get<City[]>(this.url + 'GetAllCities');
  }
}
