import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { TaskApiService } from './services/task-api.service';
import { TaskListComponent } from './task-list/task-list.component';
import { AddEditTaskComponent } from './add-edit-task/add-edit-task.component';
import { NotificationService } from './services/notification.service';


@NgModule({
  declarations: [
    AppComponent,
    TaskListComponent,
    AddEditTaskComponent   
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  entryComponents:[AddEditTaskComponent],
  providers: [TaskApiService, NotificationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
