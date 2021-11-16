import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';
import { LoginService } from 'src/app/login/services/login.service';
import { ProyectoService } from 'src/app/proyectos/services/proyecto.service';
import { Tarea } from '../../../interfaces/tarea.interface';
import { TareaService } from '../../services/tarea.service';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-ver-tareas',
  templateUrl: './ver-tareas.component.html',
  styles: [
  ]
})
export class VerTareasComponent implements OnInit {
  constructor(private tareaService: TareaService,
    private proyectoService: ProyectoService,
    private _router: Router,
    private messageService: MessageService,
    private loginService: LoginService,
    private _route: ActivatedRoute) {

    this.admin = this.loginService.isAdminLoggedIn();
    this.tester = this.loginService.isTesterIn();
    this.desarrollador = this.loginService.isDesarrolladorIn();
    this.proyectoId = 0;
    const p: Proyecto = {};
    this.proyecto = p;
  }

  home: MenuItem = { icon: 'pi pi-home', routerLink: '/' };
  public items: MenuItem[] = [];
  public admin: boolean = false;
  public tester: boolean = false;
  public desarrollador: boolean = false;
  public tareas: Tarea[] | undefined = [];
  private proyectos: Proyecto[] = [];
  private idTareaEliminar: number = -1;
  public totalHoras: number = 0;
  public proyectoId: number;
  public proyecto: Proyecto;
  public titulo: string = 'Administración de Tareas';

  ngOnInit(): void {
    this.proyectoId = parseInt(this._route.snapshot.params['proyectoId']);
    this.cargarBreadcrumb();
    this.actualizarTitulo();

    if (this.proyectoId) {
      this.proyectoService.getBy(this.proyectoId)
        .subscribe(
          (data: Proyecto) => {
            this.tareas = data.tareas;
            this.obtenerTotalHoras();
          }
        );
    } else {
      this.tareaService.getTareas()
        .subscribe(
          (tareas: Tarea[]) => {
            this.tareas = tareas;
            this.obtenerTotalHoras();
          }
        );

      this.proyectoService.getProyecto()
        .subscribe(
          ((data: Array<Proyecto>) => this.proyectos = data),
        );
    }
  }

  cargarBreadcrumb(): void {
    if (this.proyectoId) {
      this.items.push({ label: 'Proyectos', routerLink: '/proyectos' });
      this.items.push({ label: 'Tareas' });
    } else {
      this.items.push({ label: 'Tareas' });
    }
  }

  actualizarTitulo(): void {
    if (this.proyectoId) {
      this.titulo = 'Tareas del Proyecto';
    }
  }

  obtenerTotalHoras() {
    this.totalHoras = 0;
    this.tareas?.forEach(
      tar => this.totalHoras = (tar.duracion! + this.totalHoras)
    )
  }

  onConfirm() {
    this.messageService.clear();
    this.eliminar();
  }

  consultarAccion(id: number): void {
    this.idTareaEliminar = id;
    this.messageService.clear();
    this.messageService.add({ key: 'c', sticky: true, severity: 'warn', summary: 'Está seguro?', detail: 'Realmente desea Eliminar la Tarea' });
  }

  onReject() {
    this.messageService.clear();
    this.idTareaEliminar = -1;
  }

  removerTarea(id: number) :void {
    this.tareas!.forEach(tar => {
      if(tar.id === id) {
        this.tareas!.splice(this.tareas!.indexOf(tar), 1);
      }
    });
  }

  eliminar(): void {
    this.tareaService.deleteTarea(this.idTareaEliminar)
      .subscribe({
        next: data => {
          this.messageService.add({
            severity: 'success', summary: 'Listo',
            detail: 'Tarea eliminado correctamente.'
          });
          this.removerTarea(this.idTareaEliminar);
          this.obtenerTotalHoras();
          this.idTareaEliminar = -1;
        },
        error: error => {
          console.log(error);
          this.messageService.add({ severity: 'error', summary: 'Error', detail: error });
        }
      });
  }

  editar(id: number): void {
    this._router.navigate([`/tareas/${id}/editar`]);
  }

  obtenerNombre(id: number): string {
    const proyecto = this.proyectos.find(proyecto => proyecto.id === id);
    console.log(this.proyectos);
    return (proyecto?.nombre) ? proyecto.nombre : '';
  }

  volver() {
    this._router.navigate([`/proyectos`]);
  }
}
