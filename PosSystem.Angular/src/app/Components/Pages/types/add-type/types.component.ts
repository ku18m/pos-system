import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CompanyWithAPIService } from '../../../../services/company-with-api.service';
import { TypesWithAPIService } from '../../../../services/types-with-api.service';
import { ITypesBack } from '../ITypesBack';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-types',
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './types.component.html',
  styleUrls: ['./types.component.css']
})
export class TypesComponent implements OnInit {
  companyList: any[] = [];
  typeList: any[] = [];
  companyReqFlag: boolean = false;
  duplicateError: string | null = null;
  TypeRequiredError: string | null = null;
  token: any = localStorage.getItem('token');

  typeForm = new FormGroup({
    companyName: new FormControl("", [Validators.required]),
    name: new FormControl("", [Validators.required]),
    notes: new FormControl("")
  });

  constructor(private companyService: CompanyWithAPIService, private typeService: TypesWithAPIService, private changeDetector: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.loadCompanies();
    this.loadTypes();
  }

  loadCompanies() {
    this.companyService.getAllCompanies(this.token).subscribe({
      next: (companies) => {
        this.companyList = companies;
      },
      error: (err) => console.error('Error loading companies:', err)
    });
  }

  loadTypes() {
    this.typeService.getAllTypes(this.token).subscribe({
      next: (types) => {
        this.typeList = types;
      },
      error: (err) => console.error('Error loading types:', err)
    });
  }

  Submit(e: Event) {
    e.preventDefault();
    this.companyReqFlag = !this.typeForm.get('companyName')?.value;

    const typeName = this.typeForm.get('name')?.value;
    const companyId = this.typeForm.get('companyName')?.value;

    if (!typeName) {
      this.TypeRequiredError = 'Type Name Is Required';
      this.duplicateError = null;
      return;
    }

    this.checkDuplicateType(typeName);

    // Adding type
    if (companyId && typeName && !this.duplicateError) {
      const newType: ITypesBack = {
        name: typeName,
        notes: this.typeForm.get('notes')?.value || null,
        companyId: companyId
      };

      this.typeService.addType(newType).subscribe({
        next: (response) => {
          this.showNotification('success', 'Type added successfully');
          this.resetForm();
        },
        error: (err) => {
          this.showNotification('danger', 'Error adding type');
          console.error('Error adding type:', err);
        }
      });
    }
  }

  checkDuplicateType(typeName: string) {
    this.duplicateError = this.typeList.some(type => type.name === typeName)
      ? `${typeName} has already existed before`
      : null;
    this.TypeRequiredError = null;
  }

  resetForm() {
    this.typeForm.reset();
    this.ngOnInit();
    this.companyReqFlag = false;
    this.duplicateError = null;
    this.TypeRequiredError = null;
  }

  Cancel() {
    this.resetForm();
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
