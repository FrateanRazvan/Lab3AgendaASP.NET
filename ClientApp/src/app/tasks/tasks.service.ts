
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Task } from './task.model';


@Injectable({
  providedIn: 'root'
})
export class TasksService {

  private apiUrl: string;

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  getTasks(): Observable<Task[]> {
    return this.httpClient.get<Task[]>(this.apiUrl + 'Tasks');
  }

}
