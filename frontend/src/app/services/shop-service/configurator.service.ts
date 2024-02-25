import { Injectable } from '@angular/core';
import { HttpInternalService } from '../http-internal.service';
import { Observable } from 'rxjs';
import { HttpResponse } from '@angular/common/http';
import { RequestResult } from 'src/app/models/common/request-result';
import { TiaStProductsOrder } from 'src/app/models/configurator/tiast-products-order';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ConfiguratorService {

  public _routePrefix = '/api/configurator';
  private _linlToTiaStConfigurator = "https://mall.industry.siemens.com/tst/?edition=siemens_test_ua";
  private _linlToTiaStConfiguratorWithProductPID = "https://mall.industry.siemens.com/tst/?edition=siemens_test_ua&manufacturer_pid="
  private _tiaStHookUrl = `${environment.apiUrl}${this._routePrefix}`;
  private _startImgPath = "assets/images/StartTiaSt.svg";
  private _startTransparentImgPath = "assets/images/StartTiaStInvert.svg";

  constructor(private httpService: HttpInternalService) { }

  public getConfigurationResultById(resultID: string): Observable<HttpResponse<RequestResult<TiaStProductsOrder>>> {
    return this.httpService.getFullRequest<RequestResult<TiaStProductsOrder>>(`${this._routePrefix}/${resultID}`);
  }

  public getLinkToTiaStConfigurator(): string{
    return this._linlToTiaStConfigurator;
  }

  public getLinkToTiaStConfiguratorWithProductPID(productPID: string): string{
    if(productPID){
      return `${this._linlToTiaStConfiguratorWithProductPID}${productPID}`
    }
    return this._linlToTiaStConfiguratorWithProductPID;
  }

  public getTiaStHookUrl(): string{
    return this._tiaStHookUrl;
  }

  public getStartImgPath(): string{
    return this._startImgPath;
  }

  public getStartTransparentImgPath(): string{
    return this._startTransparentImgPath;
  }

}
