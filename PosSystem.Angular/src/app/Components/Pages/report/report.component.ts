import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { InvoicesWithAPIService } from '../../../services/invoices-with-api.service';
import { ItemWithAPIService } from '../../../services/item-with-api.service';
import { NavbarComponent } from '../navbar/navbar.component';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { FooterComponent } from '../footer/footer.component';

@Component({
  selector: 'app-report',
  standalone: true,
  imports: [FormsModule,NavbarComponent,SidebarComponent,FooterComponent],
  templateUrl: './report.component.html',
  styleUrl: './report.component.css'
})
export class ReportComponent implements OnInit {
  fromPeriod:any;
  toPeriod:any;
  billDateList:{id: string, billsDate: string,
      clientName: string,
      itemName: string,
      sellingPrice: number,
      unit: string,
      quantity: number,
      discount: number,
      total: number}[]=[];
  itemList:any[]=[];
  code:any;
  name:any;
  buyingPrice:any;
  sellingPrice:any;

  ReportTableItem:{code:number,name:string,buyingPrice:string,sellingPrice:string,profits:number}[]=[];

  constructor(private invoiceService:InvoicesWithAPIService, private itemService:ItemWithAPIService){}

  ngOnInit(): void {
    this.invoiceService.getAllBills().subscribe({next:(bills:any)=>{  
      this.billDateList = bills}});
    this.invoiceService.getAllBills().subscribe({next:(bills:any)=>{  
      this.billDateList = bills}});
      this.itemService.getAllItems().subscribe({next:(response)=> this.itemList=response});
  }

  Submit(e:any){
    e.preventDefault();
    const d1 = new Date(this.fromPeriod);  
    const d2 = new Date(this.toPeriod);
    
    this.billDateList.forEach(bill=>{
      
      if(new Date(bill.billsDate)>=d1 && new Date(bill.billsDate)<=d2 )
      {
        
        console.log(bill.itemName)
        this.itemList.forEach(item =>
        {
          if(bill.itemName==item.name){
            this.code=item.id;
            this.name=item.name;
            this.buyingPrice=item.buyingPrice;
            this.sellingPrice=item.sellingPrice;
            this.ReportTableItem.push({code:this.code,name:this.name,buyingPrice:this.buyingPrice,sellingPrice:this.sellingPrice,profits:this.sellingPrice-this.buyingPrice})
          }
        }

        

        )
      }
    })

  }

  Cancel(){
    this.fromPeriod=null;
    this.toPeriod=null;
  }

}
