import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Task } from './task.model';


@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {

  public tasks: Task[];

  constructor(http: HttpClient, @Inject('API_URL') apiUrl: string) {
    http.get<Task[]>(apiUrl + 'tasks').subscribe(result => {
      this.tasks = result;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}
