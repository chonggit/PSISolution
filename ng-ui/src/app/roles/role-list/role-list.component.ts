import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { RolesService } from '../roles.services';

@Component({
  standalone: true,
  selector: 'div[app-role-list]',
  templateUrl: './role-list.component.html',
  providers: [RolesService],
  imports: [CommonModule]
})
export class RoleListComponent implements OnInit {

  selectedRole: Role | undefined;
  roles: Observable<Role[]> | undefined;
  @Output() selected = new EventEmitter<Role>();

  constructor(private rolesServices: RolesService) { }

  ngOnInit(): void {

    this.roles = this.rolesServices.getRoles();
  }

  select(role: Role): void {
    this.selectedRole = role;
    this.selected.emit(role);
  }
}
