import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: 'login', loadComponent: () => import('./login/login.component').then(c => c.LoginComponent) },
  {
    path: '', loadComponent: () => import('./layout/layout.component').then(c => c.LayoutComponent),
    children: [
      { path: 'roles', loadComponent: () => import('./roles/roles.component').then(c => c.RolesComponent) },
      { path: 'users', loadComponent: () => import('./users/users.component').then(c => c.UsersComponent) }
    ],
  }
];
