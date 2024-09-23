import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/employee.service';
import {  Router, RouterLink, RouterLinkActive } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { IEmployee } from '../../models/iemployee';

@Component({
  selector: 'app-show-employee',
  standalone: true,
  imports: [CommonModule,FormsModule,RouterLink,HttpClientModule,RouterLinkActive],
  templateUrl: './show-employee.component.html',
  styleUrl: './show-employee.component.css',
   providers: [EmployeeService]
})
export class ShowEmployeeComponent implements OnInit {
  employees:IEmployee[]=[];
  currentPage: number = 1;
  employeesPerPage: number = 3;
  paginatedEmployees: IEmployee[] = [];
  constructor(public employeeServices: EmployeeService,private router: Router) {}
    ngOnInit(): void {
    this.employeeServices.getAllEmployee().subscribe({
      next: (response) => {
        this.employees = response as IEmployee[];
        console.log(this.employees)
        this.updatePaginatedProducts();
      },
    });
  }
  updatePaginatedProducts(): void {
    const startIndex = (this.currentPage - 1) * this.employeesPerPage;
    const endIndex = startIndex + this.employeesPerPage;
    this.paginatedEmployees = this.employees.slice(startIndex, endIndex);
  }
  setPage(page: number): void {
    if (page > 0 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePaginatedProducts();
    }
  }
    get totalPages(): number {
    return Math.ceil(this.employees.length / this.employeesPerPage);
  }
  editEmployee(employeeId: string) {
    this.router.navigate(['/employee', employeeId, 'edit']);
  }

  deleteEmployeeHandler(employeeId: any) {
    this.employeeServices.deleteEmployee(employeeId).subscribe({
      next: () => {
        this.employeeServices.getAllEmployee().subscribe({
          next: (response) => {
            this.employees = response as IEmployee[];
            this.updatePaginatedProducts();
          },
        });
      },
    });
  }
}
