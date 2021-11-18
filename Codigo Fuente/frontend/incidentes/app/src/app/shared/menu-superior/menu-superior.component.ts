import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-menu-superior',
  templateUrl: './menu-superior.component.html',
  styles: [
  ]
})
export class MenuSuperiorComponent implements OnInit {

  constructor() { }
  colapsado: boolean = false;

  @Output() onToogle: EventEmitter<boolean> = new EventEmitter();

  ngOnInit(): void {
  }

  toogleMenu():void{
    this.colapsado = !this.colapsado;
    this.onToogle.emit(this.colapsado);
  }
}
