import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ClientsWithAPIService } from '../../../../services/clients-with-api.service';
import { InvoicesWithAPIService } from '../../../../services/invoices-with-api.service';
import { ItemWithAPIService } from '../../../../services/item-with-api.service';
import { UnitsWithAPIService } from '../../../../services/units-with-api.service';
import { NavbarComponent } from '../../navbar/navbar.component';
import { SidebarComponent } from '../../sidebar/sidebar.component';
import { FooterComponent } from '../../footer/footer.component';
import { Router } from '@angular/router';
import { UsersWithAPIService } from '../../../../services/users-with-api.service';

@Component({
  selector: 'app-invoices',
  standalone: true,
  imports: [ReactiveFormsModule,FormsModule,NavbarComponent,SidebarComponent,FooterComponent],
  templateUrl: './invoices.component.html',
  styleUrl: './invoices.component.css'
})
export class InvoicesComponent implements OnInit {
 //   START SECTION 1&2 (VARIABLES)
 billDate: any;
 billNumber: any=1;
 clientName: any = "-- Choose From Clients --";
 itemName: any = "-- Choose From Items --";
 sellingPrice: any;
 unit: any;
 quantity: any;
 discount: any;
 total: any;
 balance: any;
 billRequiredError: any;
 billList: string[] = [];
 clientList: any[] = [];
 clientRequiredError: any;
 itemList: any[] = [];
 itemRequiredError: any;
 quantityRequiredError: any;
 quantityZeroError: any;
 codeNum: any;
 billTableItem: { id: string, name: string, unit: string, quantity: number, sellingPrice: number, discount: number, total: number, balance: number }[] = [];
 unitList: any[] = [];
 // outOFStockError:any;
 quantityGraterError: any;
 updatedQuantity: any;





 // START SECTION 3 (VARIABLES)
 billsTotal: number = 0;
 percentage: any;
 percentageError: any;
 discountValue: any;
 discountValueError: any;
 net: any;
 paidUp: any;
 paidError: any;
 rest: any;
 count: any = 0;
 billcount:any = 0;
 deletecount:any=0;
 deleteFlag:any=false;
 selectedItem:{name:string}[]=[];



 //START SECTION 4 (VARIABLES)
 employeeName: any = "-- Choose From Employees --";
 date: any;
 startTime: any;
 endTime: any;
 employeeList: any[] = [];
 employeeNameError: any;
 dateError: any;
 employeeBillList: any[] = [];
 timeError: any;
 invoiceItems: { itemId: string, quantity: number, unitId: string, sellingPrice: number }[] = [];
 itemId: any;
 itemQuantity: any;
 unitId: any;
 itemSellingPrice: any;
 clientId: any;
 employeeId: any;
 response: any;




 // START SECTION 1&2 (FORM)
 billsForm = new FormGroup({
   billsDate: new FormControl("", Validators.required),
   billsNumber: new FormControl(""),
   clientName: new FormControl("-- Choose From Clients --", Validators.required),
   itemName: new FormControl("-- Choose From Items --", Validators.required),
   sellingPrice: new FormControl(""),
   unit: new FormControl(""),
   quantity: new FormControl("", Validators.required),
   discount: new FormControl(""),
   total: new FormControl(""),
   balance: new FormControl("")
 });




 // START SECTION 3 (FORM)
 discountForm = new FormGroup({
   billsTotal: new FormControl(""),
   percentageDiscount: new FormControl(""),
   valueDiscount: new FormControl(""),
   theNet: new FormControl(""),
   paidUp: new FormControl(""),
   theRest: new FormControl(""),
 });


 //  START SECTION 4 (FORM)
 employeeForm = new FormGroup({
   employeeName: new FormControl({ value: '', disabled: true }),
   date: new FormControl({ value: '', disabled: true }),
   startTime: new FormControl({ value: '', disabled: true }),
   endTime: new FormControl({ value: '', disabled: true })
 });



 //CONSTRUCTOR
 constructor(
  private unitService: UnitsWithAPIService,
  private clientService: ClientsWithAPIService,
  private invoiceService: InvoicesWithAPIService,
  private itemService: ItemWithAPIService,
  private userServices: UsersWithAPIService,
  private router: Router
) { }


