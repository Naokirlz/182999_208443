import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';
import { ProyectoService } from 'src/app/proyectos/services/proyecto.service';
import { Tarea } from '../../../interfaces/tarea.interface';
import { TareaService } from '../../services/tarea.service';

@Component({
  selector: 'app-ver-tareas',
  templateUrl: './ver-tareas.component.html',
  styles: [
  ]
})
export class VerTareasComponent implements OnInit {

  constructor(private tareaService:TareaService,
    private proyectoService: ProyectoService,
    private messageService: MessageService) { }

  public tareas:Tarea[] = [];
  private proyectos: Proyecto[] = [];
  private idTareaEliminar: number = -1;

  ngOnInit(): void {
    this.tareaService.getTareas().subscribe(
      (tareas:Tarea[]) => {
        this.tareas = tareas;
      }
    );
    this.proyectoService.getProyecto()
    .subscribe(
      ((data: Array<Proyecto>) => this.proyectos = data),
    );
  }

  onConfirm() {
    this.messageService.clear();
    this.eliminar();
  }

  consultarAccion(id: number): void {
    this.idTareaEliminar = id;
    this.messageService.clear();
    this.messageService.add({ key: 'c', sticky: true, severity: 'warn', summary: 'Está seguro?', detail: 'Realmente desea la Tarea' });
  }

  onReject() {
    this.messageService.clear();
    this.idTareaEliminar = -1;
  }

  eliminar(): void {
    this.tareaService.deleteTarea(this.idTareaEliminar)
      .subscribe({
        next: data => {
          this.messageService.add({
            severity: 'success', summary: 'Listo',
            detail: 'Tarea eliminado correctamente.'
          });
          this.tareas = this.tareas.filter(p => p.id !== this.idTareaEliminar);
          this.idTareaEliminar = -1;
        },
        error: error => {
          console.log(error);
          this.messageService.add({ severity: 'error', summary: 'Error', detail: error });
        }
      });
  }

  obtenerNombre(id: number): string {
    const proyecto = this.proyectos.find(proyecto => proyecto.id === id);
    return (proyecto) ? proyecto.nombre : '';
  }
}
