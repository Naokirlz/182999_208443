import { Component, Input, OnInit, Output,EventEmitter} from '@angular/core';
import { Usuario } from 'src/app/interfaces/dtoUsuario.interface';
import { LoginService } from '../../services/login.service';
import {InputTextModule} from 'primeng/inputtext';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-formulario',
  templateUrl: './formulario.component.html',
  styles: [
  ]
})
export class FormularioComponent implements OnInit {



  constructor(private loginService:LoginService,
              private fb: FormBuilder) { }

  ngOnInit(): void {
  }

  
  miFormulario:FormGroup = this.fb.group({

    nombre       :[, [Validators.required, Validators.minLength(3)] ],
    contrasenia       :[, [Validators.required, Validators.minLength(3)] ],

  })

  campoEsValido(campo:string){

    return this.miFormulario.controls[campo].errors  
           && this.miFormulario.controls[campo].touched

  }

  login():void {

    
    const usuario:Usuario = {

      NombreUsuario: this.miFormulario.value.nombre,
      Contrasenia: this.miFormulario.value.contrasenia

    }


    this.loginService.login(usuario);
    
    this.miFormulario.reset();
    
  }


}
