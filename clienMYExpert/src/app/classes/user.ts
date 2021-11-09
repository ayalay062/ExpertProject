import { City } from "./city";

export class User {
  id: number;
  userName: string;
  userPassword: string;
  email: string;
  cityId: number;
  userType: number;
  imgUrl: string;

  // address:string;
  // lat:number;
  // lng:number;
  constructor(
    id?: number,
    userName?: string,
    userPassword?: string,
    email?: string,
    cityId?: number,
    type?: number,
    img?: string
  ) {
    this.id = id;
    this.userName = userName;
    this.userPassword = userPassword;
    this.email = email;
    this.cityId = cityId;
    this.userType = type;
    this.imgUrl = img;
  }
}
