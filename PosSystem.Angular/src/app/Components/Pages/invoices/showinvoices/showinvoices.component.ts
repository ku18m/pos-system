import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { InvoicesWithAPIService } from '../../../../services/invoices-with-api.service';
import { IInvoices } from '../IInvoices';
import { IInvoiceItem } from '../IInvoiceItem';

@Component({
  selector: 'app-showinvoices',
  standalone: true,
  imports: [CommonModule,RouterLink],
  templateUrl: './showinvoices.component.html',
  styleUrl: './showinvoices.component.css'
})
export class ShowinvoicesComponent {

  invoices: IInvoices[] = [];
  currentPage: number = 1;
  invoicesPerPage: number = 3;
  paginatedInvoices: IInvoices[] = [];

  constructor(public invoiceService: InvoicesWithAPIService, private router: Router) {}

  ngOnInit(): void {
    this.invoiceService.getAll().subscribe({
      next: (response) => {
        this.invoices = response.map((invoice: IInvoices) => ({
          ...invoice,
          totalPrice: invoice.invoiceItems.reduce((sum: number, item: IInvoiceItem) => sum + (item.sellingPrice * item.quantity), 0)
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
    this.router.navigate(['/invoices', 'operations', invoiceId]);
  }

  deleteInvoiceHandler(invoiceId: any) {
    this.invoiceService.delete(invoiceId).subscribe({
      next: () => {
        this.invoiceService.getAll().subscribe({
          next: (response) => {
            this.invoices = response as IInvoices[];
            this.updatePaginatedInvoices();
          },
        });
      },
    });
  }
}
