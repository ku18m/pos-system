import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CompanyWithAPIService } from '../../../../services/company-with-api.service';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-company',
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './company.component.html',
  styleUrl: './company.component.css'
})
export class CompanyComponent implements OnInit {
  requiredError: string | null = null;
  duplicateError: string | null = null;
  companyName: any;
  companyNotes: any;
  companyList: any[] = [];
  tokin:any=localStorage.getItem('token');

  companyForm = new FormGroup({
    name: new FormControl("", [Validators.required]),
    notes: new FormControl("")
  })



  constructor(private companyService: CompanyWithAPIService, private changeDetector: ChangeDetectorRef) { }
  ngOnInit() {
    this.companyService.getAllCompanies(this.tokin).subscribe({
      next: (element) => {
          for(var i=0;i<element.data.length;i++){
            this.companyList.push(element.data[i].name);
          }
      }});

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
        this.companyService.addCompanyWithNotes(this.tokin,this.companyName, this.companyNotes).subscribe(
          response => {
            console.log('Company added successfully:', response);
            this.showNotification('success', 'Company added successfully');
            this.companyName = ''; // Clear input
            this.companyNotes = ''; // Clear input
            this.duplicateError=null;
          },
          err => {
            console.error('Error adding company:', err);
            this.showNotification('danger', err.error.errors.Name[0]);
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
