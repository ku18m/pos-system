import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UnitsWithAPIService } from '../../../../services/units-with-api.service';
import { IUnits } from '../IUnits';

@Component({
  selector: 'app-add-unit',
  standalone: true,
  imports: [ ReactiveFormsModule, RouterLink,CommonModule],
  templateUrl: './unit-crud.component.html',
  styleUrls: ['./unit-crud.component.css']
})
export class UnitCrudComponent implements OnInit {
  unitId: string | null = null;
  unitForm!: FormGroup;

  constructor(
    private activatedRoute: ActivatedRoute,
    private unitService: UnitsWithAPIService,
    private router: Router,
    private changeDetector: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.unitId = this.activatedRoute.snapshot.params['id'];
    this.unitForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      notes: new FormControl('')
    });

    if (this.unitId) {
      this.loadUnitData(this.unitId);
    }
  }
  private loadUnitData(id: string): void {
    this.unitService.getById(id).subscribe({
      next: (response) => {
        this.unitForm.patchValue(response);
      },
      error: (err) => {
        console.error('Error fetching unit by ID:', err);
      }
    });
  }

  unitOperation(): void {
    if (this.unitForm.invalid) {
      this.unitForm.markAllAsTouched();
      return;
    }

    const unitData: IUnits = this.unitForm.value;
    if (!this.unitId) {
      this.unitService.add(unitData).subscribe({
        next: () => {
          this.router.navigate(['/unit']);
        },
        error: (err) => {
          console.error('Error adding new unit:', err);
        }
      });
    } else {
      unitData.id = this.unitId;
      this.unitService.update(unitData).subscribe({
        next: () => {
          this.showNotification('success', 'Unit updated successfully');
        },
        error: (err) => {
          console.error('Error updating unit:', err);
          this.showNotification('danger', 'Error updating unit');
        }
      });
    }
  }
  goBack(): void {
    this.router.navigate(['/units/operations']);
  }
  get f() {
    return this.unitForm.controls;
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
