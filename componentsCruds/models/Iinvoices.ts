import { invoiceItems } from './IinvoiceItems';
export interface Iinvoice {
    id:string,
    creationDate:Date,
    totalPrice:number,
    clientName:string,
    invoiceItems:invoiceItems[]
}