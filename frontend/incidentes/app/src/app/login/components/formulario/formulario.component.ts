import { Component, Input, OnInit, Output,EventEmitter} from '@angular/core';
import { Usuario } from 'src/app/interfaces/dtoUsuario.interface';
import { LoginService } from '../../services/login.service';
import {InputTextModule} from 'primeng/inputtext';

@Component({
  selector: 'app-formulario',
  templateUrl: './formulario.component.html',
  styles: [
  ]
})
export class FormularioComponent implements OnInit {

  NombreUsuario:string = '';
  Contrasenia :string = '';
  

  @Output() onLoguearse: EventEmitter<Usuario> = new EventEmitter();

  constructor(private loginService:LoginService) { }

  ngOnInit(): void {
  }

  login():void {

    if (this.NombreUsuario.trim().length === 0){
    
      alert('NombreUsuario no puede ser vacío');
      return;
    
    }
    
    if (this.Contrasenia.trim().length === 0){
      
      alert('Clave no puede ser vacía');
      return;
    }
    
    const usuario:Usuario ={
      NombreUsuario: this.NombreUsuario,
      Contrasenia: this.Contrasenia
    }

    this.loginService.login(usuario);
    
      this.NombreUsuario = '';
      this.Contrasenia= '';
    
  }


}
