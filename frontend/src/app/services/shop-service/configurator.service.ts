import { Injectable } from '@angular/core';
import { HttpInternalService } from '../http-internal.service';
import { Observable } from 'rxjs';
import { HttpResponse } from '@angular/common/http';
import { RequestResult } from 'src/app/models/common/request-result';
import { TiaStProductsOrder } from 'src/app/models/configurator/tiast-products-order';

@Injectable({
  providedIn: 'root'
})
export class ConfiguratorService {

  public routePrefix = '/api/configurator'

  constructor(private httpService: HttpInternalService) { }

  public getConfigurationResultById(productID: string): Observable<HttpResponse<RequestResult<TiaStProductsOrder>>> {
    return this.httpService.getFullRequest<RequestResult<TiaStProductsOrder>>(`${this.routePrefix}/${productID}`);
  }
}
