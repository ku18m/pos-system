export interface IInvoices{
    id:string,
    billsDate:string,
    billsNumber:number,
    clientName:string,
    itemName:string,
    sellingPrice:number,
    unit:string,
    quantity:number,
    discount:number,
    total:number,
    balance:number,
    billsTotal:number,
    percentageDiscount:number,
    valueDiscount:number,
    theNet:number,
    paidUp:number,
    theRest:number,
    employeeName:string,
    date:string,
    startTime:string,
    endTime:string,
    invoiceItem:string[]
    

}