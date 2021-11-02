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
    Apellido: [],
    Contrasenia:[],
    RolUsuario:[1],
    Email:[],
    NombreUsuario:[]

  })


  altaUsuario(){


  }

}
