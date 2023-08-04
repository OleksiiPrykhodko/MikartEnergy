import { Component, Input, OnInit, OnChanges } from '@angular/core';
import { keyValuePair } from 'src/app/models/common/keyValuePair';
import { ProductMinimal } from 'src/app/models/product/prodact-minimal';

@Component({
  selector: 'app-wide-product-minimals',
  templateUrl: './wide-product-minimals.component.html',
  styleUrls: ['./wide-product-minimals.component.scss']
})
export class WideProductMinimalsComponent {
  @Input() _products: keyValuePair<ProductMinimal, number>[];
  @Input() _unknownProducts: keyValuePair<string, number>[];
  @Input() _title: string;

  ngOnChanges(){
    this._products.forEach(pair => 
      {
        // If imageLowQualityURL is empty then set special img.
        if(!pair.key.imageLowQualityURL){
          pair.key.imageLowQualityURL = "assets/images/ImgNotFound.svg";
          pair.value
        }
      });
  }

  public getProductMinimals(): keyValuePair<ProductMinimal, number>[] {
    return this._products;
  }

  public getUnknownProduct(): keyValuePair<string, number>[]{
    return this._unknownProducts;
  }

  public checkLoaded(): boolean{
    return this._products.length > 0;
  }

  public setLinkToProductPage(productId: string | null | undefined): string{
    if(productId){
      return `shop/products/${productId}`;
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
