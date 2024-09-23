import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { CompaniesService } from '../../services/companies.service'; 
import { CommonModule } from '@angular/common';
import { companies } from '../../models/icompany'; 

@Component({
  selector: 'app-companies',
  standalone: true,
  imports: [RouterLink, CommonModule ,RouterLinkActive],
  templateUrl: './showcompanies.component.html',
  styleUrls: ['./showcompanies.component.css'],
})
export class CompaniesComponent implements OnInit {
  companiesForm: FormGroup;
  paginatedCompanies: companies[] = [];
  currentPage: number = 1;
  companiesPerPage: number = 3; 
  totalPages: number = 0;

  constructor(private fb: FormBuilder, private companiesService: CompaniesService,private router: Router) {
    this.companiesForm = this.fb.group({
      id: [''],
      name: ['', Validators.required],
      types: this.fb.array([]),
    });
  }

  ngOnInit(): void {
    this.fetchCompanies();
  }

  fetchCompanies(): void {
    this.companiesService.getCompanies(this.currentPage).subscribe((response) => {
      this.paginatedCompanies = response.companies; 
      this.totalPages = response.totalPages; 
    });
  }

  setPage(page: number): void {
    if (page > 0 && page <= this.totalPages) {
      this.currentPage = page;
      this.fetchCompanies(); 
    }
  }
editCompany(companyId: string): void {
    this.router.navigate(['/company', companyId, 'edit']);
  }
  addType(): void {
    const typesArray = this.companiesForm.get('types') as FormArray;
    typesArray.push(this.fb.group({
      id: [''],
      name: ['', Validators.required],
    }));
  }

  removeType(index: number): void {
    const typesArray = this.companiesForm.get('types') as FormArray;
    typesArray.removeAt(index);
  }

  onSubmit(): void {
    if (this.companiesForm.valid) {
      const companyData = this.companiesForm.value;
      this.companiesService.saveCompany(companyData).subscribe(() => {
        this.fetchCompanies(); 
        this.companiesForm.reset(); 
      });
    }
  }

  deleteCompanyHandler(companyId: string): void {
    this.companiesService.deleteCompany(companyId).subscribe(() => {
      this.fetchCompanies(); 
    });
  }
}
