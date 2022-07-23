import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { empty, Observable, of } from 'rxjs';
import { RolesService } from './roles.services';

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  providers: [RolesService]
})
export class RoleListComponent implements OnInit {

  roles: Observable<Role[]> | undefined;
  @Output() selected = new EventEmitter<Role>();

  constructor(private rolesServices: RolesService) { }

  ngOnInit(): void {

    this.roles = this.rolesServices.getRoles();
  }

  select(role: Role): void {
    this.selected.emit(role);
  }
}
