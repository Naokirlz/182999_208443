import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';
import { Incidente } from '../../../interfaces/incidente.interface';
import { IncidentesService } from '../../../incidentes/service/incidentes.service';
import { MessageService } from 'primeng/api';
import { LoginService } from 'src/app/login/services/login.service';
import { ProyectoService } from '../../../proyectos/services/proyecto.service';

@Component({
  selector: 'app-altabug-tester',
  templateUrl: './altabug-tester.component.html',
  styles: [
  ]
})
export class AltabugTesterComponent implements OnInit {

  public proyectos: Proyecto[] = [];
  public usuario:number = 0;
  
  constructor(private fb: FormBuilder,
              private incidenteService:IncidentesService,
              private messageService: MessageService,
              private proyectoService:ProyectoService,
              private loginService: LoginService,) { }

  ngOnInit(): void {

    this.usuario = this.loginService.getLoginData()?.id!;
  
    this.proyectoService.getProyecto()
    .subscribe(
      (data: Proyecto[]) => {
            this.result(data);
            
      },
      (({error}:any) => {
        alert(error);
        console.log(JSON.stringify(error));
      }
      )
    );
  }

  private result(data: Array<Proyecto>): void {
            
    this.proyectos = data;

  }

  miFormulario: FormGroup = this.fb.group({
    nombre: [, [Validators.required, Validators.minLength(3), Validators.maxLength(25)]],
    proyecto: [, [Validators.required, Validators.min(0)]],
    descripcion: [, [Validators.required, Validators.min(0)]],
    version: [, [Validators.required]],
    //estado: [, [Validators.required]],
    //usuario: [, [Validators.required]]
  })


  campoEsValido(campo: string) {
    return this.miFormulario.controls[campo].errors
      && this.miFormulario.controls[campo].touched
  }

  altaIncidente(): void {
    
    if (this.miFormulario.invalid) {
      this.miFormulario.markAllAsTouched();
      return;
    }

    const incidente: Incidente = {
      Nombre: this.miFormulario.value.nombre,
      ProyectoId: parseInt(this.miFormulario.value.proyecto),
      Descripcion: this.miFormulario.value.descripcion,
      Version: this.miFormulario.value.version,
      EstadoIncidente: 1,
      UsuarioId: this.usuario,
    }

    console.log(incidente);
    
    this.incidenteService.altaIncidente(incidente)
      .subscribe((data: Incidente) => {
        this.messageService.add({
          severity: 'success', summary: 'Listo',
          detail: 'Incidente guardado correctamente.'
        });
        this.miFormulario.reset();
      },
        (({ error }: any) => {
          
          this.messageService.add({ severity: 'error', summary: 'Error', detail: error });
        }
        )
      );
  }

}
