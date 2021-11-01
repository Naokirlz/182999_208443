import { Component, Input, OnInit, Output,EventEmitter} from '@angular/core';
import { Usuario } from '../../interfaces/dtoUsuario.interface';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-formulario',
  templateUrl: './formulario.component.html',
  styles: [
  ]
})
export class FormularioComponent implements OnInit {

  @Input() nuevo: Usuario = {

    NombreUsuario:'federico',
    Contrasenia: 'password'

  }

  @Output() onLoguearse: EventEmitter<Usuario> = new EventEmitter();

  constructor(private loginService:LoginService) { }

  ngOnInit(): void {
  }

  login(){


    if (this.nuevo.NombreUsuario.trim().length === 0){return;}
    if (this.nuevo.Contrasenia.trim().length === 0){return;}

    //this.onLoguearse.emit(this.nuevo)
    var r = this.loginService.login(this.nuevo);

    r.subscribe(
      (data: any) => {
        console.log(JSON.stringify(data.token));
        localStorage.setItem('token', JSON.stringify(data.token)) ;
        console.log('token almacenado en local storage')
      }
    );

    

    this.nuevo = { 
      NombreUsuario: "",
      Contrasenia: ""
    } 
    

  }


}
