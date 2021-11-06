import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Proyecto } from '../../../interfaces/proyecto.interface';
import { ProyectoService } from '../../services/proyecto.service';

@Component({
  selector: 'app-ver-proyectos',
  templateUrl: './ver-proyectos.component.html',
})

export class VerProyectosComponent implements OnInit {

  constructor(private proyectoService: ProyectoService,
    private _router: Router,
    private messageService: MessageService) { }

  public proyectos: Proyecto[] = [];
  private idProyEliminar: number = -1;

  ngOnInit(): void {
    this.proyectoService.getProyecto()
      .subscribe(
        ((data: Array<Proyecto>) => this.result(data)),
      );
  }

  onConfirm() {
    this.messageService.clear();
    this.eliminar();
  }

  consultarAccion(id: number): void {
    this.idProyEliminar = id;
    this.messageService.clear();
    this.messageService.add({ key: 'c', sticky: true, severity: 'warn', summary: 'Está seguro?', detail: 'Realmente desea el Proyecto' });
  }

  onReject() {
    this.messageService.clear();
    this.idProyEliminar = -1;
  }

  eliminar(): void {
    this.proyectoService.deleteProyecto(this.idProyEliminar)
      .subscribe({
        next: data => {
          this.messageService.add({
            severity: 'success', summary: 'Listo',
            detail: 'Proyecto eliminado correctamente.'
          });
          this.proyectos = this.proyectos.filter(p => p.id !== this.idProyEliminar);
          this.idProyEliminar = -1;
        },
        error: error => {
          console.log(error);
          this.messageService.add({ severity: 'error', summary: 'Error', detail: error });
        }
      });
  }

  editar(id: number): void {
    this._router.navigate([`/proyectos/${id}/editar`]);
  }

  asignados(id: number): void {

    this._router.navigate([`/proyectos/${id}/asignados`]);

  }

  incidentes(id: number): void {
    this._router.navigate([`/proyectos/${id}/incidentes`]);
  }

  tareas(id: number): void {
    this._router.navigate([`/proyectos/${id}/tareas`]);
  }

  private result(data: Array<Proyecto>): void {
    this.proyectos = data;
  }

}
