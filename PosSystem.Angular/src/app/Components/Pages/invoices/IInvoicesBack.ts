import { IInvoiceItemBack } from "./IInvoiceItemBack";

export interface IInvoicesBack{
    id?: string;
    date: string;
    billDate: string;
    paidUp: number;
    totalDiscount: number;
    totalAmount: number;
    invoiceItems: IInvoiceItemBack[];
    clientId: string;
    employeeId: string;
}