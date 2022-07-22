import { ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { click } from 'testing';

import { RoleListComponent } from './role-list.component';

describe('RoleListComponent', () => {
  let component: RoleListComponent;
  let fixture: ComponentFixture<RoleListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RoleListComponent]
    })
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
    let items = fixture.debugElement.queryAll(By.css('nz-list-item'))
    expect(items.length).toBe(0);
    component.roles.push({ Name: 'role1' });
    component.roles.push({ Name: 'role2' });
    component.roles.push({ Name: 'role3' });
    fixture.detectChanges();
    items = fixture.debugElement.queryAll(By.css('nz-list-item'))
    expect(items.length).toBe(3);
  });

  it('点击角色行触发 selected 事件', () => {
    let role: Role = { Name: 'role1' };
    let selectedRole: Role | undefined;
    component.roles.push(role);
    fixture.detectChanges();
    let item = fixture.debugElement.query(By.css('nz-list-item'))
    component.selected.subscribe(r => selectedRole = r);
    click(item);
    expect(selectedRole).toBe(role);
  });
});
