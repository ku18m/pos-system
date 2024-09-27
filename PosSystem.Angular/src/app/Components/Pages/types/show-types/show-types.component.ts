import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { TypesWithAPIService } from '../../../../services/types-with-api.service';
import { ITypes } from '../ITypes';



@Component({
  selector: 'app-show-types',
  standalone: true,
  imports: [CommonModule,RouterLink],
  templateUrl: './show-types.component.html',
  styleUrl: './show-types.component.css'
})
export class ShowTypesComponent {
  types: ITypes[] = [];
  currentPage: number = 1;
  typesPerPage: number = 3;
  paginatedTypes: ITypes[] = [];
  typeIdToDelete: string | null = null;

  constructor(public typeService: TypesWithAPIService, private router: Router, private changeDetector: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.typeService.getTypes().subscribe({
      next: (response) => {
        this.types = response;
        this.updatePaginatedTypes();
      },
    });
  }

  updatePaginatedTypes(): void {
    const startIndex = (this.currentPage - 1) * this.typesPerPage;
    const endIndex = startIndex + this.typesPerPage;
    this.paginatedTypes = this.types.slice(startIndex, endIndex);
  }

  setPage(page: number): void {
    if (page > 0 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePaginatedTypes();
    }
  }

  get totalPages(): number {
    return Math.ceil(this.types.length / this.typesPerPage);
  }

  editType(typeId: string) {
    this.router.navigate(['/types', 'operations', typeId]);
  }

  deleteTypeHandler(typeId: any) {
    this.typeService.deleteType(typeId).subscribe({
      next: () => {
        this.showNotification('success', 'Type deleted successfully');
        this.typeService.getTypes().subscribe({
          next: (response) => {
            this.types = response as ITypes[];
            this.updatePaginatedTypes();
          },
        });
      },
      error: (err) => {
        this.showNotification('danger', 'Error deleting type, type is associated with some items');
        console.error('Error deleting type:', err);
      },
    });
  }


  openDeleteModal(typeId: string) {
    this.typeIdToDelete = typeId;
    const modalElement = document.getElementById('deleteModal');
    if (modalElement) {
      const modal = new (window as any).bootstrap.Modal(modalElement);
      modal.show();
    }
  }

  confirmDelete() {
    if (this.typeIdToDelete) {
      this.deleteTypeHandler(this.typeIdToDelete);
      this.typeIdToDelete = null;
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
