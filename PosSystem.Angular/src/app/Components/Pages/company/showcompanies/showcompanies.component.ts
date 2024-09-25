import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CompanyWithAPIService } from '../../../../services/company-with-api.service';
import { ICompany } from '../ICompany';

@Component({
  selector: 'app-companies',
  standalone: true,
  imports: [RouterLink, CommonModule ,RouterLinkActive],
  templateUrl: './showcompanies.component.html',
  styleUrls: ['./showcompanies.component.css'],
})
export class CompaniesComponent implements OnInit {
  paginatedCompanies: ICompany[] = [];
  currentPage: number = 1;
  companiesPerPage: number = 3;
  totalPages: number = 0;

  constructor(private fb: FormBuilder, private companiesService: CompanyWithAPIService,private router: Router) { }

  ngOnInit(): void {
    this.fetchCompanies();
  }

  fetchCompanies(): void {
    this.companiesService.getCompaniesPage(this.currentPage, this.companiesPerPage).subscribe((response) => {
      this.paginatedCompanies = response.data;
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

  deleteCompanyHandler(companyId: string): void {
    this.companiesService.deleteCompany(companyId).subscribe(() => {
      this.fetchCompanies();
    });
  }
}
