import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Proyecto } from 'src/app/interfaces/proyecto.interface';
import { ProyectoService } from '../../services/proyecto.service';

@Component({
  selector: 'app-editar-proyecto',
  templateUrl: './editar-proyecto.component.html',
  styles: [
  ]
})
export class EditarProyectoComponent implements OnInit {

  public proyectoId: number;
  public proyecto: Proyecto | null = null;

  constructor(private fb: FormBuilder,
    private _route: ActivatedRoute,
    private proyectoService: ProyectoService,
    private _router: Router,
    private messageService: MessageService) {

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
    this.proyecto = data;
    this.miFormulario.patchValue({
      nombre: this.proyecto.nombre,
    });
  }

  miFormulario: FormGroup = this.fb.group({
    nombre: [, [Validators.required, Validators.minLength(3)]],
  })

  campoEsValido(campo: string) {
    return this.miFormulario.controls[campo].errors
      && this.miFormulario.controls[campo].touched
  }

  onConfirm() {
    this.messageService.clear();
    this.editarProyecto();
  }

  consultarAccion() {
    this.messageService.clear();
    this.messageService.add({ key: 'c', sticky: true, severity: 'warn', summary: 'Está seguro?', detail: 'Confirme para modificar el Proyecto' });
  }

  onReject() {
    this.messageService.clear();
  }

  editarProyecto() {
    if (this.miFormulario.invalid) {
      this.miFormulario.markAllAsTouched();
      return;
    }

    if (this.proyecto) {
      this.proyecto.nombre = this.miFormulario.value.nombre;
      console.log(this.proyecto);
      this.proyectoService.update(this.proyecto)
        .subscribe((data: Proyecto) => {
          this.messageService.add({
            severity: 'success', summary: 'Listo',
            detail: 'Proyecto modificado correctamente.'
          });
        },
          (({ error }: any) => {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: error });
          }
          )
        );
    }
  }

  volver() {

    this._router.navigate([`/proyectos`]);

  }



}
