import { Component, Input, OnInit, OnChanges } from '@angular/core';
import { keyValuePair } from 'src/app/models/common/keyValuePair';
import { ProductMinimal } from 'src/app/models/product/prodact-minimal';

@Component({
  selector: 'app-wide-product-minimals',
  templateUrl: './wide-product-minimals.component.html',
  styleUrls: ['./wide-product-minimals.component.scss']
})
export class WideProductMinimalsComponent {
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
  
    public setLinkToProductPage(productSupplierPID: string | null | undefined): string{
      if(productSupplierPID){
        return `shop/products/${productSupplierPID}`;
      }
      return "404";
    }
}
