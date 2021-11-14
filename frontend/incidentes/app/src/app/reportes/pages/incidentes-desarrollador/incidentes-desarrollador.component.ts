import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/interfaces/dtoUsuario.interface';
import { UsuariosService } from 'src/app/usuarios/services/usuarios.service';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-incidentes-desarrollador',
  templateUrl: './incidentes-desarrollador.component.html',
  styles: [
  ]
})
export class IncidentesDesarrolladorComponent implements OnInit {

  public desarrolladores:Usuario[]=[];
  
  constructor(private usuarioServie:UsuariosService,
              private _router: Router) { }

  ngOnInit(): void {
    this.usuarioServie.getUsuario()
    .subscribe(
      ((data: Array<Usuario>) => this.result(data)),
    );
  }
  
  home: MenuItem = { icon: 'pi pi-home', routerLink: '/' };
  public items: MenuItem[] = [
    { label: 'Reportes' },
  ];

  private result(data: Array<Usuario>): void {
    this.desarrolladores = data;
    this.desarrolladores = this.desarrolladores.filter(p => !(p.rolUsuario === 0 || p.rolUsuario === 2));
  }

  detalles(id:number){
    this._router.navigate([`/reportes/${id}/resueltos`]);
  }
}
