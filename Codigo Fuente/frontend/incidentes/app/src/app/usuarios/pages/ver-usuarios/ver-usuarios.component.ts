import { Component, OnInit } from '@angular/core';
import { Usuario } from 'src/app/interfaces/dtoUsuario.interface';
import { UsuariosService } from '../../services/usuarios.service';
import { MenuItem, MessageService } from 'primeng/api';

@Component({
  selector: 'app-ver-usuarios',
  templateUrl: './ver-usuarios.component.html',
  styles: [
  ]
})
export class VerUsuariosComponent implements OnInit {

  constructor(private usuarioServie: UsuariosService,
    private messageService: MessageService) { }

  home: MenuItem = { icon: 'pi pi-home', routerLink: '/' };
  public items: MenuItem[] = [
    { label: 'Usuarios', routerLink: '/usuarios' },
  ];
  public usuarios: Usuario[] = [];

  ngOnInit(): void {

    this.usuarioServie.getUsuario()
      .subscribe(
        ((data: Array<Usuario>) => this.usuarios = data),
      ),
      (({ error }: any) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error });
      }
      );
  }
}
