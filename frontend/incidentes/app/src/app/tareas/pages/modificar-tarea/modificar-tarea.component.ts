import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';
import { Tarea } from 'src/app/interfaces/tarea.interface';
import { ProyectoService } from 'src/app/proyectos/services/proyecto.service';
import { TareaService } from '../../services/tarea.service';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-modificar-tarea',
  templateUrl: './modificar-tarea.component.html'
})
export class ModificarTareaComponent implements OnInit {

  constructor(private fb: FormBuilder,
    private proyectoService: ProyectoService,
    private tareasService: TareaService,
    private _router: Router,
    private _route: ActivatedRoute,
    private messageService: MessageService) { }

  public proyectos: Proyecto[] = [];
  public tareaId: number = -1;
  public tarea: Tarea | null = null;

  ngOnInit(): void {
    this.tareaId = this._route.snapshot.params['tareaId'];
    this.proyectoService.getProyecto()
      .subscribe(
        (data: Array<Proyecto>) => this.proyectos = data,
      ),
      ({ error }: any) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error });
      };
    this.tareasService.getTarea(this.tareaId)
      .subscribe(
        (data: Tarea) => this.result(data),
      ),
      ({ error }: any) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error });
      };
  }

  home: MenuItem = { icon: 'pi pi-home', routerLink: '/' };
  public items: MenuItem[] = [
    { label: 'Tareas', routerLink: '/tareas' },
    { label: 'Modificar' },
  ];

  private result(data: Tarea): void {
    this.tarea = data;
    this.miFormulario.patchValue({
      nombre: this.tarea.nombre,
      costo: this.tarea.costo,
      duracion: this.tarea.duracion,
      proyecto: this.tarea.proyectoId
    });
  }

  miFormulario: FormGroup = this.fb.group({
    nombre: [, [Validators.required, Validators.minLength(3), Validators.maxLength(25)]],
    costo: [, [Validators.required, Validators.min(0)]],
    duracion: [, [Validators.required, Validators.min(0)]],
    proyecto: [, [Validators.required]]
  })

  campoEsValido(campo: string) {
    return this.miFormulario.controls[campo].errors
      && this.miFormulario.controls[campo].touched
  }

  modificarTarea() {
    if (this.miFormulario.invalid) {
      this.miFormulario.markAllAsTouched();
      return;
    }

    const tarea: Tarea = {
      id: this.tareaId,
      nombre: this.miFormulario.value.nombre,
      costo: this.miFormulario.value.costo,
      duracion: parseInt(this.miFormulario.value.duracion),
      proyectoId: this.miFormulario.value.proyecto,
    }

    this.tareasService.update(tarea)
      .subscribe((data: Tarea) => {
        this.messageService.add({
          severity: 'success', summary: 'Listo',
          detail: 'Tarea modificada correctamente.'
        });
      },
        (({ error }: any) => {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: error });
        }
        )
      );
  }

  onConfirm() {
    this.messageService.clear();
    this.modificarTarea();
  }

  consultarAccion() {
    this.messageService.clear();
    this.messageService.add({ key: 'c', sticky: true, severity: 'warn', summary: 'Está seguro?', detail: 'Confirme para modificar la Tarea' });
  }

  onReject() {
    this.messageService.clear();
  }

  volver() {
    this._router.navigate([`/tareas`]);
  }
}
