import { keyValuePair } from "../common/keyValuePair";
import { ProductMinimal } from "./prodact-minimal";

export interface Product {
    id: string;
    manufacturerName: string;
    orderNumber: string;
    productName: string;
    shortDescription: string;
    longDescription: string;
    technicalData: keyValuePair<string, string[]>[];

    imageLowQualityURL: string;
    imageHighQualityURL: string;
    pdfWith3dURL: string;

    linkToProductPage: string;
    linkToManuals: string;
    linkToFAQ: string;
    linkToTechnicalData: string;
    linkToApplicationExample: string;
    linkToVideo: string;

    relatedProductIDs: ProductMinimal[];

    minimalOrderQuantity: number;
    maximalOrderQuantity: number;
    orderQuantityMultiplier: number;
    inStock: boolean;
    price: number;
    priceCurrency: string;
}