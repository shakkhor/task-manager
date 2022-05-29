import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  readonly taskBaseAPIUrl = "https://localhost:7113/api";
  constructor(private http : HttpClient) { }
}
