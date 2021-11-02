import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Usuario } from 'src/app/login/interfaces/dtoUsuario.interface';
import { UsuariosService } from '../../services/usuarios.service';

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
  }

 
  miFormulario:FormGroup = this.fb.group({

    Nombre:[, [Validators.required, Validators.minLength(3)]],
    Apellido: [,Validators.required],
    Contrasenia:[,Validators.required],
    RolUsuario:[1],
    Email:[,Validators.required],
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
    console.log(this.miFormulario.value);

  }

}
