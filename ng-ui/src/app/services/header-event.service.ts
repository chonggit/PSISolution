import { Injectable, OnDestroy } from '@angular/core';
import { HeaderService } from 'app/services/header.service';
import { filter, Subscription } from 'rxjs';
import { HeaderEvent } from './header-event.enum';

@Injectable({
  providedIn: 'any'
})
export class HeaderEventService implements OnDestroy {

  private add: Subscription | undefined;

  private edit: Subscription | undefined;

  private remove: Subscription | undefined;

  private print: Subscription | undefined;

  private view: Subscription | undefined;

  constructor(private headerService: HeaderService) {
    this.headerService.events;
  }

  ngOnDestroy(): void {

    if (this.add) {
      this.add.unsubscribe();
    }

    if (this.edit) {
      this.edit.unsubscribe();
    }

    if (this.remove) {
      this.remove.unsubscribe();
    }

    if (this.print) {
      this.print.unsubscribe();
    }

    if (this.view) {
      this.view.unsubscribe();
    }
  }

  onAdd(complete: (() => void)): void {
    this.add = this.headerService.events.pipe(filter(value => value === HeaderEvent.ADD)).subscribe(complete);
  }

  onEdit(complete: (() => void)): void {
    this.edit = this.headerService.events.pipe(filter(value => value === HeaderEvent.EDIT)).subscribe(complete);
  }

  onRemove(complete: (() => void)): void {
    this.remove = this.headerService.events.pipe(filter(value => value === HeaderEvent.REMOVE)).subscribe(complete);
  }

  onPrint(complete: (() => void)): void {
    this.print = this.headerService.events.pipe(filter(value => value === HeaderEvent.PRINT)).subscribe(complete);
  }

  onView(complete: (() => void)): void {
    this.view = this.headerService.events.pipe(filter(value => value === HeaderEvent.VIEW)).subscribe(complete);
  }
}