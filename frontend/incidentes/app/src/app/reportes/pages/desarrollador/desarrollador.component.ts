import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ReporteBugsDesarrollador } from 'src/app/interfaces/reportes.interface';
import { ProyectoService } from '../../../proyectos/services/proyecto.service';
import { ReportesService } from '../../services/reportes.service';

@Component({
  selector: 'app-desarrollador',
  templateUrl: './desarrollador.component.html',
  styles: [
  ]
})
export class DesarrolladorComponent implements OnInit {


  public desarrolladorId: number;
  public desarrolladores:ReporteBugsDesarrollador[]=[];

  constructor(private reportesService:ReportesService,
              private _route: ActivatedRoute,
              private _router: Router) {

    this.desarrolladorId = 0;

   }

  ngOnInit(): void {

    this.desarrolladorId = this._route.snapshot.params['desarrolladorId'];

    this.reportesService.getby(this.desarrolladorId)
      .subscribe(
        ((data: ReporteBugsDesarrollador) => this.result(data)),
      );

  }

  private result(data: ReporteBugsDesarrollador): void {
    this.desarrolladores.push(data);
  
  }

  volver(){

    this._router.navigate([`/reportes/resueltos`]);

  }

}
