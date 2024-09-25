import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ClientsWithAPIService } from '../../../../services/clients-with-api.service';
import { IClients } from '../IClients';

@Component({
  selector: 'app-show-client',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterLink,
    HttpClientModule,
    RouterLinkActive,
  ],
  templateUrl: './show-clients.component.html',
  styleUrls: ['./show-clients.component.css'],
  providers: [],
})
export class ShowClientComponent implements OnInit {
  clients: IClients[] = [];
  currentPage: number = 1;
  clientsPerPage: number = 3;
  paginatedClients: IClients[] = [];

  constructor(
    public clientService: ClientsWithAPIService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.clientService.getAllClients().subscribe({
      next: (response) => {
        this.clients = response as IClients[];
        console.log(this.clients);
        this.updatePaginatedClients();
      },
    });
  }

  updatePaginatedClients(): void {
    const startIndex = (this.currentPage - 1) * this.clientsPerPage;
    const endIndex = startIndex + this.clientsPerPage;
    this.paginatedClients = this.clients.slice(startIndex, endIndex);
  }

  setPage(page: number): void {
    if (page > 0 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePaginatedClients();
    }
  }

  get totalPages(): number {
    return Math.ceil(this.clients.length / this.clientsPerPage);
  }

  editClient(clientId: string) {
    this.router.navigate(['/clients', 'operations', clientId]);
  }

  deleteClientHandler(clientId: any) {
    this.clientService.deleteClient(clientId).subscribe({
      next: () => {
        this.clientService.getAllClients().subscribe({
          next: (response) => {
            this.clients = response as IClients[];
            this.updatePaginatedClients();
          },
        });
      },
    });
  }
}
