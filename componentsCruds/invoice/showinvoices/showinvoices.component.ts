import { invoiceItems } from './../../models/IinvoiceItems';
import { Component } from '@angular/core';
import { Iinvoice } from '../../models/Iinvoices';
import { InvoiceService } from '../../services/invoice.service';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-showinvoices',
  standalone: true,
  imports: [CommonModule,RouterLink],
  templateUrl: './showinvoices.component.html',
  styleUrl: './showinvoices.component.css'
})
export class ShowinvoicesComponent {

 invoices: Iinvoice[] = [];
  currentPage: number = 1;
  invoicesPerPage: number = 3;
  paginatedInvoices: Iinvoice[] = [];

  constructor(public invoiceService: InvoiceService, private router: Router) {}

  ngOnInit(): void {
    this.invoiceService.getAllInvoices().subscribe({
      next: (response) => {
        this.invoices = response.map(invoice => ({
          ...invoice,
          totalPrice: invoice.invoiceItems.reduce((sum, item) => sum + (item.sellingPrice * item.quantity), 0) 
       }));
        this.updatePaginatedInvoices();
      },
    });
  }

  updatePaginatedInvoices(): void {
    const startIndex = (this.currentPage - 1) * this.invoicesPerPage;
    const endIndex = startIndex + this.invoicesPerPage;
    this.paginatedInvoices = this.invoices.slice(startIndex, endIndex);
  }

  setPage(page: number): void {
    if (page > 0 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePaginatedInvoices();
    }
  }

  get totalPages(): number {
    return Math.ceil(this.invoices.length / this.invoicesPerPage);
  }

  editInvoice(invoiceId: string) {
    this.router.navigate(['/invoice', invoiceId, 'edit']);
  }

  deleteInvoiceHandler(invoiceId: any) {
    this.invoiceService.deleteInvoice(invoiceId).subscribe({
      next: () => {
        this.invoiceService.getAllInvoices().subscribe({
          next: (response) => {
            this.invoices = response as Iinvoice[];
            this.updatePaginatedInvoices();
          },
        });
      },
    });
  }
}
