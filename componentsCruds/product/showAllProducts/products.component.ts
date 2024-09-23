import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Iproducts } from '../../models/iproducts';
import { ProductsService } from '../../services/dynamic-servuces.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
})
export class ProductsComponent implements OnInit {
  products: Iproducts[] = [];
  currentPage: number = 1;
  productsPerPage: number = 3;
  paginatedProducts: Iproducts[] = [];
  totalPages: any;

  constructor(public productService: ProductsService) {}

  ngOnInit(): void {
    this.fetchProducts();
  }

  fetchProducts(): void {

    this.productService.getProducts(this.currentPage, this.productsPerPage).subscribe({
      next: (response) => {
        this.products = response.products as Iproducts[]; 
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
     this.productService.deleteProduct(productId).subscribe({
      next: () => {
        this.fetchProducts();}})
  }
}
