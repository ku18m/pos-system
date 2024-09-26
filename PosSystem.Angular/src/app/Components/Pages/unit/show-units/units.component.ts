import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { UnitsWithAPIService } from '../../../../services/units-with-api.service';
import { IUnits } from '../IUnits';

@Component({
  selector: 'app-units',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, HttpClientModule],
  templateUrl: './units.component.html',
  styleUrls: ['./units.component.css'],
  providers: [UnitsWithAPIService]
})
export class ShowUnitComponent implements OnInit {
  units: IUnits[] = [];
  currentPage: number = 1;
  unitsPerPage: number = 3;
  paginatedUnits: IUnits[] = [];

  unitIdToDelete: string | null = null;

  constructor(public unitService: UnitsWithAPIService, private router: Router, private changeDetector: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.unitService.getAll().subscribe({
      next: (response) => {
        this.units = response as IUnits[];
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
    this.router.navigate(['/units', 'operations', unitId]);
  }

  deleteUnitHandler(unitId: string) {
    this.unitService.delete(unitId).subscribe({
      next: () => {
        this.showNotification('success', 'Unit deleted successfully');
        this.unitService.getAll().subscribe({
          next: (response) => {
            this.units = response as IUnits[];
            this.updatePaginatedUnits();
          },
        });
      },
      error: () => {
        this.showNotification('danger', 'Error deleting unit, unit is associated with a product');
      },
    });
  }

  openDeleteModal(unitId: string) {
    this.unitIdToDelete = unitId;
    const modalElement = document.getElementById('deleteModal');
    if (modalElement) {
      const modal = new (window as any).bootstrap.Modal(modalElement);
      modal.show();
    }
  }

  confirmDelete() {
    if (this.unitIdToDelete) {
      this.deleteUnitHandler(this.unitIdToDelete);
      this.unitIdToDelete = null;
    }
  }

  hideModal() {
    const modalElement = document.getElementById('deleteModal');
    if (modalElement) {
      const modal = new (window as any).bootstrap.Modal(modalElement);
      modal.hide();
    }
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
