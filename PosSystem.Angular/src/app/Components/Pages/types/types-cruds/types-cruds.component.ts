import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TypesWithAPIService } from '../../../../services/types-with-api.service';
import { ITypes } from '../ITypes';
import { CompanyWithAPIService } from '../../../../services/company-with-api.service';
import { ICompany } from '../../company/ICompany';


@Component({
  selector: 'app-types-cruds',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './types-cruds.component.html',
  styleUrl: './types-cruds.component.css'
})
export class TypesCrudsComponent implements OnInit {
  typeForm: FormGroup;
  typeId: string | null = null;
  types: ITypes[] = [];
  companies: ICompany[] = [];

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private typesService: TypesWithAPIService,
    private companyService: CompanyWithAPIService,
    private changeDetector: ChangeDetectorRef
  ) {

    this.typeForm = this.fb.group({
      id: [''],
      name: ['', Validators.required],
      companyName: [''],
      companyId: [''],
      notes: [''],
    });
  }

  ngOnInit(): void {
    this.typeId = this.route.snapshot.paramMap.get('id')!;

    if (this.typeId) {
      this.fetchTypeDetails(this.typeId);
    }

    this.companyService.getCompanies().subscribe({
      next: (response) => {
        this.companies = response;
      },
      error: (err) => {
        console.error('Error fetching companies:', err);
      }
    });

    this.typeForm.get('companyName')?.disable();
  }

  fetchTypeDetails(typeId: string): void {
    this.typesService.getTypeById(typeId).subscribe((type: ITypes) => {
      this.typeForm.patchValue({
        id: type.id,
        name: type.name,
        companyName: type.companyName || '',
        companyId: this.companies.find((company) => company.name === type.companyName)?.id || '',
        notes: type.notes,
      });
    });
  }

  typeOperation(): void {
    if (this.typeForm.valid) {
      const payload = {
      ...this.typeForm.value,
      }
      if (this.typeId) {
        payload.id = this.typeId;
        this.typesService.updateType(payload).subscribe({
          next: () => {
            this.showNotification('success', 'Type updated successfully');
          },
          error: (err) => {
            console.error('Error updating type:', err);
            this.showNotification('danger', `Error updating type ${err.error.message}`);
          }
        });
      } else {
        this.typesService.addType(payload).subscribe(() => {
          this.router.navigate(['/types']);
        });
      }
    }
  }

  goBack(): void {
    this.router.navigate(['/types/operations']);
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
