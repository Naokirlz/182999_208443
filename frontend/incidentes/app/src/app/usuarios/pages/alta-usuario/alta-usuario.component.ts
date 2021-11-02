import { Component, OnInit } from '@angular/core';
import { Usuario } from 'src/app/login/interfaces/dtoUsuario.interface';
import { UsuariosService } from '../../services/usuarios.service';

@Component({
  selector: 'app-alta-usuario',
  templateUrl: './alta-usuario.component.html',
  styles: [
  ]
})
export class AltaUsuarioComponent implements OnInit {

  constructor(private usuarioServive:UsuariosService) { }

  ngOnInit(): void {
  }

  Nombre:string = '';
  Apellido:string = '';
  Contrasenia:string = '';
  RolUsuario:string = '';
  Email:string = '';
  NombreUsuario:string = '';

  altaUsuario(){

    const usuario:Usuario ={
      NombreUsuario: this.NombreUsuario,
      Contrasenia: this.Contrasenia,
      Apellido:this.Apellido,
      Nombre:this.Nombre,
      RolUsuario:'0',
      Email:this.Email
    }

    this.usuarioServive.alta(usuario);

  }

}
