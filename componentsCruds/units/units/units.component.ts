import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { IUnit } from '../../models/iunit';
import { UnitServices } from '../../services/unit-services.service';

@Component({
  selector: 'app-units', 
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, HttpClientModule],
  templateUrl: './units.component.html', 
  styleUrls: ['./units.component.css'], 
  providers: [UnitServices] 
})
export class ShowUnitComponent implements OnInit {
  units: IUnit[] = []; 
  currentPage: number = 1;
  unitsPerPage: number = 3; 
  paginatedUnits: IUnit[] = []; 

  constructor(public unitService: UnitServices, private router: Router) {} 

  ngOnInit(): void {
    this.unitService.getAllUnits().subscribe({ 
      next: (response) => {
        this.units = response as IUnit[];
        console.log(this.units);
        this.updatePaginatedUnits(); 
      },
    });
  }

  updatePaginatedUnits(): void { 
    const startIndex = (this.currentPage - 1) * this.unitsPerPage;
    const endIndex = startIndex + this.unitsPerPage;
    this.paginatedUnits = this.units.slice(startIndex, endIndex);
  }

  setPage(page: number): void {
    if (page > 0 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePaginatedUnits(); 
    }
  }

  get totalPages(): number {
    return Math.ceil(this.units.length / this.unitsPerPage); 
  }

  editUnit(unitId: string) { 
    this.router.navigate(['/unit', unitId, 'edit']); 
  }

  deleteUnitHandler(unitId: string) { 
    this.unitService.deleteUnit(unitId).subscribe({ 
      next: () => {
        this.unitService.getAllUnits().subscribe({ 
          next: (response) => {
            this.units = response as IUnit[];
            this.updatePaginatedUnits(); 
          },
        });
      },
    });
  }
}
