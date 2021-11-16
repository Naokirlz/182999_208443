import { Component, Injectable, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Incidente } from 'src/app/interfaces/incidente.interface';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';
import { LoginService } from 'src/app/login/services/login.service';
import { ProyectoService } from 'src/app/proyectos/services/proyecto.service';
import { IncidentesService } from '../../service/incidentes.service';
import { Usuario } from '../../../interfaces/dtoUsuario.interface';
import { MenuItem } from 'primeng/api';
import { EstadosService } from 'src/app/estados/service/estados.service';


@Component({
  selector: 'app-detalle-bug',
  templateUrl: './detalle-bug.component.html',
  styles: [
  ]
})
export class DetalleBugComponent implements OnInit {

  public incidenteId: number = 0;
  public incidente:Incidente[]=[];
  public proyectos:Proyecto[]=[];
  public desarrolladores: Usuario[] = [];
  public nombreUsu:string = '';
  public desarrolladorId = 0;
  public proyectoId = 0;
  public resuelto = true;
  public estado:boolean = false;

  public o1 = this.incidenteService.getBy(this.incidenteId);
  public o2 = this.proyectoService.getProyecto();
  public tester:boolean = false;

  constructor(private _route: ActivatedRoute,
              private _router: Router,
              private proyectoService: ProyectoService,
              private loginService: LoginService,
              private incidenteService:IncidentesService,
              private estadosService: EstadosService,
              private fb: FormBuilder,
              private messageService: MessageService,
              ) {

              this.tester = this.loginService.isTesterIn();
              this.incidenteId = this._route.snapshot.params['incidenteId'];

              }



  ngOnInit(): void {

    this.incidenteId = this._route.snapshot.params['incidenteId'];
    
    this.incidenteService.getBy(this.incidenteId)
      .subscribe({
        next: data => {
          this.incidente.push(data)
          this.desarrolladorId = data.DesarrolladorId!;
          this.proyectoId = data.ProyectoId!;
          this.resuelto = (data.estadoIncidente == 2) ? true : false;
          this.estado = (data.estadoIncidente == 2) ? true : false;
        }             
      });

      this.proyectoService.getProyecto()
      .subscribe(
        ((data: Array<Proyecto>) => this.proyectos = data),
      );

      this.cargarBreadcrumb();
  }

  cambiarEstado():void{
    this.estado = !this.estado;
    console.log(this.estado);
  }
          
  home: MenuItem = { icon: 'pi pi-home', routerLink: '/' };
  public items: MenuItem[] = [ ];

  cargarBreadcrumb() :void {
    if(this.tester){
      this.items.push({ label: 'Incidentes', routerLink: '/tester/incidentes' });
      this.items.push({ label: 'Detalles del Incidente' });
    } else{
      this.items.push({ label: 'Incidentes', routerLink: '/desarrollador/incidentes' });
      this.items.push({ label: 'Detalles del Incidente' });
    }
  }


  miFormulario: FormGroup = this.fb.group({
    nombre: [{value: '', disabled: !this.tester}, [Validators.required, Validators.minLength(3), Validators.maxLength(25)]],
    descripcion: [{value: '', disabled: !this.tester}, [Validators.required]],
    version: [{value: '', disabled: !this.tester}, [Validators.required , Validators.min(0)]],
    duracion: [, [Validators.required , Validators.min(0)]],
  })

  campoEsValido(campo: string) {
    return this.miFormulario.controls[campo].errors
      && this.miFormulario.controls[campo].touched
  }

  modificar() : void{
    if (this.miFormulario.invalid) {
      this.miFormulario.markAllAsTouched();
      return;
    }

    if(this.estado && parseInt(this.miFormulario.value.duracion) === 0){
      this.messageService.add({severity:'error', summary:'Error', detail:'Si resuelve el incidente la duración no puede ser 0'});
      return;
    }

    const aModificar: Incidente = {
      Nombre: this.miFormulario.value.nombre,
      Descripcion: this.miFormulario.value.descripcion,
      Version: this.miFormulario.value.version,
      Duracion: parseInt(this.miFormulario.value.duracion),
      Id: this.incidente[0].id,
      ProyectoId: this.incidente[0].proyectoId,
      EstadoIncidente: this.estado ? 2 : 1
    }
    
    if(this.tester){
      this.incidenteService.modificarIncidente(aModificar)
        .subscribe({
          next: data => {
            this.messageService.add({
              severity: 'success', summary: 'Listo',
              detail: 'Incidente Actualizado correctamente.'
            });
            this.volver();
          },
          error: error => {
            console.log(error.error);
            this.messageService.add({ severity: 'error', summary: 'Error', detail: error.error });
          }
        });
    } else {
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

  obtenerEstado(id:number):string{
    if(id === 2) {
      this.resuelto = true;
      return 'Resuelto'
    };
    this.resuelto = false;
    return 'Activo'
  }

  obtenerDesarrollador(id:number):string{
    return  this.proyectos[0].asignados?.find(u => u.id === id )?.nombreUsuario!;
  }

  volver(){
    if(this.tester){
      this._router.navigate(['/tester/incidentes']);
    }
    else{this._router.navigate(['/desarrollador/incidentes']);}
  }
}


