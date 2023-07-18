import { Component, Input } from '@angular/core';
import { ProductMinimal } from 'src/app/models/product/prodact-minimal';

@Component({
  selector: 'app-product-minimal-slim',
  templateUrl: './product-minimal-slim.component.html',
  styleUrls: ['./product-minimal-slim.component.scss']
})
export class ProductMinimalSlimComponent {
  @Input() _product: ProductMinimal;



  public getLinkToProductPage(): string {
    return "";
  }
  public getProductName(): string{
    return this._product.productName;
  }
  public getImageLowQualityURL(): string {
    if (this._product.imageLowQualityURL) {
      return this._product.imageLowQualityURL;
    }
    return "assets/images/ImgNotFound.svg";
  }
  public getOrderNumber(): string{
    return this._product.orderNumber;
  }
  public getPriceAmount(): number {
    return this._product.price;
  }
  public getPriceCurrency(): string {
    return this._product.priceCurrency;
  }

}
