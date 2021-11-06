import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Proyecto } from '../../../interfaces/proyecto.interface';
import { ProyectoService } from '../../services/proyecto.service';

@Component({
  selector: 'app-ver-proyectos',
  templateUrl: './ver-proyectos.component.html',
  styles: [
    
    `
    td, th {
    text-align: center !important;
    vertical-align: middle !important;
    }

    `
  ]
})
export class VerProyectosComponent implements OnInit {

  constructor(private proyectoService:ProyectoService,
              private _router: Router) { }

  public proyectos:Proyecto[]=[];

  ngOnInit(): void {

    this.proyectoService.getProyecto()
    .subscribe(
      ((data: Array<Proyecto>) => this.result(data)),
    );


  }

  eliminar(id:number):void{
   
    this.proyectoService.deleteProyecto(id);
    this.proyectos = this.proyectos.filter(p => p.id !== id);
  }

  editar(id:number):void{
   
    this._router.navigate([`/proyectos/${id}/editar`]);

  }

  asignados(id:number):void{

    this._router.navigate([`/proyectos/${id}/asignados`]);
    
  }

  incidentes(id:number):void{

    this._router.navigate([`/proyectos/${id}/incidentes`]);
    
  }

  private result(data: Array<Proyecto>): void {
    this.proyectos = data;
  }

}
