import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Incidente } from 'src/app/interfaces/incidente.interface';

@Component({
  selector: 'app-detalle-bug',
  templateUrl: './detalle-bug.component.html',
  styles: [
  ]
})
export class DetalleBugComponent implements OnInit {

  public incidenteId: number = 0;
  public incidente:Incidente[]=[];
  constructor(private _route: ActivatedRoute,
              private _router: Router) { }

  ngOnInit(): void {

    this.incidenteId = this._route.snapshot.params['incidenteId'];

    
  }


  volver(){

    //this._router.navigate([`/proyectos`]);
    this._router.navigate(['']);

  }



}
