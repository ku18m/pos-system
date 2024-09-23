import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ClientService } from '../../services/client.service'; 
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { IClient } from '../../models/iclient'; 

@Component({
  selector: 'app-show-client',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, HttpClientModule, RouterLinkActive],
  templateUrl: './show-clients.component.html', 
  styleUrls: ['./show-clients.component.css'], 
  providers: [ClientService]
})
export class ShowClientComponent implements OnInit {
  clients: IClient[] = [];
  currentPage: number = 1;
  clientsPerPage: number = 3; 
  paginatedClients: IClient[] = [];

  constructor(public clientService: ClientService, private router: Router) {}

  ngOnInit(): void {
    this.clientService.getAllClients().subscribe({
      next: (response) => {
        this.clients = response as IClient[];
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
    this.router.navigate(['/client', clientId, 'edit']); 
  }

  deleteClientHandler(clientId: any) {
    this.clientService.deleteClient(clientId).subscribe({
      next: () => {
        this.clientService.getAllClients().subscribe({
          next: (response) => {
            this.clients = response as IClient[];
            this.updatePaginatedClients();
          },
        });
      },
    });
  }
}