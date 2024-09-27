import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ItemsMainComponent } from './items-main.component';
import { ItemsComponent } from './add-product/items.component';
import { ProductsComponent } from './showAllProducts/products.component';
import { ProductFormComponent } from './productCruds/productform.component';

const routes: Routes = [{
  path: '',
  component: ItemsMainComponent,
  children: [
    {path: '', component: ItemsComponent},
    {path: 'operations', component: ProductsComponent},
    {path: 'operations/:id', component: ProductFormComponent},
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ItemsRoutingModule { }
