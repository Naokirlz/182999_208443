import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';
import { ProyectoService } from '../../services/proyecto.service';
import { Asignado } from '../../../interfaces/asignado.interface';
import { UsuariosService } from '../../../usuarios/services/usuarios.service';
import { Usuario } from 'src/app/interfaces/dtoUsuario.interface';

@Component({
  selector: 'app-asignados',
  templateUrl: './asignados.component.html',
  styles: [
  ]
})
export class AsignadosComponent implements OnInit {


  public proyectoId: number;
  public proyectos:Proyecto[]=[];
  public asignados:Asignado[] | undefined=[];
  public noAsignados:Asignado[] | undefined=[];

  constructor(private _route: ActivatedRoute,
              private proyectoService:ProyectoService,
              private _router: Router,
              private usuariosService:UsuariosService) { 

    this.proyectoId = 0;

  }

  ngOnInit(): void {

    this.proyectoId = this._route.snapshot.params['proyectoId'];
    //let numeroRol : number = parseInt(this.proyectoId);
    
    this.proyectoService.getBy(this.proyectoId)
    .subscribe(
      ((data: Proyecto) => this.result(data)),
    );

  

  }

  editarProyecto(id:number){

    this.asignados = this.asignados!.filter(p => p.id !== id);

  }
  
  private result(data: Proyecto): void {
    this.proyectos.push(data);
    this.asignados = this.proyectos[0].asignados;
    
  }


  volver(){

    this._router.navigate([`/proyectos`]);

  }

}
