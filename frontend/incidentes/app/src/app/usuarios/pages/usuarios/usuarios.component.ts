import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styles: [
    `
    li{
      cursor:pointer;
    }
  `
  ]
})
export class UsuariosComponent implements OnInit {

  
  constructor() { }

  ngOnInit(): void {
  }

}
