import { Time } from '@angular/common';

export class Meeting {
    id: number
    idProf: number;
    idUser: number;
    title: string;
    date: Date;
    time: string;
    content: string;
    isApproved: boolean;
    constructor(id?: number, profId?: number, userId?: number, title?: string, date?: Date, time?: string, content?: string, isApproved?: boolean) {
        this.id = id;
        this.idProf = profId;
        this.idUser = userId;
        this.title = title;
        this.date = date;
        this.time = time;
        this.content = content;
        this.isApproved = isApproved
    }
}
