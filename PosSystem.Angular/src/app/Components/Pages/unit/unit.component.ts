import { Component, OnInit } from '@angular/core';
import { UnitsWithAPIService } from '../../../services/units-with-api.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { IUnits } from './IUnits';
import { NavbarComponent } from '../navbar/navbar.component';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { FooterComponent } from '../footer/footer.component';


@Component({
  selector: 'app-unit',
  standalone: true,
  imports: [ReactiveFormsModule,FormsModule,NavbarComponent,SidebarComponent,FooterComponent],
  templateUrl: './unit.component.html',
  styleUrl: './unit.component.css'
})
export class UnitComponent implements OnInit {
  notes:any;
  unitName:any;
  requiredError:any;
  duplicateError:any;
  unitList:string[]=[];
  unitForm=new FormGroup({
    name:new FormControl("",[Validators.required]),
    notes:new FormControl("")
  })

  constructor(private unitService:UnitsWithAPIService){}

  ngOnInit(): void {
    this.unitService.getAllUnits().subscribe({next:(units:IUnits[])=>{  
      this.unitList = units.map(unit => unit.id
      );  
    },
    error:(error) => {  
      console.error('Error fetching companies:', error);  
    },  
  });
    
  }

  Submit(e:any){
    e.preventDefault();
    console.log(this.unitForm.status);
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
    //adding company
    if(this.unitName!=null && this.unitName!="" && !this.unitList.includes(this.unitName)){

    
    const newUnit:any = { name: this.unitName, notes:this.notes };  

    this.unitService.addUnit(newUnit).subscribe({  
      next: () => {  
        console.log('Unit added successfully!');  
        this.unitList.push(newUnit);  
        this.unitForm.reset();   
      },  
      error: (error) => {  
        console.error('Error adding unit:', error);  
      }  
    });
  }  
  }  

  Cancel(){
    this.unitForm.reset();
    this.requiredError=null;
    this.duplicateError=null; 
  }
}
