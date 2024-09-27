import { IInvoiceItem } from "./IInvoiceItem";

export interface IInvoices {
  id: string;
  number: number;
  billDate: string;
  date: string;
  totalAmount: number;
  totalDiscount: number;
  net: number;
  paidUp: number;
  remaining: number;
  invoiceItems: IInvoiceItem[];
  clientName?: string;
  clientId: string;
  employeeName?: string;
  employeeId: string;
}