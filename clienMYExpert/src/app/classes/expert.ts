import { City } from './city';
import { Subject } from './subject';
import { User } from './user';
export class Expert {
  id: number;
  userName: string;
  userPassword: string;
  email: string;
  cityId: number;
  proSubject: number;
  businessName: string;
  description: string;
  imgUrl: string;
  scores: number;
  enable: boolean;
  phone: string;
  firstName: string;
  lastName: string;
  city?:City;
  subject?:Subject;
  numRecommends?:number;
  constructor(
    id?: number,
    userName?: string,
    userPassword?: string,
    email?: string,
    cityId?: number,
    proSubject?: number,
    img?: string,
    businessName?: string,
    description?: string,
    score?: number
  ) {
    this.id = id;
    this.userName = userName;
    this.userPassword = userPassword;
    this.email = email;
    this.cityId = cityId;
    this.proSubject = proSubject;
    this.imgUrl = img;
    this.businessName = businessName;
    this.description = description;
    this.scores = score;
    this.enable = true;
  }
}
