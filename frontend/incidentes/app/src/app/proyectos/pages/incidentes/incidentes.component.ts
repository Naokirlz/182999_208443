import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';
import { Incidente } from '../../../interfaces/incidente.interface';
import { ProyectoService } from '../../services/proyecto.service';
import { EstadosService } from '../../../estados/service/estados.service';
import { MessageService } from 'primeng/api';
import { Usuario } from 'src/app/interfaces/dtoUsuario.interface';
import { UsuariosService } from 'src/app/usuarios/services/usuarios.service';

@Component({
  selector: 'app-incidentes',
  templateUrl: './incidentes.component.html',
  styles: [
  ]
})
export class IncidentesPComponent implements OnInit {

  public proyectoId: number;
  public proyectos:Proyecto[]=[];
  public incidentes:Incidente[] | undefined=[];
  public usuarios:Usuario[]=[];
  
  constructor(private proyectoService:ProyectoService,
              private estadosService:EstadosService,
              private _route: ActivatedRoute,
              private _router: Router,
              private usuarioServie:UsuariosService,
              private messageService: MessageService) 
              
              { 

              this.proyectoId = 0;

              }

  ngOnInit(): void {
    
    this.proyectoId = this._route.snapshot.params['proyectoId'];
    
    this.proyectoService.getBy(this.proyectoId)
    .subscribe(
      ((data: Proyecto) => this.result(data)),
    );

    this.usuarioServie.getUsuario()
    .subscribe(
      ((data: Array<Usuario>) => this.usuarios = data),
    );


  }

  private result(data: Proyecto): void {
    this.proyectos.push(data);
    this.incidentes = this.proyectos[0].incidentes;

  }
  obtenerEstado(id:number):string{

    if(id === 1) return 'Activo';
    return 'Resuelto'

  }

  obtenerNombre(id:number):string{

    const usuario = this.usuarios.find(usuarios => usuarios.id === id);
    return (usuario?.nombreUsuario ) ? usuario.nombreUsuario : '';
  }

  volver(){

    this._router.navigate([`/proyectos`]);

  }

}
