import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { empty, Observable, of } from 'rxjs';
import { RolesService } from './roles.services';

@Component({
  standalone: true,
  selector: 'div[app-role-list]',
  templateUrl: './role-list.component.html',
  providers: [RolesService],
  imports: [CommonModule, HttpClientModule]
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
