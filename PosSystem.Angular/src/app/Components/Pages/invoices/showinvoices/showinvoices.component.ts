import { ChangeDetectorRef, Component } from '@angular/core';
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

  invoiceIdToDelete: string | null = null;

  constructor(public invoiceService: InvoicesWithAPIService, private router: Router, private changeDetector: ChangeDetectorRef) {}

  ngOnInit(): void {
  this.invoiceService.getAll().subscribe({
    next: (response) => {
      this.invoices = response.map((invoice: IInvoices) => {
        const itemsTotal = invoice.invoiceItems.reduce(
          (sum, item) => sum + (item.sellingPrice * item.quantity),
          0
        );
        const discount = invoice.totalDiscount || 0;
        const totalPrice = itemsTotal - discount;
        return {
          ...invoice,
          totalPrice
        };
      });


      this.updatePaginatedInvoices();
    },
    error: (err) => {
      console.error('Error fetching invoices:', err);
    }
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
        this.showNotification('success', 'Invoice deleted successfully');

        this.invoiceService.getAll().subscribe({
          next: (response) => {
            this.invoices = response as IInvoices[];
            this.updatePaginatedInvoices();
          },
        });
      },
      error: (error) => {
        this.showNotification('danger', 'Error deleting invoice');
        console.error('Error deleting invoice:', error);
      },
    });
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

  openDeleteModal(productId: string) {
    this.invoiceIdToDelete = productId;
    const modalElement = document.getElementById('deleteModal');
    if (modalElement) {
      const modal = new (window as any).bootstrap.Modal(modalElement);
      modal.show();
    }
  }

  confirmDelete() {
    if (this.invoiceIdToDelete) {
      this.deleteInvoiceHandler(this.invoiceIdToDelete);
      this.invoiceIdToDelete = null;
    }
  }

  hideModal() {
    const modalElement = document.getElementById('deleteModal');
    if (modalElement) {
      const modal = new (window as any).bootstrap.Modal(modalElement);
      modal.hide();
    }
  }
}
