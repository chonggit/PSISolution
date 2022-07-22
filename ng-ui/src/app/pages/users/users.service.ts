import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class UsersService {
  constructor(private http: HttpClient) { }

  /**
   * 获取属于角色的用户
   * @param roleName 角色名
   */
  getUsersInRole(roleName: string): Observable<User[]> {
    return this.http.get<User[]>(`v1/users/InRole/${roleName}`);
  }
}
