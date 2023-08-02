import { KeyValue } from "@angular/common";
import { ProductMinimal } from "../product/prodact-minimal";
import { keyValuePair } from "../common/keyValuePair";

export interface TiaStProductsOrder{
    id: string;
    existingInDbProducts: keyValuePair<ProductMinimal, number>[];
    notExistingInDbProducts: keyValuePair<string, number>[];
}