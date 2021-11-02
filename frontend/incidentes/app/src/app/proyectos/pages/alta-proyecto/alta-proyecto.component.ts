import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Proyecto } from '../../../interfaces/proyecto.interface';
import { ProyectoService } from '../../services/proyecto.service';

@Component({
  selector: 'app-alta-proyecto',
  templateUrl: './alta-proyecto.component.html',
  styles: [
  ]
})
export class AltaProyectoComponent implements OnInit {

  constructor(private fb: FormBuilder,
              private proyectoService:ProyectoService) { }

  ngOnInit(): void {
  }

  miFormulario:FormGroup = this.fb.group({

    nombre       :[, [Validators.required, Validators.minLength(3)] ],

  })

  campoEsValido(campo:string){

    return this.miFormulario.controls[campo].errors  
           && this.miFormulario.controls[campo].touched

  }

  altaProyecto(){

    if(this.miFormulario.invalid){
    
      this.miFormulario.markAllAsTouched();
      return;
    }
    console.log(this.miFormulario.value);

    const proyecto:Proyecto = {

      nombre: this.miFormulario.value.nombre

    }

    this.proyectoService.alta(proyecto);

    this.miFormulario.reset(      {
      RolUsuario:1
    });

  }

}
