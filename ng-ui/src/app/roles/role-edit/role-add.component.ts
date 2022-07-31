import { Component, HostBinding, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RolesService } from '../roles.services';
import { RoleEditBaseComponent } from './role-edit-base.component';


@Component({
  selector: 'div[app-role-add]',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  providers: [RolesService],
  templateUrl: './role-edit.component.html'
})
export class RoleAddComponent extends RoleEditBaseComponent implements OnInit {

  title = '添加角色';

  @HostBinding('class') className = 'modal fade';

  constructor(fb: FormBuilder, private rolesService: RolesService) { super(fb); }

  ngOnInit(): void {
    super.onInit();
  }

  onSubmit(): void {

    const role: Role = { ...this.roleForm.value };

    this.rolesService.addRole(role).subscribe();
  }
}
