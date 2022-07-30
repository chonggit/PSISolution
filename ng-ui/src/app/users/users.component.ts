import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { empty, Observable } from 'rxjs';
import { RoleListComponent } from '../roles/role-list/role-list.component';

@Component({
  standalone: true,
  selector: 'app-users',
  templateUrl: './users.component.html',
  imports: [CommonModule, RoleListComponent]
})
export class UsersComponent implements OnInit {

  users: Observable<User[]> = empty();

  constructor() { }

  ngOnInit(): void {
  }

}
