import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CompanyWithAPIService } from '../../../services/company-with-api.service';
import { ICompany } from '../../../Models/ICompany';

@Component({
  selector: 'app-company',
  standalone: true,
  imports: [ReactiveFormsModule,FormsModule],
  templateUrl: './company.component.html',
  styleUrl: './company.component.css'
})
export class CompanyComponent implements OnInit {
  requiredError:string|null=null;
  duplicateError:string|null=null;
  companyName:any;
  companyNotes:any;
  companyList:string[]=[];

  companyForm=new FormGroup({
    name:new FormControl("",[Validators.required]),
    notes:new FormControl("")
  })
  


  constructor(private companyService:CompanyWithAPIService){}
  ngOnInit() {  
    // Load existing companies on component initialization  
    this.companyService.getAllCompanies().subscribe({next:(companies:ICompany[])=>{  
      this.companyList = companies.map(company => company.name);  
    },
    error:(error) => {  
      console.error('Error fetching companies:', error);  
    },  
  });
    
  }  
  Submit(e:any){
    e.preventDefault();
    console.log(this.companyForm.status);
    if (this.companyName==null || this.companyName=="") {  
      this.requiredError = 'Company Name Is Required';
      this.duplicateError=null;
       
      
    }  
    else
        this.requiredError = null;
    if (this.companyList.includes(this.companyName)) {  
      this.duplicateError = `${this.companyName} has already existed before`; 
      
       
    } 
    else
        this.duplicateError=null; 
    //adding company
    if(this.companyName!=null && this.companyName!="" && !this.companyList.includes(this.companyName)){

    
    const newCompany:any = { name: this.companyName, notes:this.companyNotes };  

    this.companyService.addCompany(newCompany).subscribe({  
      next: () => {  
        console.log('Company added successfully!');  
        this.companyList.push(newCompany);  
        this.companyForm.reset();   
      },  
      error: (error) => {  
        console.error('Error adding company:', error);  
      }  
    });
  }  
  }  

  Cancel(){
    this.companyForm.reset(); 
    this.requiredError=null;
    this.duplicateError=null;
    console.log("canceled")
  }
    
  }


