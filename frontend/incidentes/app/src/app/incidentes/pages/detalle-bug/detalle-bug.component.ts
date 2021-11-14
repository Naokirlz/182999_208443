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
import { Observable, concat, of, pipe, forkJoin, interval} from 'rxjs';
import { map, filter, tap , take, publish, switchMap, delay} from 'rxjs/operators'


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

  public o1 = this.incidenteService.getBy(this.incidenteId);
  public o2 = this.proyectoService.getProyecto();

  public tester:boolean = false;

  constructor(private _route: ActivatedRoute,
              private _router: Router,
              private proyectoService: ProyectoService,
              private loginService: LoginService,
              private incidenteService:IncidentesService,
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
          
        }
             
      });

      this.proyectoService.getProyecto()
      .subscribe(
        ((data: Array<Proyecto>) => this.proyectos = data),
      );

  }
          
    


  miFormulario: FormGroup = this.fb.group({
    nombre: [, [Validators.required, Validators.minLength(3), Validators.maxLength(25)]],
    descripcion: [, [Validators.required]],
    version: [, [Validators.required , Validators.min(0)]],
    duracion: [, [Validators.required , Validators.min(0)]],
    //estado: [, [Validators.required]],
    //usuario: [, [Validators.required]]
  })

  campoEsValido(campo: string) {
    return this.miFormulario.controls[campo].errors
      && this.miFormulario.controls[campo].touched
  }

  modificar(){
    if (this.miFormulario.invalid) {
      this.miFormulario.markAllAsTouched();
      return;
    }

    const incidente: Incidente = {
      Nombre: this.miFormulario.value.nombre,
      Descripcion: this.miFormulario.value.descripcion,
      Version: this.miFormulario.value.version,
      Duracion: parseInt(this.miFormulario.value.duracion),
      
      
      Id: this.incidente[0].id,
      UsuarioId: this.incidente[0].desarrolladorId,
      ProyectoId: this.incidente[0].proyectoId,
      EstadoIncidente: this.incidente[0].estadoIncidente,
      
    }

    console.log(incidente);
    
    this.incidenteService.modificarIncidente(incidente)
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
  }

  obtenerEstado(id:number):string{

    if(id === 1) {
    
      this.resuelto = false;
      return 'Activo'
    
    };
    this.resuelto = true;
    return 'Resuelto'
  }

  obtenerDesarrollador(id:number):string{

    return  this.proyectos[0].asignados?.find(u => u.id === id )?.nombreUsuario!;
  }

  volver(){

    //this._router.navigate([`/proyectos`]);
    if(this.tester){
      this._router.navigate(['/tester/incidentes']);
    }
    else{this._router.navigate(['/desarrollador/incidentes']);}
    

  }



}


