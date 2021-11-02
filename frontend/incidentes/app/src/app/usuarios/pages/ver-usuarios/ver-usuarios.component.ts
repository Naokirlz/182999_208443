import { Component, OnInit } from '@angular/core';
import { Usuario } from 'src/app/interfaces/dtoUsuario.interface';
import { UsuariosService } from '../../services/usuarios.service';

@Component({
  selector: 'app-ver-usuarios',
  templateUrl: './ver-usuarios.component.html',
  styles: [
  ]
})
export class VerUsuariosComponent implements OnInit {

  constructor(private usuarioServie:UsuariosService) { }

  public usuarios:Usuario[]=[];

  ngOnInit(): void {

          this.usuarioServie.getUsuario()
          .subscribe(
            ((data: Array<Usuario>) => this.result(data)),
          );
  }

  private result(data: Array<Usuario>): void {
    this.usuarios = data;
  }

}
