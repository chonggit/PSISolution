import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html'
})
export class RoleListComponent implements OnInit {

  @Input("roles") roles: Role[] = [];
  @Output() selected = new EventEmitter<Role>();

  constructor() { }

  ngOnInit(): void {
  }

  select(role: Role): void {
    this.selected.emit(role);
  }
}
