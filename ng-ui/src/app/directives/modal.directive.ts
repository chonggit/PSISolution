import { Directive, ViewContainerRef } from '@angular/core';

/** modal 占位使用 */
@Directive({
  selector: '[appModal]',
  standalone: true
})
export class ModalDirective {

  constructor(public viewContainerRef: ViewContainerRef) {

   }
}
