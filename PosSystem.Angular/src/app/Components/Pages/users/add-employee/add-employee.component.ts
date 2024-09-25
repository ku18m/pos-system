import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UsersWithAPIService } from '../../../../services/users-with-api.service';
import { IUserOperations } from '../IUserOperations';

@Component({
  selector: 'app-add-employee',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent implements OnInit {
  employeeId: string | null = null;
  employeeForm!: FormGroup;

  constructor(
    private activatedRoute: ActivatedRoute,
    private employeeService: UsersWithAPIService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.employeeId = this.activatedRoute.snapshot.params['id'];

    if(!this.employeeId) {
      this.employeeForm = new FormGroup({
        firstName: new FormControl('', [Validators.required]),
        lastName: new FormControl('', [Validators.required]),
        userName: new FormControl('', [Validators.required]),
        password: new FormControl(null, [Validators.required]),
        role: new FormControl('Employee', [Validators.required]), // Default to "Employee"
        startTime: new FormControl('', [Validators.required]),
        endTime: new FormControl('', [Validators.required])
      });
    }
    else {
      this.employeeForm = new FormGroup({
        firstName: new FormControl('', [Validators.required]),
        lastName: new FormControl('', [Validators.required]),
        userName: new FormControl('', [Validators.required]),
        password: new FormControl('', []),
        role: new FormControl('Employee', [Validators.required]), // Default to "Employee"
        startTime: new FormControl('', [Validators.required]),
        endTime: new FormControl('', [Validators.required])
      });

      this.loadEmployeeData(this.employeeId);
    }
  }

  private loadEmployeeData(id: string): void {
    this.employeeService.getUserById(id).subscribe({
      next: (response) => {
        this.employeeForm.patchValue(response);
      },
      error: (err) => {
        console.error('Error fetching employee by ID:', err);
      }
    });
  }

  employeeOperation(): void {
    if (this.employeeForm.invalid) {
      this.employeeForm.markAllAsTouched();
      return;
    }
    const employeeData: IUserOperations = this.employeeForm.value;

    // Convert the time to a valid time span
    employeeData.startTime = this.convertToTimeSpan(employeeData.startTime);
    employeeData.endTime = this.convertToTimeSpan(employeeData.endTime);

    if (!this.employeeId) {
      this.employeeService.addNewUser(employeeData).subscribe({
        next: () => {
          this.router.navigate(['/users']);
        },
        error: (err) => {
          console.error('Error adding new employee:', err);
        }
      });
    } else {
      this.employeeService.updateUser({ ...employeeData, id: this.employeeId }).subscribe({
        next: () => {
          this.router.navigate(['/users']);
        },
        error: (err) => {
          console.error('Error updating employee:', err);
        }
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/users']);
  }

  convertToTimeSpan(time: string): string {
    return time.length === 5 ? time + ":00" : time;
  }
}
