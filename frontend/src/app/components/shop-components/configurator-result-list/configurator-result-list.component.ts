import { Component, Input, OnInit, OnChanges } from '@angular/core';
import { keyValuePair } from 'src/app/models/common/keyValuePair';
import { ProductMinimal } from 'src/app/models/product/prodact-minimal';

@Component({
  selector: 'app-configurator-result-list',
  templateUrl: './configurator-result-list.component.html',
  styleUrl: './configurator-result-list.component.scss'
})
export class ConfiguratorResultListComponent {
  @Input() _products: keyValuePair<ProductMinimal, number>[];
  @Input() _unknownProducts: keyValuePair<keyValuePair<string, string>, number>[];
  @Input() _title: string;

  ngOnChanges(){
    this._products.forEach(pair => 
      {
        // If imageLowQualityURL is empty then set special img.
        if(!pair.key.imageLowQualityURL){
          pair.key.imageLowQualityURL = "assets/images/ImgNotFound.svg";
        }
      });
  }

  public getProductMinimals(): keyValuePair<ProductMinimal, number>[] {
    return this._products;
  }

  public getUnknownProduct(): keyValuePair<keyValuePair<string, string>, number>[]{
    return this._unknownProducts;
  }

  public checkLoaded(): boolean{
    return this._products.length > 0 || this._unknownProducts.length > 0;
  }

  public setLinkToProductPage(productSupplierPID: string | null | undefined): string{
    if(productSupplierPID){
      return `shop/products/${productSupplierPID}`;
    }
    return "404";
  }

  public calculatePrice(price: number, quantity: number): number{
    return price * quantity;
  }

  public getUnknownProductImg(): string{
    return "assets/images/x-circle.svg";
  }

}
