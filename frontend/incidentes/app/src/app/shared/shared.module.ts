import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SidebarComponent } from './sidebar/sidebar.component';
import { RouterModule } from '@angular/router';
import { MenuComponent } from './menu/menu.component';
import { PrimeNgModule } from '../prime-ng/prime-ng.module';
import { MenuSuperiorComponent } from './menu-superior/menu-superior.component';


@NgModule({
  declarations: [
    SidebarComponent,
    MenuComponent,
    MenuSuperiorComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    PrimeNgModule
    
  ],
  exports: [
    SidebarComponent,
    MenuComponent,
    MenuSuperiorComponent
  ]
})
export class SharedModule { }
