import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ClientsWithAPIService } from '../../../services/clients-with-api.service';
import { InvoicesWithAPIService } from '../../../services/invoices-with-api.service';
import { ItemWithAPIService } from '../../../services/item-with-api.service';
import { UnitsWithAPIService } from '../../../services/units-with-api.service';
import { IInvoices } from './IInvoices';
import { NavbarComponent } from '../navbar/navbar.component';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { FooterComponent } from '../footer/footer.component';

@Component({
  selector: 'app-invoices',
  standalone: true,
  imports: [ReactiveFormsModule,FormsModule,NavbarComponent,SidebarComponent,FooterComponent],
  templateUrl: './invoices.component.html',
  styleUrl: './invoices.component.css'
})
export class InvoicesComponent implements OnInit {
  // ---------------------------------  START SECTION 1&2 (VARIABLES) --------------------------------------------
  billDate:any;
  billNumber:any;
  clientName:any="-- Choose From Clients --";
  itemName:any="-- Choose From Items --";
  sellingPrice:any;
  unit:any="-- Choose From Units --";
  quantity:any;
  discount:any;
  total:any;
  balance:any;
  billRequiredError:any;
  billList:string[]=[];
  clientList:any[]=[];
  clientRequiredError:any;
  itemList:any[]=[];
  itemRequiredError:any;
  quantityRequiredError:any;
  quantityZeroError:any;
  codeNum:any;
  billTableItem:{id:string,name:string,unit:string,quantity:number,sellingPrice:number,discount:number,total:number,balance:number}[]=[];
  unitList:any[]=[];
  // outOFStockError:any;
  quantityGraterError:any;
  updatedQuantity:any;
// ---------------------------------  END SECTION 1&2 --------------------------------------------

// ---------------------------------  START SECTION 3 (VARIABLES) --------------------------------------------
  billsTotal:number=0;
  percentage:any;
  percentageError:any;
  discountValue:any;
  discountValueError:any;
  net:any;
  paidUp:any;
  paidError:any;
  rest:any;

// ---------------------------------  END SECTION 3 --------------------------------------------


// ---------------------------------  START SECTION 4 (VARIABLES) --------------------------------------------
  employeeName:any="-- Choose From Employees --";
  date:any;
  startTime:any;
  endTime:any;
  employeeList:any[]=[];
  employeeNameError:any;
  dateError:any;
  employeeBillList:any[]=[];
  timeError:any;

// ---------------------------------  END SECTION 4 --------------------------------------------


  // ---------------------------------  START SECTION 1&2 (FORM) --------------------------------------------
  billsForm=new FormGroup ({
    billsDate:new FormControl("",Validators.required),
    billsNumber:new FormControl(""),
    clientName:new FormControl("-- Choose From Clients --",Validators.required),
    itemName:new FormControl("-- Choose From Items --",Validators.required),
    sellingPrice:new FormControl(""),
    unit:new FormControl("-- Choose From Units --"),
    quantity:new FormControl("",Validators.required),
    discount:new FormControl(""),
    total:new FormControl(""),
    balance:new FormControl("")

  })
  // ---------------------------------  END SECTION 1&2 --------------------------------------------

  // ---------------------------------  START SECTION 3 (FORM) --------------------------------------------
  discountForm=new FormGroup ({
    billsTotal:new FormControl(""),
    percentageDiscount:new FormControl(""),
    valueDiscount:new FormControl(""),
    theNet:new FormControl(""),
    paidUp:new FormControl(""),
    theRest:new FormControl(""),
  });
  // ---------------------------------  END SECTION 3 --------------------------------------------

  // ---------------------------------  START SECTION 4 (FORM) --------------------------------------------
  employeeForm=new FormGroup ({
    employeeName:new FormControl(""),
    date:new FormControl(""),
    startTime:new FormControl(""),
    endTime:new FormControl("")
  });


  // ---------------------------------  END SECTION 4 --------------------------------------------

  constructor(private unitService:UnitsWithAPIService, private clientService:ClientsWithAPIService,private invoiceService:InvoicesWithAPIService,private itemService:ItemWithAPIService){}

