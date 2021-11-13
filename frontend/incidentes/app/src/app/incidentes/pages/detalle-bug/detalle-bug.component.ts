import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Incidente } from 'src/app/interfaces/incidente.interface';
import { IncidentesService } from '../../service/incidentes.service';

@Component({
  selector: 'app-detalle-bug',
  templateUrl: './detalle-bug.component.html',
  styles: [
  ]
})
export class DetalleBugComponent implements OnInit {

  public incidenteId: number = 0;
  public incidente:Incidente[]=[];
  constructor(private _route: ActivatedRoute,
              private _router: Router,
              private incidenteService:IncidentesService,
              private fb: FormBuilder,
              private messageService: MessageService) { }

  ngOnInit(): void {

    this.incidenteId = this._route.snapshot.params['incidenteId'];

    this.incidenteService.getBy(this.incidenteId)
      .subscribe((data:Incidente) => this.incidente.push(data) )
    
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
      .subscribe((data: Incidente) => {
        this.messageService.add({
          severity: 'success', summary: 'Listo',
          detail: 'Incidente modificado correctamente.'
        });
        alert('exito');
        this.volver();
      },
        (({ error }: any) => {
          console.log(error);
          //this.messageService.add({ severity: 'error', summary: 'Error', detail: error });
        }
        )
      );
  }

  obtenerEstado(id:number):string{

    if(id === 1) return 'Activo';
    return 'Resuelto'

  }

  volver(){

    //this._router.navigate([`/proyectos`]);
    this._router.navigate(['/tester/incidentes']);

  }



}
