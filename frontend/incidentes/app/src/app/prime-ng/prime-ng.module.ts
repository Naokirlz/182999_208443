import { NgModule } from '@angular/core';
import { MessageService } from 'primeng/api';

import {ButtonModule} from 'primeng/button';
import {CardModule} from 'primeng/card';
import {MenubarModule} from 'primeng/menubar';
import {TableModule} from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';



@NgModule({
  declarations: [],
  imports: [
    BrowserAnimationsModule,
  ],
  exports: [
    ButtonModule,
    CardModule,
    MenubarModule,
    TableModule,
    ToastModule,
  ]
})
export class PrimeNgModule { }