  ngOnInit(): void {
    // ---------------------------------  START SECTION 1&2 (oNiNIT)  --------------------------------------------
    this.billNumber=1000000;
    

    this.invoiceService.GetAllClients().subscribe({
      next:(response)=>{
        const res=response.data;
        res.forEach((element:any) => {
          this.clientList.push(element);
        });
      }
    });

    this.invoiceService.GetAllItems().subscribe({
      next:(response)=>{
        const res=response.data;
        res.forEach((element:any) => {
          this.itemList.push(element);
        });
      }
    })

    this.invoiceService.GetAllUnits().subscribe({
      next:(response)=>{
        const res=response.data;
        res.forEach((element:any) => {
          this.unitList.push(element);
        });
        
      }
    });

    this.billsForm.get('billsNumber')?.disable();
    this.billsForm.get('total')?.disable();
    // ---------------------------------  END SECTION 1&2 --------------------------------------------

    // ---------------------------------  START SECTION 3 (oNiNIT)  --------------------------------------------
      this.discountForm.get('billsTotal')?.disable();
      this.discountForm.get('theNet')?.disable();
      this.discountForm.get('theRest')?.disable();
    // ---------------------------------  END SECTION 3 --------------------------------------------

    // ---------------------------------  START SECTION 4 (oNiNIT)  --------------------------------------------
    this.invoiceService.getAllEmployees().subscribe({next:(response)=> this.employeeList=response});
    this.invoiceService.getAllBillEmployees().subscribe({next:(bills:IInvoices[])=>{  
      this.employeeBillList = bills.map(bill => bill.employeeName)}});

    // ---------------------------------  END SECTION 4 --------------------------------------------


  }



  // ---------------------------------  START SECTION 1&2 (FUNCTIONS)  --------------------------------------------
  calculateTotal(){
    this.total=(this.sellingPrice*this.quantity);
  }

  ChangeItem(){
    this.itemList.forEach(item =>{
      if(item.name==this.itemName)
      {
        this.sellingPrice=item.sellingPrice;
        if(item.quantity==0)
          {
            // this.outOFStockError="Out Of Stock";
            this.billsForm.get('quantity')?.disable();
            this.quantity="Out Of Stock";
            this.quantityGraterError=null;

          }
          else
          {
            // this.outOFStockError=null;
            this.quantity="";
            this.billsForm.get('quantity')?.enable();
            
          }
        }
        });
      this.itemList.forEach(item =>{
        if(item.name==this.itemName)
        {
          this.codeNum=item.id;
        }
          
      
      
    });

    console.log(this.itemName);
  }

  FirstSectionSubmit(e:any){
    e.preventDefault();
    this.billNumber=this.invoiceService.GetBillNumber().subscribe({
      next:(response)=>{
        return response.data;
      }
      // ,
      // error:(error)=>{
      //   this.billNumber=1000000;
      // }
    });

    console.log(this.billsForm.status);
    this.itemList.forEach(item=>{
      if(this.itemName==item.name){
        item.quantity=item.quantity-this.quantity;
        console.log(item.quantity)
        this.itemService.updateItemQuantity(item.id, item);  
      }
    })
    
    this.billTableItem.push({id:this.codeNum,name:this.itemName,unit:this.unit,quantity:this.quantity,sellingPrice:this.sellingPrice,discount:this.discount,total:this.total,balance:this.balance});

    // ---------------------------------  START SECTION 3 (FUNCTIONS)  --------------------------------------------
    this.billTableItem.forEach(bill=>{
      this.billsTotal+=Number(bill.total)
    })


  // ---------------------------------  END SECTION 3 --------------------------------------------
    if (this.billDate==null) {  
      this.billRequiredError = 'Bill Date Is Required';
    }  
    else
        this.billRequiredError = null;

    if(this.clientName === "-- Choose From Clients --" )
      {
        this.clientRequiredError="Client Name Is Required";
      }
      else
      this.clientRequiredError=null;

    if(this.itemName === "-- Choose From Items --" )
      {
        this.itemRequiredError="Item Name Is Required";
      }
      else
      this.itemRequiredError=null;

      if (this.quantity==null || this.quantity=="") {  
        this.quantityRequiredError = 'Quantity Is Required';
      }  
      else
          this.quantityRequiredError = null;

      if (this.quantity==0) {  
        this.quantityZeroError = 'Quantity  Must Be Greater Than Zero';
      }  
      else
          this.quantityZeroError = null;

          this.itemList.forEach(item =>{
            if(item.name==this.itemName)
            {
              if(Number(this.quantity)>Number(item.quantity))
              {
                console.log(item.quantity);
                this.quantityGraterError=`There are Only ${item.quantity} Products in the Stock`;
              }
              else
              this.quantityGraterError=null;
            }
          });

    //adding bill
    if(this.billDate!=null && this.clientName != "-- Choose From Clients --" && this.itemName != "-- Choose From Items --" && this.quantity!=null && this.quantity!="" && this.quantity!=0 ){

    
    const newBill:any = { billsDate: this.billDate, billsNumber:this.billNumber, clientName:this.clientName, itemName:this.itemName, sellingPrice:this.sellingPrice,unit:this.unit, quantity:this.quantity, discount:this.discount, total:this.total };  

    this.invoiceService.addBill(newBill).subscribe({  
      next: () => {  
        console.log('Bill added successfully!');  
        this.billList.push(newBill);  
        this.billsForm.reset();  
        this.clientName === "-- Choose From Clients --"; 
        this.itemName === "-- Choose From Items --"; 
        this.unit === "-- Choose From Units --"; 
      },  
      error: (error) => {  
        console.error('Error adding bill:', error);  
      }  
    });

    
  }  
  }  
  // ---------------------------------  END SECTION 1&2 --------------------------------------------

