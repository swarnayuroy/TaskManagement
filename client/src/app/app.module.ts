import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserSigninComponent } from './user-signin/user-signin.component';
import { UserSignupComponent } from './user-signup/user-signup.component';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { TaskManagementService } from './shared/task-management.service';
import { HttpClientModule } from '@angular/common/http';
import { TaskDashboardComponent } from './task-dashboard/task-dashboard.component'


@NgModule({
  declarations: [
    AppComponent,
    UserSigninComponent,
    UserSignupComponent,
    TaskDashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    FormsModule,
    FlexLayoutModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    HttpClientModule
   ],
  providers: [TaskManagementService],
  bootstrap: [AppComponent]
})
export class AppModule { }
