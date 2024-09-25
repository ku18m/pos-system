import { HttpClientModule } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import {
  ReactiveFormsModule,
  FormGroup,
  FormControl,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IClients } from '../IClients';
import { ClientsWithAPIService } from '../../../../services/clients-with-api.service';

@Component({
  selector: 'app-add-client',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, HttpClientModule],
  templateUrl: './client-Cruds.component.html',
  styleUrls: ['./client-Cruds.component.css'],
  providers: [],
})
export class ClientCrudsComponent implements OnInit {
  clientId: string = "";
  clientForm!: FormGroup;

  constructor(
    private activatedRoute: ActivatedRoute,
    private clientService: ClientsWithAPIService,
    private router: Router,
    private changeDetector: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.clientId = this.activatedRoute.snapshot.params['id'];
    this.clientForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      number: new FormControl(''),
      address: new FormControl(''),
      phoneNumber: new FormControl('', [
        Validators.required,
        Validators.pattern(/^.{14}$/),
      ]),
    });

    this.clientForm.get('number')?.disable();

    if (this.clientId && this.clientId !== null) {
      this.loadClientData(this.clientId);
    }
  }

  private loadClientData(id: string): void {
    this.clientService.getClientById(id).subscribe({
      next: (response) => {
        this.clientForm.patchValue(response);
      },
      error: (err) => {
        console.error('Error fetching client by ID:', err);
      },
    });
  }

  clientOperation(): void {
    console.log('Client form:', this.clientForm.value);
    if (this.clientForm.invalid) {
      this.clientForm.markAllAsTouched();
      return;
    }
    const clientData: IClients = this.clientForm.value;

    clientData.id = this.clientId;
    clientData.number = this.clientForm.get('number')?.value;

    if (this.clientId === null) {
      this.clientService.addNewClient(clientData).subscribe({
        next: () => {
          this.router.navigate(['/clients/operations']);
        },
        error: (err) => {
          console.error('Error adding new client:', err);
        },
      });
    } else {
      this.clientService.updateClient(clientData).subscribe({
        next: () => {
          this.router.navigate(['/clients/operations']);
        },
        error: (err) => {
          console.error('Error updating client:', err.error.errors.Name[0]);
          this.showNotification('danger', err.error.errors.Name[0]);
        },
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/clients/operations']);
  }

  get f() {
    return this.clientForm.controls;
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
