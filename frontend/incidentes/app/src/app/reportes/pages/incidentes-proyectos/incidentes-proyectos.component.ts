import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReporteBugsProyecto } from 'src/app/interfaces/reportes.interface';
import { ReportesService } from '../../services/reportes.service';

@Component({
  selector: 'app-incidentes-proyectos',
  templateUrl: './incidentes-proyectos.component.html',
  styles: [
  ]
})
export class IncidentesProyectosComponent implements OnInit {

  public proyectos:ReporteBugsProyecto[]=[];
  
  constructor(private _router: Router,
              private reportesService:ReportesService) { }

  ngOnInit(): void {

    
    this.reportesService.getAll()
    .subscribe(
      ((data: ReporteBugsProyecto[]) => this.result(data)),
    );

  }

  private result(data: ReporteBugsProyecto[]): void {

    this.proyectos = data;

  }


  volver(){

    this._router.navigate([`/reportes`]);

  }

}
