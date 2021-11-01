import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/pages/login/login.component';
import { LogoutComponent } from './login/pages/logout/logout.component';
import { AltaComponent } from './agregar-usuario/alta/alta.component';


const routes: Routes = [
  {
    path: '',
    component: LoginComponent,
    pathMatch: 'full'

},

{
  path: 'salir',
  component: LogoutComponent,
  
},
{
  path: 'usuarios',
  component: AltaComponent,
  
},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
