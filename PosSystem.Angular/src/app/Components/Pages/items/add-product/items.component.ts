import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CompanyWithAPIService } from '../../../../services/company-with-api.service';
import { TypesWithAPIService } from '../../../../services/types-with-api.service';
import { ItemWithAPIService } from '../../../../services/item-with-api.service';
import { IItems } from '../IItems';
import { UnitsWithAPIService } from '../../../../services/units-with-api.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-items',
  standalone: true,
  imports: [ReactiveFormsModule,FormsModule, CommonModule],
  templateUrl: './items.component.html',
  styleUrl: './items.component.css'
})
export class ItemsComponent implements OnInit {
  companyId:any="-- Choose From Companies --";
  typeId:any="-- Choose From Types --";
  unitId:string="-- Choose From Units --";
  itemName:any;
  sellingPrice:any;
  buyingPrice:any;
  quantity:any = 0;
  notes:any;
  ItemList:string[]=[];
  tokin:any=localStorage.getItem('token');

  companyNameRequiredError:any;
  typeNameRequiredError:any;
  unitNameRequiredError:any;
  itemNameRequiredError:any;
  itemNameDuplicateError:any;
  sellingError:any;
  buyingError:any;
  companyList:any;
  typeList:any;
  unitList:any;
  buyGreaterThanSellError:any;


  constructor(
    private companyService:CompanyWithAPIService,
    private typeService:TypesWithAPIService,
    private itemService:ItemWithAPIService,
    private unitService: UnitsWithAPIService,
    private changeDetector: ChangeDetectorRef
  ){}
  ngOnInit(): void {
    this.companyService.getCompanies().subscribe({
      next:(companies)=>{this.companyList=companies;},
      error:(error)=>{console.error('Error fetching companies:',error);}
    });

    this.unitService.getAll().subscribe({
      next:(units)=>{this.unitList=units;},
      error:(error)=>{console.error('Error fetching units:',error);}
    });

    this.itemService.getAll().subscribe({next:(items:IItems[])=>{
      this.ItemList = items.map(item => item.name);
    },
    error:(error) => {
      console.error('Error fetching items:', error);
    },
  });
  }

  itemForm=new FormGroup ({
    companyId:new FormControl ("",Validators.required),
    typeId:new FormControl ("",Validators.required),
    unitId:new FormControl ("",Validators.required),
    name:new FormControl ("",Validators.required),
    sellingPrice:new FormControl (""),
    buyingPrice:new FormControl (""),
    quantity:new FormControl (""),
    notes:new FormControl ("")
  })

  ChangeType(){
    this.typeService.getTypesByCompanyId(this.companyId).subscribe({
      next:(types)=>{this.typeList=types},
      error:(error)=>{console.error('Error fetching types:',error);}
    });
  }

  Submit(e:any){
    e.preventDefault();
    if(this.companyId === "-- Choose From Companies --" )
    {
      this.companyNameRequiredError="Company Name Is Required";
    }
    else
    this.companyNameRequiredError=null;



    if(this.typeId === "-- Choose From Types --" )
      {
        this.typeNameRequiredError="Type Name Is Required";
      }
      else
      this.typeNameRequiredError=null;

    if(this.unitId === "-- Choose From Units --" )
      {
        this.unitNameRequiredError="Unit Name Is Required";
      }
      else
      this.unitNameRequiredError=null;


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
    if(this.companyId != "-- Choose From Companies --" && this.typeId != "-- Choose From Types --" && this.unitId != '-- Choose From Units --' && this.itemName!=null && this.itemName!="" && !this.ItemList.includes(this.itemName)&& this.sellingPrice>=0 && this.buyingPrice>=0&&this.buyingPrice<=this.sellingPrice){
    const newItem:any = { companyId: this.companyId, categoryId:this.typeId,name:this.itemName, unitId:this.unitId,sellingPrice:this.sellingPrice,buyingPrice:this.buyingPrice,quantity:this.quantity, notes:this.notes };

    this.itemService.addNewItem(newItem).subscribe({
      next: () => {
        this.showNotification('success', 'Item added successfully!');
        this.ItemList.push(newItem);
        this.itemForm.reset();
        this.companyId="-- Choose From Companies --";
        this.typeId="-- Choose From Types --";
      },
      error: (error) => {
        this.showNotification('danger', error.error.errors.Name[0]);
      }
    });
  }
  }

  Cancel(){
    this.itemForm.reset();
    this.companyId="-- Choose From Companies --";
    this.typeId="-- Choose From Types --";
    this.companyNameRequiredError=null;
    this.typeNameRequiredError=null;
    this.itemNameRequiredError=null;
    this.itemNameDuplicateError=null;
    this.sellingError=null;
    this.buyingError=null;
  }


  notification: { type: string; message: string } | null = null;

  showNotification(type: string, message: string) {
    this.notification = { type, message };
    this.changeDetector.detectChanges();
  }

  closeNotification() {
    this.notification = null;
    this.changeDetector.detectChanges();
  }
}
