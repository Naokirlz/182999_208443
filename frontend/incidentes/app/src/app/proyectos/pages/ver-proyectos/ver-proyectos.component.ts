import { Component, OnInit } from '@angular/core';
import { Proyecto } from '../../../interfaces/proyecto.interface';
import { ProyectoService } from '../../services/proyecto.service';

@Component({
  selector: 'app-ver-proyectos',
  templateUrl: './ver-proyectos.component.html',
  styles: [
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

  private result(data: Array<Proyecto>): void {
    this.usuarios = data;
  }

}
