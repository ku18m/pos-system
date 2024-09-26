import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ItemWithAPIService } from '../../../../services/item-with-api.service';
import { ICompany } from '../../company/ICompany';
import { ITypes } from '../../types/ITypes';
import { CompanyWithAPIService } from '../../../../services/company-with-api.service';
import { TypesWithAPIService } from '../../../../services/types-with-api.service';
import { UnitsWithAPIService } from '../../../../services/units-with-api.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './productform.component.html',
  styleUrls: ['./productform.component.css'],
})
export class ProductFormComponent implements OnInit {
  productId: string | null = null;
  productForm!: FormGroup;
  companies: ICompany[] = [];
  types: ITypes[] = [];
  units: any[] = [];
  selectedCompanyId: string | null = null;
  selectedTypeId: string | null = null;
  selectedUnitId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private productServices: ItemWithAPIService,
    private companyService: CompanyWithAPIService,
    private typeService: TypesWithAPIService,
    private unitService: UnitsWithAPIService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.productForm = this.fb.group({
      companyId: ['', Validators.required],
      categoryId: [{ value: ''}, Validators.required],
      unitId: ['', Validators.required],
      name: ['', Validators.required],
      sellingPrice: [0, [Validators.required, Validators.min(1)]],
      buyingPrice: [0, [Validators.required, Validators.min(1)]],
      quantity: ['', [Validators.required, Validators.min(1)]],
      notes: [''],
    });

    this.companyService.getCompanies().subscribe({
      next: (response) => {
        this.companies = response;
      },
      error: (err) => {
        console.error('Error fetching companies:', err);
      }
    });

    this.unitService.getAll().subscribe({
      next: (response) => {
        this.units = response;
      },
      error: (err) => {
        console.error('Error fetching units:', err);
      }
    });

    this.productId = this.activatedRoute.snapshot.params['id'];
    if (this.productId) {
      this.productServices.getItemById(this.productId).subscribe({
        next: (response) => {
          // Patch form values
          this.productForm.patchValue({
            name: response.name,
            sellingPrice: response.sellingPrice,
            buyingPrice: response.buyingPrice,
            quantity: response.quantity,
            notes: response.notes,
            unitId: this.units.find(unit => unit.name === response.unitName)?.id,
          });

          // Find the corresponding company and category names
          const selectedCompany = this.companies.find(c => c.name === response.companyName);
          if (selectedCompany) {
            this.selectedCompanyId = selectedCompany.id;
            this.productForm.patchValue({ companyId: selectedCompany.id });
            this.fetchCompanyTypes(this.selectedCompanyId, () => {
              const selectedType = this.types.find(t => t.name === response.categoryName);
              if (selectedType) {
                this.selectedTypeId = selectedType.id;
                this.productForm.patchValue({ categoryId: selectedType.id });
              }
            });
          }

        },
        error: (err) => {
          console.error('Error fetching product by ID:', err);
        }
      });
    }
  }

  fetchCompanyTypes(companyId: string, setTypeId: () => void) {
    this.typeService.getTypesByCompanyId(companyId).subscribe({
      next: (response) => {
        this.types = response;
        setTypeId();
      },
      error: (err) => {
        console.error('Error fetching types for the company:', err);
      }
    });
  }

  onCompanyNameChange(event: any) {
    let companyName = event.target.value;

    if (companyName) {
      this.selectedCompanyId = companyName;
      this.productForm.get('categoryId')?.enable();

      this.typeService.getTypesByCompanyId(companyName).subscribe({
        next: (response) => {
          this.types = response;
        },
        error: (err) => {
          console.error('Error fetching types for the company:', err);
        },
      });
    } else {
      this.selectedCompanyId = null;
      this.productForm.get('categoryId')?.disable();
    }
  }

  productOperation() {
    if (this.productForm.valid) {
      const formData = this.productForm.value;

      const productData = formData;

      if (this.productId === undefined) {
        // Add new product
        this.productServices.addNewItem(productData).subscribe({
          next: () => {
            this.router.navigate(['/products']);
          },
          error: (err) => {
            console.error('Error adding product:', err);
          }
        });
      } else {
        // Update product
        productData.id = this.productId;
        this.productServices.updateItem(productData).subscribe({
          next: () => {
            this.router.navigate(['/products']);
          },
          error: (err) => {
            console.error('Error updating product:', err);
          }
        });
      }
    }
  }

  goBack() {
    this.router.navigate(['/products']);
  }
}

