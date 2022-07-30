import { Injectable } from '@angular/core';
import { HeaderEvent } from 'app/services/header-event.enum';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HeaderService {

  private eventSubject = new Subject<string>();

  events = this.eventSubject.asObservable();

  constructor() { }

  add(): void {
    this.eventSubject.next(HeaderEvent.ADD);
  }

  edit(): void {
    this.eventSubject.next(HeaderEvent.EDIT);
  }

  remove(): void {
    this.eventSubject.next(HeaderEvent.REMOVE);
  }

  print(): void {
    this.eventSubject.next(HeaderEvent.PRINT);
  }

  view(): void {
    this.eventSubject.next(HeaderEvent.VIEW);
  }
}
