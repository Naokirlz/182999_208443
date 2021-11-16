import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { EstadosService } from 'src/app/estados/service/estados.service';
import { IncidentesService } from 'src/app/incidentes/service/incidentes.service';
import { Usuario } from 'src/app/interfaces/dtoUsuario.interface';
import { Incidente } from 'src/app/interfaces/incidente.interface';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';
import { LoginService } from 'src/app/login/services/login.service';
import { ProyectoService } from 'src/app/proyectos/services/proyecto.service';
import { TesterService } from '../../../tester/service/tester.service';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-mis-incidentes',
  templateUrl: './mis-incidentes.component.html',
  styles: [
  ]
})
export class MisIncidentesComponent implements OnInit {


  public usuario: number = 0;
  public usuarios: Usuario[] = [];
  public incidentes: Incidente[] | undefined = [];
  public estado: string = 'Activo';

  public idIncidenteEliminar = 0

  public idMenor: boolean = false;
  public nombreMenor: boolean = false;
  public estadoMenor: boolean = false;
  public proyectoMenor: boolean = false;

  public tester: boolean = false;
  public desarrollador: boolean = false;

  constructor(private loginService: LoginService,
    private testerService: TesterService,
    private incidenteService: IncidentesService,
    private estadosService: EstadosService,
    private messageService: MessageService,
    private _router: Router) {

    this.usuario = this.loginService.getLoginData()?.id!;
    this.tester = this.loginService.isTesterIn();
    this.desarrollador = this.loginService.isDesarrolladorIn();

  }

  ngOnInit(): void {

    this.usuario = this.loginService.getLoginData()?.id!;
    this.subscribirseLista();
  }

  subscribirseLista() : void{
    this.incidenteService.getMisIncidentes()
      .subscribe(
        (data: any) => {
          this.incidentes = data;
        },
        (({ error }: any) => {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: error });
        }
        ),
        () => {}
      );
  }

  home: MenuItem = { icon: 'pi pi-home', routerLink: '/' };
  public items: MenuItem[] = [
    { label: 'Incidentes' }
  ];

  onConfirm() : void {
    this.messageService.clear();
    this.eliminar(this.idIncidenteEliminar);
  }

  onReject() : void {
    this.messageService.clear();
    this.idIncidenteEliminar = -1;
  }

  consultarAccion(id: number): void {
    this.idIncidenteEliminar = id;
    this.messageService.clear();
    this.messageService.add({ key: 'c', sticky: true, severity: 'warn', summary: 'Está seguro?', detail: 'Realmente desea el Incidente' });
  }


  eliminar(id: number) : void {
    this.testerService.delete(id)
      .subscribe({
        next: data => {
          this.messageService.add({
            severity: 'success', summary: 'Listo',
            detail: 'Incidente eliminado correctamente.'
          });
          this.incidentes = this.incidentes!.filter(p => p.id !== this.idIncidenteEliminar);
          this.idIncidenteEliminar = -1;
        },
        error: error => {
          console.log(error);
          this.messageService.add({ severity: 'error', summary: 'Error', detail: error });
        }
      });
  }

  obtenerEstado(id: number): string {
    if (id === 0 || id === 1) return 'Activo';
    return 'Resuelto'
  }

  filtroId() : void {
    if (!this.idMenor) {
      this.incidentes?.sort((a, b) => a.id! - b.id!);
      this.idMenor = true;
    } else {
      this.incidentes?.sort((a, b) => b.id! - a.id!);
      this.idMenor = false;
    }
  }

  filtroEstado() : void {
    if (!this.estadoMenor) {
      this.incidentes?.sort((a, b) => a.estadoIncidente! - b.estadoIncidente!);
      this.estadoMenor = true;
    } else {
      this.incidentes?.sort((a, b) => b.estadoIncidente! - a.estadoIncidente!);
      this.estadoMenor = false;
    }
  }

  filtroProyecto() : void {

    if (!this.proyectoMenor) {
      this.incidentes?.sort((a, b) => a.NombreProyecto!.localeCompare(b.NombreProyecto!));
      this.proyectoMenor = true;
    } else {
      this.incidentes?.sort((a, b) => b.NombreProyecto!.localeCompare(a.NombreProyecto!));
      this.proyectoMenor = false;
    }
  }

  filtroNombre() : void {

    if (!this.nombreMenor) {
      this.incidentes?.sort((a, b) => a.nombre!.localeCompare(b.nombre!));
      this.nombreMenor = true;
    } else {
      this.incidentes?.sort((a, b) => b.nombre!.localeCompare(a.nombre!));
      this.nombreMenor = false;
    }
  }

  detalle(id: number): void {
    if (this.tester) {
      this._router.navigate([`/tester/incidentes/${id}`]);
    } else {
      this._router.navigate([`/desarrollador/incidentes/${id}`]);
    }
  }


  resolver(ide: number) : void {
    const incidente: Incidente = this.incidentes?.find(i => i.id == ide)!;

    if (incidente.estadoIncidente == 1 || incidente.EstadoIncidente ==0) { incidente.estadoIncidente = 2 }
    else { incidente.estadoIncidente = 1 }

    const aModificar: Incidente = {
      Nombre: incidente.nombre,
      Descripcion: incidente.descripcion,
      Version: incidente.version,
      Duracion: incidente.duracion,
      Id: incidente.id,
      ProyectoId: incidente.proyectoId,
      EstadoIncidente: incidente.estadoIncidente,
      DesarrolladorId: this.usuario
    }

    this.estadosService.put(aModificar)
      .subscribe((data: Incidente) => {
        this.messageService.add({ severity: 'success', summary: 'Listo', detail: "Incidente actualizado con éxito." });
      },
        (({ error }: any) => {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: error });
        }
        )
      );
  }
}
