import { Injectable } from '@angular/core';
import { HttpInternalService } from '../http-internal.service';
import { Product } from 'src/app/models/product/product';
import { ProductMinimal } from 'src/app/models/product/prodact-minimal';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  public routePrefix = '/api/products';
  public routeMinimalPrefix = '/api/products/minimal';

  constructor(private httpService: HttpInternalService) { }

  public getProductById(productID: string) {
    return this.httpService.getFullRequest<Product>(`${this.routePrefix}/${productID}`);
  }

  public getProductMinimalById(productID: string) {
    return this.httpService.getFullRequest<ProductMinimal>(`${this.routeMinimalPrefix}/${productID}`);
  }

}


