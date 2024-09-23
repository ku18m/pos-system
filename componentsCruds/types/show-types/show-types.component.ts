import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { TypeService } from '../../services/type.service';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { itype } from '../../models/itype';



@Component({
  selector: 'app-show-types',
  standalone: true,
  imports: [CommonModule,RouterLink],
  templateUrl: './show-types.component.html',
  styleUrl: './show-types.component.css'
})
export class ShowTypesComponent {
   types: itype[] = [];
  currentPage: number = 1;
  typesPerPage: number = 3;
  paginatedTypes: itype[] = [];

  constructor(public typeService: TypeService, private router: Router) {}

  ngOnInit(): void {
    this.typeService.getAllTypes().subscribe({
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
    this.router.navigate(['/type', typeId, 'edit']); 
  }

  deleteTypeHandler(typeId: any) {
    this.typeService.deleteType(typeId).subscribe({
      next: () => {
        this.typeService.getAllTypes().subscribe({
          next: (response) => {
            this.types = response as itype[];
            this.updatePaginatedTypes();
          },
        });
      },
    });
  }

}
