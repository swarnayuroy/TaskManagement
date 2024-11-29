import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserSigninComponent } from './user-signin/user-signin.component';
import { UserSignupComponent } from './user-signup/user-signup.component';
import { TaskDashboardComponent } from './task-dashboard/task-dashboard.component';

const routes: Routes = [

  {
    path: '',
    component: UserSigninComponent
  },

  {
    path: 'signUp',
    component: UserSignupComponent
  },
  {
    path: 'taskDashboard',
    component: TaskDashboardComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
