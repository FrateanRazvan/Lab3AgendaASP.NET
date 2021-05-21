import { Component, OnInit } from '@angular/core';
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
    this.tasksService.getTasks().subscribe(t => this.tasks == t);
  }

  ngOnInit() {
    this.getTasks();
  }

}
