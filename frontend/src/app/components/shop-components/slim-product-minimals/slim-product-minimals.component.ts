import { Component, Input, OnInit, OnChanges } from '@angular/core';
import { ProductMinimal } from 'src/app/models/product/prodact-minimal';

@Component({
  selector: 'app-slim-product-minimals',
  templateUrl: './slim-product-minimals.component.html',
  styleUrls: ['./slim-product-minimals.component.scss']
})
export class SlimProductMinimalsComponent {
  @Input() _products: ProductMinimal[];
  @Input() _title: string;

  ngOnChanges(){
    this._products.forEach(p => 
      {
        // If imageLowQualityURL is empty then set special img.
        if(!p.imageLowQualityURL){
          p.imageLowQualityURL = "assets/images/ImgNotFound.svg";
        }
      });
  }

  public getProductMinimals(): ProductMinimal[] {
    return this._products;
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
}


