import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TaskApiService {

  readonly taskBaseAPIUrl = "https://localhost:7113/api";
  constructor(private http : HttpClient) { }

  getTaskList(title: string, sortBy:string, sortOder: number): Observable<any[]> {
    return this.http.get<any>(this.taskBaseAPIUrl+"/taskdetails",{params:{title:title, sortBy:sortBy,sortOrder:sortOder}});
  }

  addNewTask(data: any){
    return this.http.post(this.taskBaseAPIUrl+'/taskdetails', data);
  }

  updateTask(id: number|string, data:any){
    return this.http.put(this.taskBaseAPIUrl + `/taskdetails/${id}`, data);
  }

  deleteTask(id: number|string){
    return this.http.delete(this.taskBaseAPIUrl + `/taskdetails/${id}`);
  }
  
}
