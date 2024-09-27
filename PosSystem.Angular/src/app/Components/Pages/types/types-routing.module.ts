import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TypesMainComponent } from './types-main.component';
import { TypesComponent } from './add-type/types.component';
import { ShowTypesComponent } from './show-types/show-types.component';
import { TypesCrudsComponent } from './types-cruds/types-cruds.component';

const routes: Routes = [{
  path: '',
  component: TypesMainComponent,
  children: [
    {path: '', component: TypesComponent},
    {path: 'operations', component: ShowTypesComponent},
    {path: 'operations/:id', component: TypesCrudsComponent},
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TypesRoutingModule { }
