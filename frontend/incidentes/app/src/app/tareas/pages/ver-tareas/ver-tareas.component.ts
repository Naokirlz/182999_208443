import { Component, OnInit } from '@angular/core';
import { Tarea } from '../../../interfaces/tarea.interface';
import { TareaService } from '../../services/tarea.service';

@Component({
  selector: 'app-ver-tareas',
  templateUrl: './ver-tareas.component.html',
  styles: [
  ]
})
export class VerTareasComponent implements OnInit {

  constructor(private tareaService:TareaService) { }

  public tareas:Tarea[] = [];

  ngOnInit(): void {
    this.tareaService.getTareas().subscribe(
      (tareas:Tarea[]) => {
        this.tareas = tareas;
      }
    );
  }

}
