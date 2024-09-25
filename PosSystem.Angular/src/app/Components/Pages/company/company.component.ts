import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CompanyWithAPIService } from '../../../services/company-with-api.service';
import { NavbarComponent } from '../navbar/navbar.component';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { FooterComponent } from '../footer/footer.component';
import { Router } from '@angular/router';


@Component({
  selector: 'app-company',
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, NavbarComponent, SidebarComponent, FooterComponent],
  templateUrl: './company.component.html',
  styleUrl: './company.component.css'
})
export class CompanyComponent implements OnInit {
  requiredError: string | null = null;
  duplicateError: string | null = null;
  companyName: any;
  companyNotes: any;
  companyList: any[] = [];
  

  companyForm = new FormGroup({
    name: new FormControl("", [Validators.required]),
    notes: new FormControl("")
  })



  constructor(private companyService: CompanyWithAPIService, private router:Router) { }
  ngOnInit() {
    this.companyService.getAllCompanies().subscribe({  
      next: (element) => {
          for(var i=0;i<element.data.length;i++){
            this.companyList.push(element.data[i].name);
          }
      }});

    }

    show(){
      this.router.navigate(['/'])
    }

  Submit(e: any) {
    e.preventDefault();
    console.log(this.companyForm.status);
    if (this.companyName == null || this.companyName == "") {
      this.requiredError = 'Company Name Is Required';
      this.duplicateError = null;


    }
    else
      this.requiredError = null;

      console.log(this.companyList)
      
      

    this.companyList.forEach((company)=>{
      if(company==this.companyName)
      {
        this.duplicateError = `${this.companyName} has already existed before`;
        
      }
      

    })
    
      
    //adding company
    if (this.companyName != null && this.companyName != "") {  
        this.companyService.addCompanyWithNotes(this.companyName, this.companyNotes).subscribe(  
          response => {  
            console.log('Company added successfully:', response);  
            this.companyName = ''; // Clear input  
            this.companyNotes = ''; // Clear input 
            this.duplicateError=null; 
          },  
          error => {  
            console.error('Error adding company:', error);  
          }  
        ); 

  }
}

  Cancel() {
    this.companyForm.reset();
    this.requiredError = null;
    this.duplicateError = null;
    console.log("canceled")
  }

}


