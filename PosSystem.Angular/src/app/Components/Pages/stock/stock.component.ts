import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ItemWithAPIService } from '../../../services/item-with-api.service';
import { NavbarComponent } from '../navbar/navbar.component';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { FooterComponent } from '../footer/footer.component';

@Component({
  selector: 'app-stock',
  standalone: true,
  imports: [FormsModule,NavbarComponent,SidebarComponent,FooterComponent],
  templateUrl: './stock.component.html',
  styleUrl: './stock.component.css'
})
export class StockComponent implements OnInit {

  items:any="-- Choose From Items --";
  itemList:any[]=[];
  code:any;
  name:any;
  quantity:any;
  itemTable:{code:string,name:string,quantity:number}[]=[];

  constructor(private itemService:ItemWithAPIService){}
  ngOnInit(): void {
    this.itemService.getAllItems().subscribe({next:(response)=> this.itemList=response});
  }

  Search(){
    this.itemList.forEach(item=>{
      if(this.items==item.name)
      {
        this.code=item.itemCode;
        this.name=item.name;
        this.quantity=item.quantity;
        this.itemTable.push({code:this.code,name:this.name,quantity:this.quantity})
      }
    })

    if(this.items=="All Products"){
      this.itemList.forEach(item=>{
        this.code=item.id;
        this.name=item.name;
        this.quantity=item.quantity;
        this.itemTable.push({code:this.code,name:this.name,quantity:this.quantity})
      })
    }
  }
}
