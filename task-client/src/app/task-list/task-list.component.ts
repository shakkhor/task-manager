import { Component, Injectable, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { Observable } from 'rxjs';
import { TaskApiService } from '../services/task-api.service';
import * as signalR from '@microsoft/signalr';
import TaskDetail from '../models/task.interface';
import { v4 as uuidv4 } from 'uuid';
@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {
  titleArrowUp:boolean = true;
  dateArrowUp:boolean = true;
  taskList$!:Observable<any[]>;
  taskList: any=[];
  modalTitle:string = "dummy title";
  task:any;
  title: string ='';
  taskDetail : TaskDetail = {
    id: '',
    title: '',
    status: 0,
    progress: 0,
    createdDate: new Date()
  }
  isNewTask : boolean = false;
  private hubConnection: HubConnection;

  deleteTaskId = "";
  constructor(
    private taskService : TaskApiService,
    private apiService: TaskApiService) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7113/pushNotification').build();
    }
    sortBy: string = "CreateDate";
    sortOrder: number = 0;

    
  ngOnInit(): void {
    this.taskList$ = this.taskService.getTaskList("","Title",0);
    this.hubConnection.start().then(() => {
      console.log('connection started');
    }).catch(err => console.log(err));

    this.hubConnection.onclose(() => {
      debugger;
      setTimeout(() => {
        console.log('try to re start connection');
        debugger;
        this.hubConnection.start().then(() => {
          debugger;
          console.log('connection re started');
        }).catch(err => console.log(err));
      }, 5000);
    });

    this.hubConnection.on('privateMessageMethodName', (data) => {
      debugger;
      console.log('private Message:' + data);
    });

    this.hubConnection.on('publicMessageMethodName', (data) => {
      this.taskList$ = this.taskService.getTaskList("","Title",0);
      console.log('public Message:' + data);
    });

    this.hubConnection.on('clientMethodName', (data) => {
      debugger;
      console.log('server message:' + data);
    });

    this.hubConnection.on('WelcomeMethodName', (data) => {
      debugger;
      console.log('client Id:' + data);
      this.hubConnection.invoke('GetDataFromClient', 'abc@abc.com', data).catch(err => console.log(err));
    });
  }
  openAddModal(){
    this.modalTitle = "Add new task";
    this.isNewTask = true;
    this.taskDetail = {
      id: uuidv4(),
      title: '',
      status: 0,
      progress: 0,
      createdDate: new Date()
    }
  }

  openEditModal(item : TaskDetail)
  {
    this.modalTitle = "Edit task";
    this.taskDetail = item;
    this.isNewTask = false;
  }
  deleteTask(){
    this.apiService.deleteTask(this.deleteTaskId).subscribe(_ =>{ console.log("Task Deleted")})
  }
  setDeleteTaskId(item : TaskDetail){
    this.modalTitle = "Delete task"
    this.deleteTaskId = item.id;
  }

  searchByTitle(){
    this.taskList$ = this.taskService.getTaskList(this.title,this.sortBy,this.sortOrder);
    console.table(this.taskList$)
  }
  sortByTitle(order:number){
    this.titleArrowUp = !this.titleArrowUp
    this.sortBy = "Title";
    this.sortOrder = order;
    this.taskList$ = this.taskService.getTaskList('',this.sortBy,this.sortOrder);
    
  }
  sortByDate(order:number){
    this.dateArrowUp = !this.dateArrowUp
    this.sortBy = "CreateDate";
    this.sortOrder = order;    
    this.taskList$ = this.taskService.getTaskList('',this.sortBy,this.sortOrder);    
  }
}
