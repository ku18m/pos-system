import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersMainComponent } from './users-main.component';
import { ShowEmployeeComponent } from './show-employee/show-employee.component';
import { AddEmployeeComponent } from './add-employee/add-employee.component';

const routes: Routes = [{
  path: '',
  component: UsersMainComponent,
  children: [
    {path: '', component: ShowEmployeeComponent},
    {path: 'new', component: AddEmployeeComponent},
    {path: 'edit/:id', component: AddEmployeeComponent}
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
