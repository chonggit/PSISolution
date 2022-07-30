import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoleListComponent } from './role-list/role-list.component';

@Component({
  selector: 'div[app-roles]',
  standalone: true,
  imports: [
    CommonModule,
    RoleListComponent
  ],
  templateUrl: './roles.component.html'
})
export class RolesComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
