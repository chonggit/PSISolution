import { ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { of } from 'rxjs';
import { click } from 'testing';

import { RoleListComponent } from './role-list.component';
import { RolesService } from './roles.services';

describe('RoleListComponent', () => {
  let component: RoleListComponent;
  let fixture: ComponentFixture<RoleListComponent>;
  let rolesService = jasmine.createSpyObj<RolesService>('RolesService', ['getRoles']);

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RoleListComponent]
    })
      .overrideProvider(RolesService, { useValue: rolesService })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RoleListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('生成角色列表', () => {
    rolesService.getRoles.and.returnValue(of([]))
    component.ngOnInit();
    let items = fixture.debugElement.queryAll(By.css('nz-list-item'))
    expect(items.length).toBe(0);
    rolesService.getRoles.and.returnValue(of([{ Name: 'role1' }, { Name: 'role2' }, { Name: 'role3' }]))
    component.ngOnInit();
    fixture.detectChanges();
    items = fixture.debugElement.queryAll(By.css('nz-list-item'))
    expect(items.length).toBe(3);
  });

  it('点击角色行触发 selected 事件', () => {
    let role: Role = { Name: 'role1' };
    let selectedRole: Role | undefined;
    rolesService.getRoles.and.returnValue(of([role]))
    component.ngOnInit();
    fixture.detectChanges();
    let item = fixture.debugElement.query(By.css('nz-list-item'))
    component.selected.subscribe(r => selectedRole = r);
    click(item);
    expect(selectedRole).toBe(role);
  });
});
