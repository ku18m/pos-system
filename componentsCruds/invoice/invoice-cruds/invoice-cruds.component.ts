import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { ReactiveFormsModule, FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { InvoiceService } from '../../services/invoice.service';
import { Iinvoice } from '../../models/Iinvoices';

@Component({
  selector: 'app-invoice-cruds',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './invoice-cruds.component.html',
  styleUrls: ['./invoice-cruds.component.css']
})
export class InvoiceCrudsComponent implements OnInit {
  invoiceId: string | null = null;
  invoiceForm!: FormGroup;

  constructor(
    private activatedRoute: ActivatedRoute,
    private invoiceService: InvoiceService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.invoiceId = this.activatedRoute.snapshot.paramMap.get('id');
    this.initializeForm();

    if (this.invoiceId) {
      this.loadInvoice(this.invoiceId);
    }
  }

 initializeForm(): void {
    this.invoiceForm = new FormGroup({
      billNumber: new FormControl({ value: '', disabled: false }),
      creationDate: new FormControl('', Validators.required),
      clientName: new FormControl('', Validators.required),
      invoiceItems: new FormArray([])
    });
  }

 loadInvoice(id: string): void {
  this.invoiceService.getInvoiceById(id).subscribe(invoice => {
    this.invoiceForm.patchValue({
      billNumber: invoice.id,
      creationDate: new Date(invoice.creationDate).toISOString().split('T')[0],  
      clientName: invoice.clientName,
    });
    
    this.invoiceItems.clear();
 invoice.invoiceItems.forEach(item => {
      this.addInvoiceItem(item);
    });
  });
}
checkFormValidity(): void {
  const controls = this.invoiceForm.controls;
  for (const control in controls) {
    if (controls[control].invalid) {
      console.log(`${control} is invalid`, controls[control].errors);
    }
  }
}

addInvoiceItem(item?: any): void {
  const invoiceItems = this.invoiceForm.get('invoiceItems') as FormArray;
  const itemGroup = new FormGroup({
     itemId: new FormControl(item ? item.itemId : '',), 
    name: new FormControl(item ? item.name : '', Validators.required),
    sellingPrice: new FormControl(item ? item.sellingPrice : '', Validators.required),
    quantity: new FormControl(item ? item.quantity : '', [Validators.required, Validators.min(1)])
  });
  invoiceItems.push(itemGroup);
}

  removeInvoiceItem(index: number): void {
    const invoiceItems = this.invoiceForm.get('invoiceItems') as FormArray;
    invoiceItems.removeAt(index);
  }
   get invoiceItems(): FormArray {
    return this.invoiceForm.get('invoiceItems') as FormArray; 
  }

  saveInvoice(): void {
    if (this.invoiceForm.invalid) {
      this.invoiceForm.markAllAsTouched();
      console.log(this.invoiceForm.controls);
      return;
    }
    const invoiceData: Iinvoice = this.invoiceForm.value;
    if (!this.invoiceId) {
      this.invoiceService.addInvoice(invoiceData).subscribe({
        next: () => this.router.navigate(['/invoices']),
        error: (err) => console.error('Error adding invoice:', err)
      });
    } else {
      this.invoiceService.updateInvoice(this.invoiceId, invoiceData).subscribe({
        next: () => this.router.navigate(['/invoices']),
        error: (err) => console.error('Error updating invoice:', err)
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/invoices']);
  }

  get f() {
    return this.invoiceForm.controls;
  }
}
