import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { ReactiveFormsModule, FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { InvoicesWithAPIService } from '../../../../services/invoices-with-api.service';
import { IInvoices } from '../IInvoices';

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
  selectedInvoice: IInvoices | undefined;

  constructor(
    private activatedRoute: ActivatedRoute,
    private invoiceService: InvoicesWithAPIService,
    private router: Router,
    private changeDetector: ChangeDetectorRef
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
      number: new FormControl({ value: '', disabled: true }),
      billDate: new FormControl('', Validators.required),
      clientName: new FormControl({ value: '', disabled: true }, Validators.required),
      paidUp:new FormControl ('', [Validators.required, Validators.min(0)]),
      totalDiscount: new FormControl('', [Validators.min(0)]),
      invoiceItems: new FormArray([])
    });
  }

 loadInvoice(id: string): void {
  this.invoiceService.getById(id).subscribe(invoice => {
    this.selectedInvoice = invoice;
    this.invoiceForm.patchValue({
      number: invoice.number,
      billDate: new Date(invoice.billDate).toISOString().split('T')[0],
      clientName: invoice.clientName,
      paidUp:invoice.paidUp ,
      totalDiscount: invoice.totalDiscount || 0
    });

    this.invoiceItems.clear();
    invoice.invoiceItems.forEach((item: any) => {
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
    invoiceItemId: new FormControl(item ? item.invoiceItemId : ''),
    unitId: new FormControl(item ? item.unitId : ''),
    itemId: new FormControl(item ? item.itemId : '',),
    name: new FormControl(item ? item.itemName : '', Validators.required),
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
    console.log('before calc:', this.selectedInvoice);
    if (this.invoiceForm.invalid) {
      this.invoiceForm.markAllAsTouched();
      console.log(this.invoiceForm.controls);
      return;
    }

    let invoiceData: IInvoices =  {
      ...this.invoiceForm.value,
      totalDiscount: this.invoiceForm.get('totalDiscount')?.value || 0
    };

    const itemsTotal = invoiceData.invoiceItems.reduce(
      (sum, item) => sum + (item.sellingPrice * item.quantity),
      0
    );

    if (this.selectedInvoice) {
      this.selectedInvoice.totalAmount = (itemsTotal || 0);
      this.selectedInvoice.totalDiscount = invoiceData.totalDiscount;
      this.selectedInvoice.net = (itemsTotal - invoiceData.totalDiscount);
    }

    if (!this.invoiceId) {
      this.invoiceService.addInvoice(invoiceData).subscribe({
        next: () => this.router.navigate(['/invoices/operations']),
        error: (err) => console.error('Error adding invoice:', err)
      });
    } else {
      console.log('after calc:', this.selectedInvoice);
      invoiceData = { ...this.selectedInvoice, ...invoiceData };
      this.invoiceService.update(invoiceData).subscribe({
        next: () => {
          this.showNotification('success', 'Invoice updated successfully');
        },
        error: (err) => {
          this.showNotification('danger', 'Error updating invoice');
          console.error('Error updating invoice:', err);
        }
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/invoices/operations']);
  }

  get f() {
    return this.invoiceForm.controls;
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
