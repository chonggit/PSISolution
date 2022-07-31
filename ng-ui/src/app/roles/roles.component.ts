import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoleListComponent } from './role-list/role-list.component';
import { HeaderEventService } from 'app/services/header-event.service';
import { RolesService } from './roles.services';
import {  RoleEditComponent } from './role-edit/role-edit.component';
import { RoleAddComponent } from './role-edit/role-add.component';

@Component({
  selector: 'div[app-roles]',
  standalone: true,
  imports: [
    CommonModule,
    RoleListComponent,
    RoleEditComponent,
    RoleAddComponent
  ],
  providers: [RolesService, HeaderEventService],
  templateUrl: './roles.component.html'
})
export class RolesComponent implements OnInit {

  constructor(private headerEventService: HeaderEventService,
    private rolesService: RolesService) { }

  ngOnInit(): void {
  }
}
