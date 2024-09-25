import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShowClientComponent } from './show-clients/show-clients.component';
import { ClientCrudsComponent } from './client-cruds/client-cruds.component';
import { ClientsmainComponent } from './clientsmain.component';
import { ClientsComponent } from './add-client/clients.component';

const routes: Routes = [
  {
    path: '',
    component: ClientsmainComponent,
    children: [
      { path: '', component: ClientsComponent },
      { path: 'operations', component: ShowClientComponent },
      { path: 'operations/:id', component: ClientCrudsComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ClientsRoutingModule {}
