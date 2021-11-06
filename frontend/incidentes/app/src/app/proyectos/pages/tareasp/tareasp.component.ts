import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';
import { Tarea } from '../../../interfaces/tarea.interface';
import { ProyectoService } from '../../services/proyecto.service';

@Component({
  selector: 'app-tareasp',
  templateUrl: './tareasp.component.html',
  styles: [
  ]
})
export class TareaspComponent implements OnInit {

  public proyectoId: number;
  public proyectos:Proyecto[]=[];
  public tareas:Tarea[] | undefined=[];
  
  constructor(private proyectoService:ProyectoService,
              private _route: ActivatedRoute,
              private _router: Router) { 

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
    this.tareas = this.proyectos[0].tareas;

  }

  volver(){

    this._router.navigate([`/proyectos`]);

  }

}
