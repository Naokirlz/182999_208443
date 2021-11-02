import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styles: [
  ]
})
export class MenuComponent implements OnInit {

  items: MenuItem[] = [];
 
  constructor() { }

  ngOnInit(): void {

    this.items = [
      {
          
          icon: 'pi pi-fw pi-power-off',
          items: [{
                  label: 'Login', 
                                   
                  },
                  {
                  label: 'Logout'},
              
                  ]
      },
      {
          label: 'Usuarios',
          icon: 'pi pi-fw pi-user',
          items: [
              {label: 'Alta', icon: 'pi pi-fw pi-user-plus'},
              {label: 'Ver', icon: 'pi pi-fw pi-search'}
          ]
      }
  ];


  }

}
