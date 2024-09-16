import { IInvoiceItem } from "./IInvoiceItem";

export interface IInvoicesBack{
    id:string,
    billsDate:string,
    billsNumber:number,
    clienId:string,
    clientName:string,
    itemId:string,
    itemName:string,
    // sellingPrice:number,
    unitId:string,
    unit:string,
    quantity:number,
    total:number,
    billsTotal:number,
    percentageDiscount:number,
    valueDiscount:number,
    theNet:number,
    paidUp:number,
    theRest:number,
    employeeId:string,
    employeeName:string,
    date:string,
    startTime:string,
    endTime:string,
    invoiceItem:IInvoiceItem[]
    

}