import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';
import { Incidente } from '../../../interfaces/incidente.interface';
import { ProyectoService } from '../../services/proyecto.service';
import { EstadosService } from '../../../estados/service/estados.service';

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
  
  constructor(private proyectoService:ProyectoService,
              private estadosService:EstadosService,
              private _route: ActivatedRoute,
              private _router: Router) 
              
              { 

              this.proyectoId = 0;

              }

  ngOnInit(): void {
    
    this.proyectoId = this._route.snapshot.params['proyectoId'];
    
    this.proyectoService.getBy(this.proyectoId)
    .subscribe(
      ((data: Proyecto) => this.result(data)),
    );
  }

  private result(data: Proyecto): void {
    this.proyectos.push(data);
    this.incidentes = this.proyectos[0].incidentes;

  }

  resolver(ide:number){
  
    const incidente:Incidente = this.incidentes?.find(i => i.id == ide)!;
    
    incidente.estadoIncidente = 2;
    this.estadosService.put(incidente);


  }

  volver(){

    this._router.navigate([`/proyectos`]);

  }

}
