export class Subject {
    id:number;
    subName:string;
    parent?:number;
    constructor(id?:number,subName?:string,parent?:number){
        this.id=id;
        this.subName=subName;
        this.parent=parent;
    }
}
