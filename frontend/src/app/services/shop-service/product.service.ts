import { Injectable } from '@angular/core';
import { HttpInternalService } from '../http-internal.service';
import { Product } from 'src/app/models/product/product';
import { ProductMinimal } from 'src/app/models/product/prodact-minimal';
import { RequestResult } from 'src/app/models/common/request-result';
import { Observable } from 'rxjs';
import { HttpResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  public routePrefix = '/api/products';
  public routeForGetingProductBySupplierPID = `${this.routePrefix}/productBySupplierPID`;
  public routeForGettingProductMinimalBySupplierPID = `${this.routePrefix}/productMinimalBySupplierPID`;

  constructor(private httpService: HttpInternalService) { }

  public getProductBySupplierPID(productID: string): Observable<HttpResponse<RequestResult<Product>>> {
    return this.httpService.getFullRequest<RequestResult<Product>>(`${this.routeForGetingProductBySupplierPID}/${productID}`);
  }

  public getProductMinimalBySupplierPID(productID: string): Observable<HttpResponse<RequestResult<ProductMinimal>>> {
    return this.httpService.getFullRequest<RequestResult<ProductMinimal>>(`${this.routeForGettingProductMinimalBySupplierPID}/${productID}`);
  }

}