  // ---------------------------------  START SECTION 3 (FUNCTIONS)  --------------------------------------------
  checkPercentage(){
    if(Number(this.percentage)<0)
    {
      this.percentageError="Percentage Discount Must Be Greater Than Or Equal Zero"
    }
    else
      this.percentageError=null;
  }

  checkValue(){
    if(Number(this.discountValue)<0)
      {
        this.discountValueError="Value Discount Must Be Greater Than Or Equal Zero"
      }
      else
        this.discountValueError=null;

        
    }

    calculateDiscount(){
      this.discountValue=this.billsTotal*(this.percentage/100);
    }

    calculateNet(){
      
        this.net=this.billsTotal-this.discountValue;
      
    }

    checkPaid(){
      if(Number(this.paidUp)<0)
        {
          this.paidError="Paid Up Must Be Greater Than Or Equal Zero"
        }
        else
          this.paidError=null;
  
          
      }

      calculateRest(){
        this.rest=this.paidUp-this.net;
      }

  // ---------------------------------  END SECTION 3 --------------------------------------------


  // ---------------------------------  START SECTION 4 (FUNCTIONS)  --------------------------------------------
  employeeSubmit(e:any){
    e.preventDefault();
    if(this.employeeName === "-- Choose From Employees --" )
    {
      this.employeeNameError="Employee Name Is Required";
    }
    else
    this.employeeNameError=null;

    if (this.date==null) {  
      this.dateError = 'Date Is Required';
        
    }  
    else
        this.dateError = null;

    
    const startDate = this.convertToDate(this.startTime);  
    const endDate = this.convertToDate(this.endTime);
    if(endDate<startDate)
    {
      this.timeError="End Time Must Be Greater Than Start Time"
    }
    else
    this.timeError=null;

    //adding Employee
    if(this.employeeName != "-- Choose From Employees --" && this.date!=null)
      {
        const newEmployee:any = { employeeName: this.employeeName, date:this.date, startTime:this.startTime, endTime:this.endTime  };  

        this.invoiceService.addBillEmployee(newEmployee).subscribe({  
          next: () => {  
            console.log('EmployeeBill added successfully!');  
            this.employeeBillList.push(newEmployee);  
            this.employeeForm.reset();
            this.employeeName = "-- Choose From Employees --" 
            // this.typeName=null;
            // this.companyValue="-- Choose From Companies --";  
          },  
          error: (error) => {  
            console.error('Error adding Employee:', error);  
          }  
        });  
      }
  }  
  Cancel(){
    this.employeeForm.reset(); 
    this.employeeName = "-- Choose From Employees --";
    this.employeeNameError=null;
    this.dateError=null;
    this.timeError=null;

    
  }

  private convertToDate(timeString: string): Date {  
    const [hours, minutes] = timeString.split(':').map(Number);  
    const date = new Date();  
    date.setHours(hours, minutes, 0, 0); // Set hours and minutes, seconds and milliseconds to 0  
    return date; 
  // ---------------------------------  END SECTION 4 --------------------------------------------

}

}
  


