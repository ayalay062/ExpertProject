import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'israelidate',
})
export class IsraelidatePipe implements PipeTransform {
  transform(value: Date, ...args: unknown[]): string {
    if (!value || value == null) {
      return '';
    }
  //  var dt = new Date(value);
    let date: string = value.toString().substring(8, 10);
    let month: string = value.toString().substring(5, 7);
    let year: string = value.toString().substring(0, 4);
    let day: number = Number(date);
    let hebrewMonths: string[] = [
      'ינואר',
      'פברואר',
      'מרץ',
      'אפריל',
      'מאי',
      'יוני',
      'יולי',
      'אוגוסט',
      'ספטמבר',
      'אוקטובר',
      'נובמבר',
      'דצמבר',
    ];
    let hebrewDate: string =
      day + ' ב' + hebrewMonths[Number(month) - 1] + ', ' + year;
    return hebrewDate;
  }
}
