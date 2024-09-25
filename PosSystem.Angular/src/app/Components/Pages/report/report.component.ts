import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { InvoicesWithAPIService } from '../../../services/invoices-with-api.service';
import { ItemWithAPIService } from '../../../services/item-with-api.service';
import { NavbarComponent } from '../navbar/navbar.component';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { FooterComponent } from '../footer/footer.component';
import { ReportWithApiService } from '../../../services/report-with-api.service';

@Component({
  selector: 'app-report',
  standalone: true,
  imports: [FormsModule, NavbarComponent, SidebarComponent, FooterComponent],
  templateUrl: './report.component.html',
  styleUrl: './report.component.css',
})
export class ReportComponent implements OnInit {
  tokin: any = localStorage.getItem('token');
  reportData: any;
  fromPeriod: any;
  toPeriod: any;

  Invoices: {
    id: string;
    number: number;
    billDate: string;
    date: string;
    paidUp: number;
    net: number;
    discountValue: number;
    totalAmount: number;
    clientName: string;
    employeeName: string;
  }[] = [];

  billDateList: {
    id: string;
    billsDate: string;
    clientName: string;
    itemName: string;
    sellingPrice: number;
    unit: string;
    quantity: number;
    discount: number;
    total: number;
  }[] = [];
  itemList: any[] = [];
  code: any;
  name: any;
  buyingPrice: any;
  sellingPrice: any;

  ReportTableItem: {
    code: number;
    name: string;
    buyingPrice: string;
    sellingPrice: string;
    profits: number;
  }[] = [];

  constructor(
    private invoiceService: InvoicesWithAPIService,
    private itemService: ItemWithAPIService,
    private reportService: ReportWithApiService
  ) {}

  getSalesReport() {
    const d1 = new Date(this.fromPeriod);
    const d2 = new Date(this.toPeriod);
    this.reportService.getPeriodReport(d1, d2, this.tokin).subscribe({
      next: (data: any) => {
        this.reportData = data;
        this.Invoices = data.invoices;
        console.log('Report data:', this.Invoices);
      },
    });
  }

  ngOnInit(): void {
    this.invoiceService.getAllBills().subscribe({
      next: (bills: any) => {
        this.billDateList = bills;
      },
    });
    this.invoiceService.getAllBills().subscribe({
      next: (bills: any) => {
        this.billDateList = bills;
      },
    });
    this.itemService
      .getAllItems()
      .subscribe({ next: (response) => (this.itemList = response) });
  }

  Submit(e: any) { 
    e.preventDefault();

    this.getSalesReport();

    const d1 = new Date(this.fromPeriod);
    const d2 = new Date(this.toPeriod);

    this.billDateList.forEach((bill) => {
      if (new Date(bill.billsDate) >= d1 && new Date(bill.billsDate) <= d2) {
        console.log(bill.itemName);
        this.itemList.forEach((item) => {
          if (bill.itemName == item.name) {
            this.code = item.id;
            this.name = item.name;
            this.buyingPrice = item.buyingPrice;
            this.sellingPrice = item.sellingPrice;
            this.ReportTableItem.push({
              code: this.code,
              name: this.name,
              buyingPrice: this.buyingPrice,
              sellingPrice: this.sellingPrice,
              profits: this.sellingPrice - this.buyingPrice,
            });
          }
        });
      }
    });
  }

  Cancel() {
    this.fromPeriod = null;
    this.toPeriod = null;
  }
}
