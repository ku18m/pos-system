export interface IInvoiceItem{
  invoiceItemId?: string;
  itemName?: string;
  itemId: string;
  unitName?: string;
  unitId: string;
  quantity: number;
  total?: number;
  sellingPrice: number;
}