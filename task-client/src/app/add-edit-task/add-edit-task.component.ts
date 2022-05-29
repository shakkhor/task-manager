import { Component, Input, OnInit } from '@angular/core';
import TaskDetail from '../models/task.interface';
import { TaskApiService } from '../services/task-api.service';
@Component({
  selector: 'app-add-edit-task',
  templateUrl: './add-edit-task.component.html',
  styleUrls: ['./add-edit-task.component.css']
})
export class AddEditTaskComponent implements OnInit {

  @Input() taskDetail: TaskDetail = {
    id : '',
    title: '',
    status: 0,
    progress: 0,
    createdDate: new Date()
  };
  @Input() isNewTask : boolean = false 
  constructor(
    private taskAPI : TaskApiService,) { }
  
  ngOnInit(): void {
  }
  saveNewTask(){
    console.log(this.taskDetail);
    if(this.isNewTask){
      this.taskAPI.addNewTask(this.taskDetail).subscribe(_ => {
        console.log("task saved");
      });
    }
    else{
      this.taskAPI.updateTask(this.taskDetail.id, this.taskDetail).subscribe(_ => {
        console.log("task updated");
      });
    }    
  }
}
