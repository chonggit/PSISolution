import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', component: LayoutComponent, }
  //{
  //  path: '', component: LayoutComponent,
  //  children: [
  //    { path: '', loadChildren: () => import('./pages/welcome/welcome.module').then(m => m.WelcomeModule) },
  //    { path: 'users', loadChildren: () => import('./pages/users/users.module').then(m => m.UsersModule) }
  //  ]
  //},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
