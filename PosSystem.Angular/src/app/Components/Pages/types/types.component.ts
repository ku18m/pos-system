import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CompanyWithAPIService } from '../../../services/company-with-api.service';
import { ITypes } from '../../../Models/ITypes';
import { TypesWithAPIService } from '../../../services/types-with-api.service';

@Component({
  selector: 'app-types',
  standalone: true,
  imports: [ReactiveFormsModule,FormsModule],
  templateUrl: './types.component.html',
  styleUrl: './types.component.css'
})
export class TypesComponent implements OnInit {
  
  companyList:any;
  companyValue:any="-- Choose From Companies --";
  companyRequiredError:any="Company Name Is Required";
  companyReqFlag:boolean=false;
  notes:any;
  typeName:any;
  TypeList:any;
  duplicateError:any;
  TypeRequiredError:string|null=null;

  constructor(private companyService:CompanyWithAPIService, private typeService:TypesWithAPIService){}

  ngOnInit(): void {
    this.companyService.getAllCompanies().subscribe({next:(response)=> this.companyList=response});
    this.typeService.getAllTypes().subscribe({next:(types:ITypes[])=>{  
      this.TypeList = types.map(type => type.name)}});
  }


  typeForm=new FormGroup({
    companyName:new FormControl(""),
    name:new FormControl("",[Validators.required]),
    notes:new FormControl("")
  })

  Submit(e:any){
    e.preventDefault();
    if(this.companyValue === "-- Choose From Companies --" )
    {
      this.companyReqFlag=true;
    }
    else
    this.companyReqFlag=false;

    if (this.typeName==null || this.typeName=="") {  
      this.TypeRequiredError = 'Type Name Is Required';
      this.duplicateError=null;
        
    }  
    else
        this.TypeRequiredError = null;

    if (this.TypeList.includes(this.typeName)) {  
      this.duplicateError = `${this.typeName} has already existed before`; 
        
    } 
    else
        this.duplicateError=null; 

    //adding type
    if(this.companyValue != "-- Choose From Companies --" && this.typeName!=null && this.typeName!="" && !this.TypeList.includes(this.typeName))
      {
        const newType:any = { companyName: this.companyValue, name:this.typeName, notes:this.notes  };  

        this.typeService.addType(newType).subscribe({  
          next: () => {  
            console.log('Type added successfully!');  
            this.TypeList.push(newType);  
            this.typeForm.reset(); 
            this.typeName=null;
            this.companyValue="-- Choose From Companies --";  
          },  
          error: (error) => {  
            console.error('Error adding type:', error);  
          }  
        });  
      }
  }  
  Cancel(){
    this.typeForm.reset(); 
    this.typeName=null;
    this.companyValue="-- Choose From Companies --"; 
    this.companyRequiredError=null;
    this.duplicateError=null;
    this.TypeRequiredError=null;
  }
  }

  
