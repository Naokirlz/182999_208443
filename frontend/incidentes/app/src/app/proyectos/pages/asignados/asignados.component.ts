import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';
import { ProyectoService } from '../../services/proyecto.service';
import { Asignado } from '../../../interfaces/asignado.interface';

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

  constructor(private _route: ActivatedRoute,
              private proyectoService:ProyectoService) { 

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


  private result(data: Proyecto): void {
    this.proyectos.push(data);
    this.asignados = this.proyectos[0].asignados;
    
  }

}
