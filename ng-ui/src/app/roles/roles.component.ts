import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoleListComponent } from './role-list/role-list.component';
import { HeaderEventService } from 'app/services/header-event.service';
import { RolesService } from './roles.services';
import { RoleEditComponent } from './role-edit/role-edit.component';
import { RoleAddComponent } from './role-edit/role-add.component';
import { Modal } from 'bootstrap';
import { ModalDirective } from 'app/directives/modal.directive';
import { Type } from '@angular/core';
import { RoleEditBaseComponent } from './role-edit/role-edit-base.component';
@Component({
  selector: 'div[app-roles]',
  standalone: true,
  imports: [
    CommonModule,
    RoleListComponent,
    RoleEditComponent,
    RoleAddComponent,
    ModalDirective
  ],
  providers: [RolesService, HeaderEventService],
  templateUrl: './roles.component.html'
})
export class RolesComponent implements OnInit {

  private current_selector!: string;
  private modal!: Modal;

  @ViewChild(ModalDirective, { static: true }) appModal!: ModalDirective;

  constructor(private headerEventService: HeaderEventService) { }

  ngOnInit(): void {
    this.headerEventService.onAdd(() => this.loadComponent(RoleAddComponent, '[app-role-add]'));
    this.headerEventService.onEdit(() => this.loadComponent(RoleEditComponent, '[app-role-edit]'))
  }

  private loadComponent(componentType: Type<any>, selector: string) {
    if (this.current_selector === selector) {
      this.modal.show();
      return;
    }
    const viewContainerRef = this.appModal.viewContainerRef;
    viewContainerRef.clear();
    viewContainerRef.createComponent(componentType);
    if (this.modal) {
      this.modal.dispose();
    }
    this.modal = new Modal(selector, {});
    this.modal.show();
  }
}
