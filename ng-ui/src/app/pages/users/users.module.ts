import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersComponent } from './users.component';
import { UsersRoutingModule } from './users-routing.module';
import { RoleListComponent } from '../roles/role-list.component';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzListModule } from 'ng-zorro-antd/list';

@NgModule({
  declarations: [
    UsersComponent,
    RoleListComponent
  ],
  imports: [
    CommonModule,
    UsersRoutingModule,
    NzTableModule,
    NzListModule
  ]
})
export class UsersModule { }
