import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ItemWithAPIService } from '../../../../services/item-with-api.service';
import { IItems } from '../IItems';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
})
export class ProductsComponent implements OnInit {
  products: IItems[] = [];
  productIdToDelete: string | null = null;
  currentPage: number = 1;
  productsPerPage: number = 3;
  // paginatedProducts: Iproducts[] = [];
  totalPages: any;

  constructor(public productService: ItemWithAPIService, private changeDetector: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.fetchProducts();
  }

  fetchProducts(): void {

    this.productService.getItemsPage(this.currentPage, this.productsPerPage).subscribe({
      next: (response) => {
        this.products = response.data as IItems[];
        this.totalPages = response.totalPages;
      },
    });
  }

  setPage(page: number): void {
    if (page > 0 && page <= this.totalPages) {
      this.currentPage = page;
      this.fetchProducts();
    }
  }

  deleteProductHandler(productId: any) {
    this.productService.deleteItem(productId).subscribe({
      next: () => {
        this.fetchProducts();
        this.showNotification('success', 'Item deleted successfully');
        this.hideModal();
      },
      error: (error) => {
        this.showNotification('danger', 'Error deleting item, item associated with invoice');
        console.error('Error deleting item:', error);
      },
    });
  }

  openDeleteModal(productId: string) {
    this.productIdToDelete = productId;
    const modalElement = document.getElementById('deleteModal');
    if (modalElement) {
      const modal = new (window as any).bootstrap.Modal(modalElement);
      modal.show();
    }
  }

  confirmDelete() {
    if (this.productIdToDelete) {
      this.deleteProductHandler(this.productIdToDelete);
      this.productIdToDelete = null;
    }
  }

  hideModal() {
    const modalElement = document.getElementById('deleteModal');
    if (modalElement) {
      const modal = new (window as any).bootstrap.Modal(modalElement);
      modal.hide();
    }
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
