import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CompaniesService } from '../../services/companies.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-company-cruds',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './company-cruds.component.html',
  styleUrl: './company-cruds.component.css'
})
export class CompanyCrudsComponent implements OnInit {
  companyForm: FormGroup;
  companyId: string="";
  types: any[] = []; 

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private companiesService: CompaniesService
  ) {
    this.companyForm = this.fb.group({
      name: [''],
      types: this.fb.array([]),
    });
  }

  ngOnInit(): void {
    this.companyId = this.route.snapshot.paramMap.get('id')!;
    this.fetchTypes();
    if (this.companyId) {
      this.fetchCompanyDetails(this.companyId);
    }
  }

  fetchTypes(): void {
    this.companiesService.getTypes().subscribe((response) => {
      this.types = response; 
    });
  }

  fetchCompanyDetails(companyId: string): void {
    this.companiesService.getCompanyById(companyId).subscribe((company) => {
      this.companyForm.patchValue({
        name: company.name,
        types: company.Types.map((type: { id: any; }) => type.id), 
      });
    });
  }

  onTypeChange(event: any): void {
    const typesArray: FormArray = this.companyForm.get('types') as FormArray;
    if (event.target.checked) {
      typesArray.push(this.fb.control(event.target.value));
    } else {
      const index = typesArray.controls.findIndex(control => control.value === event.target.value);
      typesArray.removeAt(index);
    }
  }

  companyOperation(): void {
    if (this.companyForm.valid) {
      if (this.companyId) {
        this.companiesService.updateCompany(this.companyId, this.companyForm.value).subscribe(() => {
          this.router.navigate(['/companies']);
        });
      } else {
        this.companiesService.addCompany(this.companyForm.value).subscribe(() => {
          this.router.navigate(['/companies']);
        });
      }
    }
  }

  goBack(): void {
    this.router.navigate(['/companies']);
  }
}
