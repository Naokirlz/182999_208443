import { Component, OnInit } from '@angular/core';
import { Usuario } from '../../interfaces/dtoUsuario.interface';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: [
  ]
})
export class LoginComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  login(argumento : Usuario){

    this.login(argumento);

  }

}
