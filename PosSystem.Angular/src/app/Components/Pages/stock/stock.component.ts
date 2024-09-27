import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ItemWithAPIService } from '../../../services/item-with-api.service';
import { NavbarComponent } from '../navbar/navbar.component';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { FooterComponent } from '../footer/footer.component';
import { StockWithAPIService } from '../../../services/stock-with-api.service';

@Component({
  selector: 'app-stock',
  standalone: true,
  imports: [FormsModule, NavbarComponent, SidebarComponent, FooterComponent],
  templateUrl: './stock.component.html',
  styleUrl: './stock.component.css',
})
export class StockComponent implements OnInit {
  items: any = '-- Choose From Items --';
  itemList: any[] = [];
  code: any;
  name: any;
  quantity: any;
  itemTable: { code: string; name: string; quantity: number }[] = [];
  Error: any;

  constructor(
    private itemService: ItemWithAPIService,
    private stockService: StockWithAPIService
  ) {}
  ngOnInit(): void {
    this.stockService.GetAllItems().subscribe({
      next: (response) => {
        response.data.forEach((element: any) => {
          this.itemList.push(element);
        });
      },
    });
  }

  Search() {
    this.itemTable = [];
    this.itemList.forEach((item) => {
      if (this.items == item.name) {
        this.code = item.id;
        this.name = item.name;
        this.quantity = item.quantity;
        this.itemTable.push({
          code: this.code,
          name: this.name,
          quantity: this.quantity,
        });
      } else {
        this.Error = 'Wrong Item Name!';
        // <div class="text-danger">{{Error}}</div>
      }
    });

    if (this.items == 'All Products') {
      this.itemList.forEach((item) => {
        this.code = item.id;
        this.name = item.name;
        this.quantity = item.quantity;
        this.itemTable.push({
          code: this.code,
          name: this.name,
          quantity: this.quantity,
        });
      });
    }
  }
}
