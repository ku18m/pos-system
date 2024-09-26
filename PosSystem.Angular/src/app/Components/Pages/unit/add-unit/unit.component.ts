import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { UnitsWithAPIService } from '../../../../services/units-with-api.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { IUnits } from '../IUnits';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-unit',
  standalone: true,
  imports: [ReactiveFormsModule,FormsModule, CommonModule],
  templateUrl: './unit.component.html',
  styleUrl: './unit.component.css'
})
export class UnitComponent implements OnInit {
  notes:any;
  unitName:any;
  requiredError:any;
  duplicateError:any;
  unitList:string[]=[];
  tokin:any=localStorage.getItem('token');
  unitForm=new FormGroup({
    name:new FormControl("",[Validators.required]),
    notes:new FormControl("")
  })

  constructor(private unitService:UnitsWithAPIService, private changeDetector: ChangeDetectorRef){}

  ngOnInit(): void {
    this.unitService.getAll().subscribe({next:(units:IUnits[])=>{
      this.unitList = units.map(unit => unit.name);
    },
    error:(error) => {
      console.error('Error fetching units:', error);
    },
  });

  }

  Submit(e:any){
    e.preventDefault();
    console.log(this.unitName);
    if (this.unitName==null || this.unitName=="") {
      this.requiredError = 'Unit Name Is Required';
      this.duplicateError=null;

      return;
    }
    else
        this.requiredError = null;
    if (this.unitList.includes(this.unitName)) {
      this.duplicateError = `${this.unitName} has already existed before`;

      return;
    }
    else
        this.duplicateError=null;
    if(this.unitName!=null && this.unitName!="" && !this.unitList.includes(this.unitName)){


    const newUnit:any = { name: this.unitName, notes:this.notes };

    this.unitService.add(newUnit).subscribe({
      next: () => {
        console.log('Unit added successfully!');
        this.showNotification('success', 'Unit added successfully!');
        this.ngOnInit();
        this.unitForm.reset();
      },
      error: (error) => {
        console.error('Error adding unit:', error);
        this.showNotification('error', 'Error adding unit!');
      }
    });
  }
  }

  Cancel(){
    this.unitForm.reset();
    this.requiredError=null;
    this.duplicateError=null;
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
