import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';
import { ProyectoService } from '../../services/proyecto.service';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-proyecto-info',
  templateUrl: './proyecto-info.component.html',
  styles: [
  ]
})
export class ProyectoInfoComponent implements OnInit {

  constructor(private _router: Router,
              private _route: ActivatedRoute,
              private proyectoService:ProyectoService) { 
    
    this.proyectoId = 0;
  
  }
  public proyectos:Proyecto[]=[];
  public proyectoId: number;

  ngOnInit(): void {

    this.proyectoId = this._route.snapshot.params['proyectoId'];
    
    this.proyectoService.getBy(this.proyectoId)
    .subscribe(
      ((data: Proyecto) => this.proyectos.push(data)),
    );

  }

  home: MenuItem = { icon: 'pi pi-home', routerLink: '/' };
  public items: MenuItem[] = [
    { label: 'Proyectos', routerLink: '/proyectos' },
    { label: 'Detalles' },
  ];

  volver(){

    this._router.navigate([`/proyectos`]);

  }

}
