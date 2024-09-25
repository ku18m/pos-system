import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CompanyWithAPIService } from '../../../services/company-with-api.service';
import { TypesWithAPIService } from '../../../services/types-with-api.service';

import { NavbarComponent } from '../navbar/navbar.component';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { FooterComponent } from '../footer/footer.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-types',
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, NavbarComponent, SidebarComponent, FooterComponent],
  templateUrl: './types.component.html',
  styleUrl: './types.component.css'
})
export class TypesComponent implements OnInit {

  companyList: any[]=[];
  companyValue: any = "-- Choose From Companies --";
  companyRequiredError: any = "Company Name Is Required";
  companyReqFlag: boolean = false;
  notes: any;
  typeName: any;
  TypeList: any[]=[];
  duplicateError: any;
  TypeRequiredError: string | null = null;
  companyID:any;

  constructor(private companyService: CompanyWithAPIService, private typeService: TypesWithAPIService, private router:Router) { }

  ngOnInit(): void {
    this.companyService.getAllCompanies().subscribe({
      next: (element) => {
        for (var i = 0; i < element.data.length; i++) {
          this.companyList.push(element.data[i].name);
        }
      }
    });
    this.typeService.getAllTypes().subscribe({  
      next: (element) => {   
          for(var i=0;i<element.length;i++){
            this.TypeList.push(element[i].name);
          }
      }});

      
  }


  typeForm = new FormGroup({
    companyName: new FormControl(""),
    name: new FormControl("", [Validators.required]),
    notes: new FormControl("")
  });


  show(){
    this.router.navigate(['/'])
  }

  onChange(){
    
    this.companyService.GetCompany(this.companyValue).subscribe({
      next:(element)=>{
        this.companyID=element.id;
      }
    });
  }

  Submit(e: any) {
    e.preventDefault();
    if (this.companyValue === "-- Choose From Companies --") {
      this.companyReqFlag = true;
    }
    else
      this.companyReqFlag = false;

    if (this.typeName == null || this.typeName == "") {
      this.TypeRequiredError = 'Type Name Is Required';
      this.duplicateError = null;

    }
    else
      this.TypeRequiredError = null;

      this.companyList.forEach((company)=>{
        if(company==this.typeName)
        {
          this.duplicateError = `${this.typeName} has already existed before`;
          
        }
        
  
      })
    //adding type
    if (this.companyValue != "-- Choose From Companies --" && this.typeName != null && this.typeName != "" ) { 
        this.typeService.addTypeWithNotes(this.typeName, this.notes,this.companyID).subscribe(  
          response => {  
            console.log('Type added successfully:', response);  
            this.typeName = ''; // Clear input  
            this.notes = ''; // Clear input 
            this.duplicateError=null; 
          },  
          error => {  
            console.error('Error adding type:', error);  
          }  
        ); 
    }
  }
  Cancel() {
    this.typeForm.reset();
    this.typeName = null;
    this.companyValue = "-- Choose From Companies --";
    this.companyRequiredError = null;
    this.duplicateError = null;
    this.TypeRequiredError = null;
  }
}


