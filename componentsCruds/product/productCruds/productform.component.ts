import { types } from './../../../models/types';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ProductsService } from '../../../services/dynamic-servuces.service';
import { companies } from '../../../models/companies';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule,FormsModule],
  templateUrl: './productform.component.html',
  styleUrls: ['./productform.component.css'],
})
export class ProductFormComponent implements OnInit {
  productId: number | null = null;
  productForm!: FormGroup;
  companies: companies[] = [];
  types: types[] = [];
  selectedCompanyId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private productServices: ProductsService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.productForm = this.fb.group({
      companyName: ['', Validators.required],
      companyType: [{ value: '', disabled: true }, Validators.required],
      name: ['', Validators.required],
      sellingPrice: [0, [Validators.required, Validators.min(120)]],
      buyingPrice: [0, [Validators.required, Validators.min(120)]],
      quantity: ['', [Validators.required, Validators.min(1)]],
      notes: ['']
    });
    this.productServices.getCompanies().subscribe({
      next: (response) => {
        this.companies = response;
      },
      error: (err) => {
        console.error('Error fetching companies:', err);
      }
    });
   
    this.productId = +this.activatedRoute.snapshot.params['id'];
    if (this.productId && this.productId !== 0) {
      this.productServices.getProductById(this.productId).subscribe({
        next: (response) => {
          this.productForm.patchValue(response); 
        },
        error: (err) => {
          console.error('Error fetching product by ID:', err);
        }
      });
    }
  }
 
   onCompanyNameChange(event: any) {
    let companyName = event.target.value;
    let selectedCompany = this.companies.find(c => c.name === companyName);
    this.types=[];
    if (selectedCompany) {
      this.selectedCompanyId = selectedCompany.id;
      this.fetchCompanyTypes(selectedCompany.id);
    }
  }

fetchCompanyTypes(companyId: string) {
  this.productServices.getCompanyById(companyId).subscribe(company => {
    console.log(company)
     this.types =company.Types; 
    this.productForm.controls['companyType'].enable();
  });
}

 
  productOperation(): void {
    if (this.productForm.invalid) {
      this.productForm.markAllAsTouched();
      return;
    }

    const productData = this.productForm.value;

    if (this.productId === 0) {
      this.productServices.addNewProduct(productData).subscribe({
        next: () => this.router.navigate(['/products']),
        error: (err) => console.error('Error adding new product:', err)
      });
    } else {
      this.productServices.editProduct(productData).subscribe({
        next: () => this.router.navigate(['/products']),
        error: (err) => console.error('Error updating product:', err)
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/products']);
  }
  get f() {
    return this.productForm.controls;
  }

  onCompanyTypeChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    this.productForm.get('companyType')?.setValue(selectElement.value);
  }
}

  