 //ONINTI
 ngOnInit(): void {

   //   START SECTION 1&2 (oNiNIT)
  this.billDate = new Date().toISOString().substring(0, 10);

   this.invoiceService.GetBillNumber().subscribe({
     next:(response)=>{
       this.billNumber= response.invoiceNumber;
     },
     error:(error)=>{
       console.error(error);
     }
   });

   this.invoiceService.GetAllClients().subscribe({
     next: (response) => {
       const res = response.data;
       res.forEach((element: any) => {
         this.clientList.push(element);
       });
     }
   });

   this.invoiceService.GetAllItems().subscribe({
     next: (response) => {
       const res = response.data;
       res.forEach((element: any) => {
         this.itemList.push(element);
       });
     }
   })

   this.invoiceService.GetAllUnits().subscribe({
     next: (response) => {
       const res = response.data;
       res.forEach((element: any) => {
         this.unitList.push(element);
       });

     }
   });

   this.billsForm.get('billsNumber')?.disable();
   this.billsForm.get('total')?.disable();




   // START SECTION 3 (oNiNIT)
   this.discountForm.get('billsTotal')?.disable();
   this.discountForm.get('theNet')?.disable();
   this.discountForm.get('theRest')?.disable();



   //  START SECTION 4 (oNiNIT)
   this.invoiceService.fetchAllUsers().subscribe({
     next: (response) => {
       response.forEach((element: any) => {
         if (element.role == "Employee") {
           this.employeeList.push(element);
         }
       });
     }
   });

   this.userServices.getCurrentUser().subscribe({
    next:(response)=>{
      this.employeeName = response.firstName + " " + response.lastName;
      this.startTime = response.startTime;
      this.endTime = response.endTime;
      this.employeeId = response.id;
    },
    error:(error)=>{
      console.error('Error fetching current user:',error);
    },
   })
 }



 //   START SECTION 1&2 (FUNCTIONS)

 //CALCULATE TOTAL
 calculateTotal() {
   this.total = (this.sellingPrice * this.quantity);
 }

 //SET SELLINGPRICE AND UNIT AND CHECK QUANTITY
 ChangeItem() {
  this.total = 0;
   this.itemList.forEach(item => {
     if (item.name == this.itemName) {
       this.sellingPrice = item.sellingPrice;
       if (item.quantity == 0) {
         // this.outOFStockError="Out Of Stock";
         this.billsForm.get('quantity')?.disable();
         this.quantity = "Out Of Stock";
         this.quantityGraterError = null;

       }
       else {
         // this.outOFStockError=null;
         this.quantity = "";
         this.billsForm.get('quantity')?.enable();

       }
     }
   });
   this.itemList.forEach(item => {
     if (item.name == this.itemName) {
       this.codeNum = item.id;
     }
   });

   this.invoiceService.GetAllItems().subscribe({
     next: (response) => {
       response.data.forEach((element: any) => {
         if (this.itemName == element.name) {
           this.unit = element.unitName;
         }
       })
     }
   });
 }


