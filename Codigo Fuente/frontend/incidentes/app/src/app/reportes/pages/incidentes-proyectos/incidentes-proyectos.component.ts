import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReporteBugsProyecto } from 'src/app/interfaces/reportes.interface';
import { ReportesService } from '../../services/reportes.service';
import { MenuItem, MessageService } from 'primeng/api';

@Component({
  selector: 'app-incidentes-proyectos',
  templateUrl: './incidentes-proyectos.component.html',
  styles: [
  ]
})
export class IncidentesProyectosComponent implements OnInit {

  public proyectos: ReporteBugsProyecto[] = [];

  constructor(private _router: Router,
    private reportesService: ReportesService,
    private messageService: MessageService) { }

  ngOnInit(): void {
    this.reportesService.getAll()
      .subscribe(
        ((data: ReporteBugsProyecto[]) => this.result(data)),
      ),
      ({ error }: any) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error });
      };
  }

  home: MenuItem = { icon: 'pi pi-home', routerLink: '/' };
  public items: MenuItem[] = [
    { label: 'Reportes' },
  ];

  private result(data: ReporteBugsProyecto[]): void {
    this.proyectos = data;
  }
}
