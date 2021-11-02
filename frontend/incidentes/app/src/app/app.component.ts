import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent {
  title = 'app';

  colapsado: boolean = false;

  toogleMenu(estado: boolean): void {
    this.colapsado = estado;
  }

}
