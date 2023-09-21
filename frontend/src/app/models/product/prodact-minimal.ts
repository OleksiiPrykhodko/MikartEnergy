export interface ProductMinimal{
    supplierPID: string;
    manufacturerName: string;
    orderNumber: string;
    productName: string;
    shortDescription: string;
    longDescription: string;

    imageLowQualityURL: string;

    inStock: boolean;
    price: number;
    priceCurrency: string;
}