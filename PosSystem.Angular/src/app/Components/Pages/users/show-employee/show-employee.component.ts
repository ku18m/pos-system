import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {  Router, RouterLink, RouterLinkActive } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { UsersWithAPIService } from '../../../../services/users-with-api.service';
import { IUser } from '../IUser';

@Component({
  selector: 'app-show-employee',
  standalone: true,
  imports: [CommonModule,FormsModule,RouterLink,HttpClientModule,RouterLinkActive],
  templateUrl: './show-employee.component.html',
  styleUrl: './show-employee.component.css',
  providers: [UsersWithAPIService]
})
export class ShowEmployeeComponent implements OnInit {
  employees:IUser[]=[];
  currentPage: number = 1;
  employeesPerPage: number = 3;
  paginatedEmployees: IUser[] = [];
  constructor(public employeeServices: UsersWithAPIService, private router: Router) {}
    ngOnInit(): void {
    this.employeeServices.getAllUsers().subscribe({
      next: (response) => {
        this.employees = response as IUser[];
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
    this.router.navigate(['/users', 'edit', employeeId]);
  }

  deleteEmployeeHandler(employeeId: any) {
    this.employeeServices.deleteUser(employeeId).subscribe({
      next: () => {
        this.employeeServices.getAllUsers().subscribe({
          next: (response) => {
            this.employees = response as IUser[];
            this.updatePaginatedProducts();
          },
        });
      },
    });
  }
}
