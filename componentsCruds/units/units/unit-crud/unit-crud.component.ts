import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { IUnit } from '../../../models/iunit';
import { UnitServices } from '../../../services/unit-services.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-unit',
  standalone: true,
  imports: [ ReactiveFormsModule, RouterLink,CommonModule],
  templateUrl: './unit-crud.component.html',
  styleUrls: ['./unit-crud.component.css']
})
export class UnitCrudComponent implements OnInit {
  unitId: any | null = null;
  unitForm!: FormGroup;  

  constructor(
    private activatedRoute: ActivatedRoute,
    private unitService: UnitServices,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.unitId = +this.activatedRoute.snapshot.params['id'];
    this.unitForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      notes: new FormControl('') 
    });

    if (this.unitId && this.unitId !== 0) {
      this.loadUnitData(this.unitId);
    }
  }
  private loadUnitData(id: string): void {
    this.unitService.getUnitById(id).subscribe({
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

    const unitData: IUnit = this.unitForm.value;
    if (this.unitId === 0) {
      this.unitService.addNewUnit(unitData).subscribe({
        next: () => {
          this.router.navigate(['/unit']);  
        },
        error: (err) => {
          console.error('Error adding new unit:', err);
        }
      });
    } else {
     
      this.unitService.updateUnit(unitData).subscribe({
        next: () => {
          this.router.navigate(['/unit']); 
        },
        error: (err) => {
          console.error('Error updating unit:', err);
        }
      });
    }
  }
  goBack(): void {
    this.router.navigate(['/unit']);
  }
  get f() {
    return this.unitForm.controls;
  }
}
