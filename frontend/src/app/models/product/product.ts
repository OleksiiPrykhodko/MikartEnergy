import { keyValuePair } from "../common/keyValuePair";

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

    relatedProductIDs: string[];

    minimalOrderQuantity: number;
    maximalOrderQuantity: number;
    orderQuantityMultiplier: number;
    inStock: boolean;
    price: number;
    priceCurrency: string;
}