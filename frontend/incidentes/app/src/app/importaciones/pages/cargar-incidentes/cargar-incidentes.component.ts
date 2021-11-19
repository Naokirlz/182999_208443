import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { dtoImportaciones } from 'src/app/interfaces/dtoImportaciones.interface';
import { ImportacionesService } from '../../services/importaciones.service';

@Component({
  selector: 'app-cargar-incidentes',
  templateUrl: './cargar-incidentes.component.html'
})
export class CargarIncidentesComponent implements OnInit {

  constructor(private messageService: MessageService,
    private importacionesService: ImportacionesService,
    private fb: FormBuilder) { }

  public importaciones: dtoImportaciones[] = [];

  ngOnInit(): void {
    this.importacionesService.getImportaciones().subscribe(
      (data) => {
        this.importaciones = data;
      },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error.error });
      }
    );
  }

  miFormulario: FormGroup = this.fb.group({
    rutaArch: [, [Validators.required]],
    rutaPlug: [, ],
    selectBin: [, ],
  })

  campoEsValido(campo: string) {
    return this.miFormulario.controls[campo].errors
      && this.miFormulario.controls[campo].touched
  }

  extraerNombreArchivo(ruta: string): string {
    let nombreArchivo = ruta.split('\\');
    return nombreArchivo[nombreArchivo.length - 1];
  }

  cargarBugs():void {
    if (this.miFormulario.invalid) {
      this.miFormulario.markAllAsTouched();
      return;
    }
    /* .replace(/\\/g, '/') */
    const dtoImp: dtoImportaciones = {
      rutaFuente: this.miFormulario.controls.rutaArch.value,
      rutaBinario: (this.miFormulario.controls.rutaPlug.value) ? this.miFormulario.controls.rutaPlug.value : this.miFormulario.value.selectBin,
      usuarioId: 1
    }
    this.importacionesService.postImportaciones(dtoImp).subscribe(
      (data) => {
        this.messageService.add({ severity: 'success', summary: 'Exito', detail: 'Carga exitosa' });
      },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error.error });
      }
    );
  }
}
