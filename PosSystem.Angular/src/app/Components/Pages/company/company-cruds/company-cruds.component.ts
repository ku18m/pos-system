import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CompanyWithAPIService } from '../../../../services/company-with-api.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TypesWithAPIService } from '../../../../services/types-with-api.service';
import { ICompany } from '../ICompany';
import { response } from 'express';

@Component({
  selector: 'app-company-cruds',
  standalone: true,
  imports: [RouterLink, ReactiveFormsModule,CommonModule],
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
    private companiesService: CompanyWithAPIService,
    private typesService: TypesWithAPIService,
    private changeDetector: ChangeDetectorRef
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
    this.typesService.getTypesByCompanyId(this.companyId).subscribe({
      next: (types: any) => {
        this.types = types;
      },
      error: (err) => {
        console.error(err);
        this.showNotification('danger', err.error.errors.Name[0]);
      }
    });
  }

  fetchCompanyDetails(companyId: string): void {
    this.companiesService.getCompanyById(companyId).subscribe({
      next: (company: ICompany) => {
        this.companyForm.patchValue(company);
      },
      error: (err) => {
        console.error(err);
        this.showNotification('danger', err.error.errors.Name[0]);
    }});
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
    const companyData:ICompany = this.companyForm.value;

    companyData.id = this.companyId;

    if (this.companyForm.valid) {
      if (this.companyId) {
        this.companiesService.updateCompany(companyData).subscribe(() => {
          this.router.navigate(['/company/operations']);
        });
      }
    }
  }

  deleteCompanyTypeHandler(typeId: string): void {
    console.log(typeId);
    this.typesService.deleteType(typeId).subscribe({
      next: (response) => {
        this.fetchTypes();
        this.showNotification('success', 'Type deleted successfully');
        console.log(response);
      },
      error: (err) => {
        console.error(err);
        this.showNotification('danger', err.error.errors);
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/company/operations']);
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
