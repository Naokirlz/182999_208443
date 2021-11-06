import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';
import { ProyectoService } from 'src/app/proyectos/services/proyecto.service';
import { Tarea } from '../../../interfaces/tarea.interface';
import { TareaService } from '../../services/tarea.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-alta-tarea',
  templateUrl: './alta-tarea.component.html',
  styles: [
  ]
})
export class AltaTareaComponent implements OnInit {

  public proyectos:Proyecto[]=[];

  constructor(private fb: FormBuilder,
              private proyectoService:ProyectoService,
              private tareasService:TareaService,
              private messageService: MessageService) { }

  ngOnInit(): void {
    
    this.proyectoService.getProyecto()
    .subscribe(
      ((data: Array<Proyecto>) => this.result(data)),
    );

  }

  private result(data: Array<Proyecto>): void {
    this.proyectos = data;
  }

  miFormulario:FormGroup = this.fb.group({

    nombre       :[, [Validators.required, Validators.minLength(3)] ],
    costo        :[, [Validators.required, Validators.min(0)] ],
    duracion     :[, [Validators.required, Validators.min(0)] ],
    proyecto     :[, [Validators.required] ]

  })

  campoEsValido(campo:string){

    return this.miFormulario.controls[campo].errors  
           && this.miFormulario.controls[campo].touched

  }

  altaTarea(){

    if(this.miFormulario.invalid){
    
      this.miFormulario.markAllAsTouched();
      return;
    }

    const tarea:Tarea = {

      nombre: this.miFormulario.value.nombre,
      costo:  this.miFormulario.value.costo,
      duracion: this.miFormulario.value.duracion,
      proyectoId: this.miFormulario.value.proyecto,
    }

    this.tareasService.altaTareas(tarea)
      .subscribe((data:Tarea) => {
                                   this.messageService.add({severity:'success', summary: 'Listo', 
                                                            detail: 'Tarea guardada correctamente.'});
                                    this.miFormulario.reset({
                                                               RolUsuario:0
                                                            });
                                   },
                (({error}:any) => {
        
                  console.log(error.value);
                  this.messageService.add({severity:'error', summary: 'Error', detail: 'resp.message'});
                                  }
                                  )
                ); 
  }

}
