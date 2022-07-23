import { Component, OnInit } from '@angular/core';
import { empty, Observable } from 'rxjs';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html'
})
export class UsersComponent implements OnInit {

  users: Observable<User[]> = empty();

  constructor() { }

  ngOnInit(): void {
  }

}
