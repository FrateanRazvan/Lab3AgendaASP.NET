import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Task } from '../task.model';
import { TasksService } from '../tasks.service';

@Component({
  selector: 'app-list-tasks',
  templateUrl: './tasks-list.component.html',
  styleUrls: ['./tasks-list.component.css']
})
export class TasksListComponent implements OnInit {

  

  public tasks: Task[];

  constructor(private tasksService: TasksService) {

  }

  getTasks() {
    this.tasksService.getTasks().subscribe(t => this.tasks = t);
  }

  //constructor(http: HttpClient, @Inject('API_URL') apiUrl: string) {
  //  http.get<Task[]>(apiUrl + 'tasks').subscribe(result => {
  //    this.tasks = result;
  //  }, error => console.error(error));
  //} 

  ngOnInit() {
    this.getTasks();
  }

}
