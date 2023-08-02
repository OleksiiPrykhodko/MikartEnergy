import { ProductMinimal } from "../product/prodact-minimal";

export interface TiaStProductsOrder{
    id: string;
    existingInDbProducts: ProductMinimal[];
    notExistingInDbProducts: string[];
}