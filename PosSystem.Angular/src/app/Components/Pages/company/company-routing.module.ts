import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CompanyMainComponent } from './company-main.component';
import { CompanyComponent } from './add-company/company.component';
import { CompaniesComponent } from './showcompanies/showcompanies.component';
import { CompanyCrudsComponent } from './company-cruds/company-cruds.component';

const routes: Routes = [{
  path: '',
  component: CompanyMainComponent,
  children: [
    {path: '', component: CompanyComponent},
    {path: 'operations', component: CompaniesComponent},
    {path: 'operations/:id', component: CompanyCrudsComponent},
  ],
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CompanyRoutingModule { }