 //ADD BILL
 AddSubmit(e: any) {
   e.preventDefault();

  // this.selectedItem.push({name:this.itemName})

   //BILLNUMBER
  //  this.billNumber++;
  //  this.invoiceService.GetBillNumber().subscribe({
  //    next: (response) => {
  //      console.log(response.invoiceNumber)
  //      this.billNumber = response.invoiceNumber;
  //    },
  //    error: (error) => {
  //      this.billNumber = 1000000;
  //    }
  //  });


          //CHECK VALIDATIONS
          if (this.billDate == null) {
            this.billRequiredError = 'Bill Date Is Required';
            return;
          }
          else
            this.billRequiredError = null;

          if (this.clientName === "-- Choose From Clients --") {
            this.clientRequiredError = "Client Name Is Required";
            return;
          }
          else
            this.clientRequiredError = null;

          if (this.itemName === "-- Choose From Items --") {
            this.itemRequiredError = "Item Name Is Required";
            return;
          }
          else
            this.itemRequiredError = null;

          if (this.quantity == null || this.quantity == "") {
            this.quantityRequiredError = 'Quantity Is Required';
            return;
          }
          else
            this.quantityRequiredError = null;

          if (this.quantity == 0) {
            this.quantityZeroError = 'Quantity  Must Be Greater Than Zero';
            return;
          }
          else
            this.quantityZeroError = null;

   //CALCULATE BALANCE
   this.itemList.forEach(item => {
     if (this.itemName == item.name) {
       this.balance = item.quantity - this.quantity;
     }
   })

   this.itemList.forEach(item => {
     if (item.name == this.itemName) {
       if (Number(this.quantity) > Number(item.quantity)) {
         this.quantityGraterError = `There are Only ${item.quantity} Products in the Stock`;
       }
       else
         this.quantityGraterError = null;
     }
   });


   //TO SHOW IN TABLE & FILTER SELECT ITEM
   this.itemList.forEach(item => {
     if (item.name == this.itemName) {
       if (this.billDate != null && this.clientName != "-- Choose From Clients --" && this.itemName != "-- Choose From Items --" && this.quantity != null && this.quantity != "" && this.quantity != 0 && Number(this.quantity) <= Number(item.quantity)) {
         this.billTableItem.push({ id: this.codeNum, name: this.itemName, unit: this.unit, quantity: this.quantity, sellingPrice: this.sellingPrice, discount: this.discount, total: this.total, balance: this.balance });

         //filtering select list
         this.itemList = this.itemList.filter(item =>
           item.name != this.itemName);
         this.itemName = "-- Choose From Items --";

       }
     }
   });


   // Item List Data
   this.invoiceService.GetAllItems().subscribe({
     next: (response) => {
       response.data.forEach((element: any) => {
         if (this.itemName == element.name) {
           this.itemId = element.id;
           this.itemSellingPrice = element.sellingPrice;
           this.itemQuantity = this.balance;
           this.invoiceService.GetAllUnits().subscribe({
             next: (unit) => {
               unit.data.forEach((u: any) => {
                 if (element.unitName == u.name) {
                   this.unitId = u.id;
                 }
               });
             }
           });
         }
       });
     }
   });

   //GET CLIENT ID
   this.invoiceService.GetAllClients().subscribe({
     next: (response) => {
       response.data.forEach((element: any) => {
         if (element.name == this.clientName) {
           this.clientId = element.id;

         }
       });
     }
   })


   //  START SECTION 3 (FUNCTIONS) *ADD BILL FUNCTION

   this.count++;
   var tableCount=0;
   this.billTableItem.forEach(bill => {
    tableCount++;
    // this.billsTotal += Number(bill.total);
     if (tableCount == this.count)  {
       this.billsTotal += Number(bill.total);
       this.calculateDiscount();
       this.calculateNet();
       this.calculateRest();
     }
      // else if(this.deleteFlag){
      //   console.log(`difference = ${this.count-this.deletecount}`)

      // if (++billcount==(this.count-this.deletecount)){
      //   console.log('hello')
      //   this.billsTotal += Number(bill.total)
      //  }
      //  this.deleteFlag=false;
    //  }

   });

   this.quantity = null;
   this.total = null;
   this.sellingPrice = null;
 }

 //SHOW ALL FUNCTION
 show() {
   this.router.navigate(['/'])
 }

 //DELETE TABLE ITEM
 delete(billId: any) {
  this.count--;
   this.billTableItem.forEach((item) => {
     if (item.id == billId) {
       this.billsTotal = this.billsTotal - item.total;
       this.invoiceService.GetAllItems().subscribe({
        next:(response)=>{
          const res = response.data;
       res.forEach((element: any) => {
        if(element.name==item.name)
         this.itemList.push(element);
       });
        }
       })
     }
   });
   this.billTableItem = this.billTableItem.filter(bill => bill.id != billId);
   this.deletecount++;
   this.deleteFlag=true;






 }

 //  START SECTION 3 (FUNCTIONS)

 // CHECK VALIDATIONS
 checkPercentage() {
   if (Number(this.percentage) < 0) {
     this.percentageError = "Percentage Discount Must Be Greater Than Or Equal Zero"
   }
   else
     this.percentageError = null;
 }

 checkValue() {
   if (Number(this.discountValue) < 0) {
     this.discountValueError = "Value Discount Must Be Greater Than Or Equal Zero"
   }
   else
     this.discountValueError = null;
 }

