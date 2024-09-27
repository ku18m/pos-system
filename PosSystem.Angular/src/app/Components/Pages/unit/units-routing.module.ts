import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UnitsMainComponent } from './units-main.component';
import { UnitComponent } from './add-unit/unit.component';
import { ShowUnitComponent } from './show-units/units.component';
import { UnitCrudComponent } from './unit-crud/unit-crud.component';

const routes: Routes = [{
  path: '',
  component: UnitsMainComponent,
  children: [
    {path: '', component: UnitComponent},
    {path: 'operations', component: ShowUnitComponent},
    {path: 'operations/:id', component: UnitCrudComponent}
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UnitsRoutingModule { }
