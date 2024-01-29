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

  private routePrefix: string = '/api/products';
  private routeToGetProductBySupplierPID: string = `${this.routePrefix}/productBySupplierPID`;
  private routeToGetProductMinimalBySupplierPID: string = `${this.routePrefix}/productMinimalBySupplierPID`;
  private routeToGetOrderNumbersByFirstChars: string = `${this.routePrefix}/orderNumbersByFirstChars`;
  private routeToGetProductMinamalsByPartOfOrderNumber: string = `${this.routePrefix}/productMinamalsByPartOfProductOrderNumber`;

  constructor(private httpService: HttpInternalService) { }

  public getProductBySupplierPID(supplierPID: string): Observable<HttpResponse<RequestResult<Product>>> {
    return this.httpService.getFullRequest<RequestResult<Product>>(`${this.routeToGetProductBySupplierPID}/${supplierPID}`);
  }

  public getProductMinimalBySupplierPID(supplierPID: string): Observable<HttpResponse<RequestResult<ProductMinimal>>> {
    return this.httpService.getFullRequest<RequestResult<ProductMinimal>>(`${this.routeToGetProductMinimalBySupplierPID}/${supplierPID}`);
  }

  public getOrderNumbersByFirstChars(firstCharsOfOrderNumber: string): Observable<HttpResponse<RequestResult<string[]>>> {
    return this.httpService.getFullRequest<RequestResult<string[]>>(`${this.routeToGetOrderNumbersByFirstChars}/${firstCharsOfOrderNumber}`);
  }

  public getProductMinamalsByPartOfOrderNumber(partOfProductOrderNumber: string): Observable<HttpResponse<RequestResult<ProductMinimal[]>>>{
    return this.httpService.getFullRequest<RequestResult<ProductMinimal[]>>(`${this.routeToGetProductMinamalsByPartOfOrderNumber}/${partOfProductOrderNumber}`);
  }

}