 checkPaid() {
   if (Number(this.paidUp) < 0) {
     this.paidError = "Paid Up Must Be Greater Than Or Equal Zero"
   }
   else
     this.paidError = null;
 }

 //CALCULATE DISCOUNT VALUE
 calculateDiscount() {
  if (this.percentage) {
   this.discountValue = this.billsTotal * ((this.percentage | 0) / 100);
  }
 }

 //CALCULATE NET VALUE
 calculateNet() {
   this.net = this.billsTotal - (this.discountValue | 0);
 }

 //CALCULATE ITEM DISCOUNT IN TABLE
 calculateItem() {
   this.billTableItem.forEach((item) => {
     item.discount = this.percentage;
     item.total = item.total - (item.total * (item.discount / 100));
   })
 }

 //CALCULATE REST VALUE
 calculateRest() {
  if(this.paidUp){
    this.rest = this.paidUp - this.net;
  }
 }



 //START SECTION 4 (FUNCTIONS)
 FinalSubmit(e: any) {
   e.preventDefault();

  // CHECK VALIDATION
   if (this.employeeName === "-- Choose From Employees --") {
     this.employeeNameError = "Employee Name Is Required";
     return;
   }
   else
     this.employeeNameError = null;

   if (this.billDate == null) {
     this.dateError = 'Date Is Required';
     return;
    }
    else{
      this.date = this.billDate;
      this.dateError = null;
     }


     //CHECK DATE
   const startDate = this.convertToDate(this.startTime);
   const endDate = this.convertToDate(this.endTime);
   if (endDate < startDate) {
     this.timeError = "End Time Must Be Greater Than Start Time";
     return;
   }
   else
     this.timeError = null;

    //GET EMPLOYEE ID
  // this.employeeList.forEach((element) => {
  //   if (element.firstName + " " + element.lastName == this.employeeName) {
  //     this.employeeId = element.id;
  //   }
  // });
  //  this.invoiceService.fetchAllUsers().subscribe({
  //    next: (response) => {
  //      response.forEach((element: any) => {
  //        if (this.employeeName == (element.firstName + " " + element.lastName)) {
  //          this.employeeId = element.id
  //          console.log(this.employeeId)
  //        }
  //      })
  //    }
  //  });

   //PUSH IN INVOICE LIST
   console.log('before push');
   console.log(this.billTableItem);
   console.log(this.invoiceItems);
   
   this.billTableItem.forEach((item) => {
    let invoiceItem = {
      itemId: item.id,
      quantity: Number(item.quantity),
      unitId: this.unitList.find(unit => unit.name == item.unit).id,
      sellingPrice: item.sellingPrice
    }
    this.invoiceItems.push(invoiceItem);
  });
  
  console.log('after push');
  console.log(this.billTableItem);
  console.log(this.invoiceItems);
  //return;

   //ADDING INVOICE
   const INVOICE = {
      date: this.date,
      billDate: this.billDate,
      paidUp: Number(this.paidUp),
      totalDiscount: this.discountValue,
      totalAmount: this.billsTotal,
      invoiceItems: this.invoiceItems,
      clientId: this.clientId,
      employeeId: this.employeeId
   }

   this.invoiceService.addInvoice(INVOICE).subscribe({
      next: () => {
        this.Cancel();
        console.log('Invoice Added Successfully');
      },
      error: (error) => {
        console.error(error);
      }
   });

 }
 Cancel() {
   this.billsForm.reset();
   this.discountForm.reset();
   this.employeeForm.reset();
   this.employeeName = "-- Choose From Employees --";
   this.clientName = "-- Choose From Clients --";
   this.itemName = "-- Choose From Items --";
   this.employeeNameError = null;
   this.dateError = null;
   this.timeError = null;
   this.employeeBillList = [];
   this.billTableItem = [];
   this.invoiceItems = [];
   this.clientList = [];
   this.itemList = [];
   this.unitList = [];
   this.ngOnInit();
 }

 //CONVERT TO DATE FUNCTION
 private convertToDate(timeString: string): Date {
   const [hours, minutes] = timeString.split(':').map(Number);
   const date = new Date();
   date.setHours(hours, minutes, 0, 0); // Set hours and minutes, seconds and milliseconds to 0
   return date;

 }


}



