import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Usuario } from 'src/app/interfaces/dtoUsuario.interface';
import { UsuariosService } from '../../services/usuarios.service';
import {InputTextModule} from 'primeng/inputtext';

@Component({
  selector: 'app-alta-usuario',
  templateUrl: './alta-usuario.component.html',
  styles: [
  ]
})
export class AltaUsuarioComponent implements OnInit {

  constructor(private usuarioServive:UsuariosService,
              private fb: FormBuilder) { }

  ngOnInit(): void {

    this.miFormulario.reset(
      {
        RolUsuario:1
      }
    )
  }

 
  miFormulario:FormGroup = this.fb.group({

    Nombre       :[, [Validators.required, Validators.minLength(3)] ],
    Apellido     : [,Validators.required],
    Contrasenia  :[,Validators.required],
    RolUsuario   :[],
    Email        :[,Validators.required],
    NombreUsuario:[,Validators.required]

  })

  campoEsValido(campo:string){
    return this.miFormulario.controls[campo].errors  
           && this.miFormulario.controls[campo].touched
  }


  altaUsuario(){

    if(this.miFormulario.invalid){
    
      this.miFormulario.markAllAsTouched();
      return;
    }
    console.log(this.miFormulario.value.Nombre);

    let numeroRol : number = parseInt(this.miFormulario.value.RolUsuario);
    const usuario:Usuario = {
    
      Nombre:this.miFormulario.value.Nombre,
      Apellido:this.miFormulario.value.Apellido,
      Contrasenia:this.miFormulario.value.Contrasenia,
      RolUsuario:numeroRol,
      Email:this.miFormulario.value.Email,
      NombreUsuario:this.miFormulario.value.NombreUsuario,

    }

    console.log(usuario);
    alert(usuario);

    let correcto:boolean = this.usuarioServive.alta(usuario);

    if (correcto) {
      this.miFormulario.reset(      {
        RolUsuario:1
      });
    }

  }

}
