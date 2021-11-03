import { Component, OnInit } from '@angular/core';
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

  constructor(private proyectoService:ProyectoService) { }

  public usuarios:Proyecto[]=[];

  ngOnInit(): void {

    this.proyectoService.getProyecto()
    .subscribe(
      ((data: Array<Proyecto>) => this.result(data)),
    );


  }

  eliminar(id:number):void{
    this.proyectoService.deleteProyecto(id);
  }

  private result(data: Array<Proyecto>): void {
    this.usuarios = data;
  }

}
