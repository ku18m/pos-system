import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CompanyWithAPIService } from '../../../services/company-with-api.service';
import { TypesWithAPIService } from '../../../services/types-with-api.service';
import { ItemWithAPIService } from '../../../services/item-with-api.service';
import { IItems } from './IItems';
import { NavbarComponent } from '../navbar/navbar.component';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { FooterComponent } from '../footer/footer.component';

@Component({
  selector: 'app-items',
  standalone: true,
  imports: [ReactiveFormsModule,FormsModule,NavbarComponent,SidebarComponent,FooterComponent],
  templateUrl: './items.component.html',
  styleUrl: './items.component.css'
})
export class ItemsComponent implements OnInit {
  companyName:any="-- Choose From Companies --";
  typeName:any="-- Choose From Types --";
  itemName:any;
  sellingPrice:any;
  buyingPrice:any;
  quantity:any;
  notes:any;
  ItemList:string[]=[];
  companyNameRequiredError:any;
  typeNameRequiredError:any;
  itemNameRequiredError:any;
  itemNameDuplicateError:any;
  sellingError:any;
  buyingError:any;
  companyList:any;
  typeList:any;
  buyGreaterThanSellError:any;
  


  constructor(private companyService:CompanyWithAPIService, private typeService:TypesWithAPIService, private itemService:ItemWithAPIService){}
  ngOnInit(): void {
    // this.companyService.getAllCompanies().subscribe({next:(response)=> this.companyList=response});
    this.itemService.getAllItems().subscribe({next:(items:IItems[])=>{  
      this.ItemList = items.map(item => item.id);  
    },
    error:(error) => {  
      console.error('Error fetching items:', error);  
    },  
  });
  }

  itemForm=new FormGroup ({
    companyName:new FormControl ("",Validators.required),
    typeName:new FormControl ("",Validators.required),
    name:new FormControl ("",Validators.required),
    sellingPrice:new FormControl (""),
    buyingPrice:new FormControl (""),
    quantity:new FormControl (""),
    notes:new FormControl ("")
  })

  ChangeType(){
    this.typeService.getAllTypes().subscribe({next:(response)=>this.typeList=response});

  }

  Submit(e:any){
    e.preventDefault();
    if(this.companyName === "-- Choose From Companies --" )
    {
      this.companyNameRequiredError="Company Name Is Required";
    }
    else
    this.companyNameRequiredError=null;



    if(this.typeName === "-- Choose From Types --" )
      {
        this.typeNameRequiredError="Type Name Is Required";
      }
      else
      this.typeNameRequiredError=null;


    if (this.itemName==null || this.itemName=="") {  
      this.itemNameRequiredError = 'Item Name Is Required';
      this.itemNameDuplicateError=null;
    }  
    else
        this.itemNameRequiredError = null;


    if (this.ItemList.includes(this.itemName)) {  
      this.itemNameDuplicateError = `${this.itemName} has already existed before`; 
    } 
    else
        this.itemNameDuplicateError=null; 



    if(this.sellingPrice<0)
      {
        this.sellingError="Selling Price Must Be Greater Than Or Equal Zero";
      } 
    else
        this.sellingError=null;


    if(this.buyingPrice<0)
      {
        this.buyingError="Buying Price Must Be Greater Than Or Equal Zero"; 
      } 
    else
        this.buyingError=null;


    if(this.buyingPrice>this.sellingPrice)
      {
        this.buyGreaterThanSellError="Buying Price Must Be Less Than Or Equal Selling Price";
        
      } 
    else
        this.buyingError=null;



    //adding type
    if(this.companyName != "-- Choose From Companies --" && this.typeName != "-- Choose From Types --" && this.itemName!=null && this.itemName!="" && !this.ItemList.includes(this.itemName)&& this.sellingPrice>=0 && this.buyingPrice>=0&&this.buyingPrice<=this.sellingPrice){
    const newItem:any = { companyName: this.companyName, typeName:this.typeName,name:this.itemName,sellingPrice:this.sellingPrice,buyingPrice:this.buyingPrice,quantity:this.quantity, notes:this.notes , itemCode:Math.random()*100 };  

    this.itemService.addItem(newItem).subscribe({  
      next: () => {  
        console.log('Item added successfully!');  
        this.ItemList.push(newItem);  
        this.itemForm.reset(); 
        this.companyName="-- Choose From Companies --";  
        this.typeName="-- Choose From Types --";  
      },  
      error: (error) => {  
        console.error('Error adding Item:', error);  
      }  
    });  
  }
  }  
  Cancel(){
    this.itemForm.reset(); 
    this.companyName="-- Choose From Companies --";  
    this.typeName="-- Choose From Types --"; 
    this.companyNameRequiredError=null;
    this.typeNameRequiredError=null;
    this.itemNameRequiredError=null;
    this.itemNameDuplicateError=null;
    this.sellingError=null;
    this.buyingError=null;
  }
}
