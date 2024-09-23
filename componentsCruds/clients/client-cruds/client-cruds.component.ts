import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ClientService } from '../../services/client.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IClient } from '../../models/iclient';

@Component({
  selector: 'app-add-client',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink,HttpClientModule],
  templateUrl: './client-Cruds.component.html',
  styleUrls: ['./client-Cruds.component.css']
})
export class ClientCrudsComponent implements OnInit {
 
  clientId: number | null = null;
  clientForm!: FormGroup;

  constructor(
    private activatedRoute: ActivatedRoute,
    private clientService: ClientService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.clientId = +this.activatedRoute.snapshot.params['id'];
    this.clientForm = new FormGroup({
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      email: new FormControl('', [
        Validators.required,
        Validators.pattern(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/)  
      ]),
      address: new FormControl(''),
      phone: new FormControl('', [
        Validators.required,
        Validators.pattern(/^(010|012|011|015)\d{8}$/)  
      ])
    });

    if (this.clientId && this.clientId !== 0) {
      this.loadClientData(this.clientId);
    }
  }

  private loadClientData(id: number): void {
    this.clientService.getClientById(id).subscribe({
      next: (response) => {
        this.clientForm.patchValue(response);
      },
      error: (err) => {
        console.error('Error fetching client by ID:', err);
      }
    });
  }

  clientOperation(): void {
    if (this.clientForm.invalid) {
      this.clientForm.markAllAsTouched();
      return;
    }
    const clientData: IClient = this.clientForm.value;

    if (this.clientId === 0) {
      this.clientService.addNewClient(clientData).subscribe({
        next: () => {
          this.router.navigate(['/client']);
        },
        error: (err) => {
          console.error('Error adding new client:', err);
        }
      });
    } else {
      this.clientService.updateClient(clientData).subscribe({
        next: () => {
          this.router.navigate(['/client']);
        },
        error: (err) => {
          console.error('Error updating client:', err);
        }
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/client']);
  }

  get f() {
    return this.clientForm.controls;
  }
}

