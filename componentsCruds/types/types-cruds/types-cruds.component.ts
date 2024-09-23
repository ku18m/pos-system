import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { itype } from '../../models/itype';import { TypeService } from '../../services/type.service';
 

@Component({
  selector: 'app-types-cruds',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './types-cruds.component.html',
  styleUrl: './types-cruds.component.css'
})
export class TypesCrudsComponent implements OnInit {
  typeForm: FormGroup;
  typeId: string = ""; 
  types: itype[] = []; 

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private typesService: TypeService 
  ) {
    
    this.typeForm = this.fb.group({
      typeId: [''], 
      name: ['', Validators.required], 
      companyName: [''], 
      companyId: [''], 
      notes: [''],
    });
  }

  ngOnInit(): void {
    this.typeId = this.route.snapshot.paramMap.get('id')!;

    if (this.typeId) {
      this.fetchTypeDetails(this.typeId);
    }

  
    this.typeForm.get('companyName')?.disable();
  }
  fetchTypeDetails(typeId: string): void {
    this.typesService.getTypeById(typeId).subscribe((type: itype) => {
      this.typeForm.patchValue({
        typeId: typeId,
        name: type.name,
        companyName: type.companyName || '',
        companyId: type.companyId,
        notes: type.notes,
      });
    });
  }
  typeOperation(): void {
    if (this.typeForm.valid) {
          const payload = {
      ...this.typeForm.value,
      companyName: this.typeForm.get('companyName')?.value || '', }
      console.log(payload)
      if (this.typeId) {
        this.typesService.updateType(this.typeId, payload).subscribe(() => {
           
          this.router.navigate(['/types']); 
        });
      } else {
        this.typesService.addType(payload).subscribe(() => {
          this.router.navigate(['/types']); 
        });
      }
    }
  }

  goBack(): void {
    this.router.navigate(['/types']); 
  }
}
