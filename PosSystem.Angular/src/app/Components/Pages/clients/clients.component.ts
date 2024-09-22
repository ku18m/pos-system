import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ClientsWithAPIService } from '../../../services/clients-with-api.service';
import { IClients } from './IClients';
import { NavbarComponent } from '../navbar/navbar.component';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { FooterComponent } from '../footer/footer.component';

@Component({
  selector: 'app-clients',
  standalone: true,
  imports: [ReactiveFormsModule,FormsModule,NavbarComponent,SidebarComponent,FooterComponent],
  templateUrl: './clients.component.html',
  styleUrl: './clients.component.css'
})
export class ClientsComponent implements OnInit {
  name:any;
  phone:any="";
  number:any;
  address:any;
  clientsList:string[]=[];
  tokin:any=localStorage.getItem('token');

  requiredError:any;
  duplicateError:any;
  phoneError:any;
  phoneLengthError:any;
  addressError:any;


  constructor(private clientService:ClientsWithAPIService){}


  clientForm=new FormGroup ({
    name:new FormControl ("",Validators.required),
    phone:new FormControl ("",[Validators.required,Validators.minLength(14),Validators.maxLength(14)]),
    number:new FormControl (""),
    address:new FormControl ("",Validators.required)
  });

  ngOnInit(): void {
    this.clientService.getAllClients(this.tokin).subscribe({next:(clients:IClients[])=>{
    this.clientsList = clients.map(client => client.id)}});
    this.clientForm.get('number')?.disable();

}


  Submit(e:any){

    e.preventDefault();
    console.log(this.clientForm.status);
    if (this.name==null || this.name=="") {
      this.requiredError = 'Client Name Is Required';
      this.duplicateError=null;
    }
    else
        this.requiredError = null;


    if (this.clientsList.includes(this.name)) {
      this.duplicateError = `${this.name} has already existed before`;
    }
    else
        this.duplicateError=null;



    if(this.phone.length !=14)
    {
      this.phoneLengthError="Phone Must Be Just A 14 Digit Number"
    }
    else
    this.phoneLengthError=null;

    if (this.phone==null || this.phone=="") {
      this.phoneError = 'Phone Is Required';
      this.phoneLengthError=null;
    }
    else
        this.phoneError = null;

    if (this.address==null || this.address=="") {
      this.addressError = 'Address Is Required';
    }
    else
        this.addressError = null;


    //adding company
    if(this.name!=null && this.name!="" && !this.clientsList.includes(this.name) && this.phone.length ==14 && this.address!=null && this.address!="" && this.phone!=null && this.phone!="" ){
      const newClient:any = { name: this.name, phone:this.phone, number:this.number, address:this.address };

      this.clientService.addClient(this.tokin,this.name,this.number,this.phone,this.address).subscribe({
        next: () => {
          console.log('Client added successfully!');
          this.clientsList.push(newClient);
          this.clientForm.reset();
          this.name=null;
        },
        error: (error) => {
          console.error('Error adding client:', error);
        }
      });

    }
  }

  Cancel(){
    // this.clientForm.reset();
    this.name=null;
    this.phone="";
    this.address=null;
    this.requiredError=null;
    this.duplicateError=null;
    this.phoneError=null;
    this.phoneLengthError=null;
    this.addressError = null;
    console.log("canceled")
  }
}
