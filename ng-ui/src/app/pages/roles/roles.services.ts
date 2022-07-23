import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

const baseUri = 'v1/roles';

@Injectable()
export class RolesService {

  constructor(private http: HttpClient) { }

  /** 获取所有角色*/
  getRoles(): Observable<Role[]> {
    return this.http.get<Role[]>(baseUri);
  }

  /**
   * 获取角色
   * @param id 角色 Id
   */
  getRole(id: number): Observable<Role> {
    return this.http.get<Role>(`${baseUri}/${id}`);
  }

  /**
   * 添加角色
   * @param role 角色
   */
  addRole(role: Role) {
    return this.http.post(baseUri, role);
  }

  /**
   * 删除角色
   * @param role 角色
   */
  deleteRole(role: Role) {
    return this.http.delete(`${baseUri}/${role.Id}`);
  }

  /**
   * 更新角色
   * @param role 角色
   */
  updateRole(role: Role) {
    return this.http.patch(baseUri, role);
  }
}
