import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/employee.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import {  ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IEmployee } from '../../models/iemployee';

@Component({
  selector: 'app-add-employee',
  standalone: true,
  imports: [ CommonModule,ReactiveFormsModule, RouterLink],
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent implements OnInit {
  employeeId: number | null = null;
  employeeForm!: FormGroup;  

  constructor(
    private activatedRoute: ActivatedRoute,
    private employeeService: EmployeeService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.employeeId = +this.activatedRoute.snapshot.params['id'];
    this.employeeForm = new FormGroup({
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      address: new FormControl(''),
      birthDate: new FormControl(''),
      phone: new FormControl('', [
        Validators.required,
        Validators.pattern(/^(010|012|011|015)\d{8}$/)  
      ])
    });

    if (this.employeeId && this.employeeId !== 0) {
      this.loadEmployeeData(this.employeeId);
    }
  }

  private loadEmployeeData(id: number): void {
    this.employeeService.getEmployeeById(id).subscribe({
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
    const employeeData: IEmployee = this.employeeForm.value; 

    if (this.employeeId === 0) {
      this.employeeService.addNewEmployee(employeeData).subscribe({
        next: () => {
          this.router.navigate(['/employee']); 
        },
        error: (err) => {
          console.error('Error adding new employee:', err);
        }
      });
    } else {
    
      this.employeeService.updateEmployee(employeeData).subscribe({
        next: () => {
          this.router.navigate(['/employee']); 
        },
        error: (err) => {
          console.error('Error updating employee:', err);
        }
      });
    }
  }


  goBack(): void {
    this.router.navigate(['/employee']);
  }


  get f() {
    return this.employeeForm.controls;
  }
}
