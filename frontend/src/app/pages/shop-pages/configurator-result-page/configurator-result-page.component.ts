import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { keyValuePair } from 'src/app/models/common/keyValuePair';
import { TiaStProductsOrder } from 'src/app/models/configurator/tiast-products-order';
import { ProductMinimal } from 'src/app/models/product/prodact-minimal';
import { ConfiguratorService } from 'src/app/services/shop-service/configurator.service';

@Component({
  selector: 'app-configurator-result-page',
  templateUrl: './configurator-result-page.component.html',
  styleUrls: ['./configurator-result-page.component.scss']
})
export class ConfiguratorResultPageComponent {

  private _infoIsLoading: boolean = true;
  private _subscriptionToRoutParamChange: Subscription;
  private _configurationResultIdFromRoute: string;
  private _configuratorSubscription: Subscription;
  private _tiaStProductsOrder: TiaStProductsOrder;

  constructor( 
    private _activateRoute: ActivatedRoute,
    private _configuratorService: ConfiguratorService,
    private _router: Router) 
  {
    this._subscriptionToRoutParamChange =
      _activateRoute.params.subscribe(params => this._configurationResultIdFromRoute = params["resultID"]);
  }

  ngOnInit(){
    this._configuratorSubscription = this._configuratorService.getConfigurationResultById(this._configurationResultIdFromRoute)
    .subscribe(
      result => {
        // Check HttpResponse body on Null
        if(result.body){
          if(result.body.successful){
            // All OK
            this._tiaStProductsOrder = result.body?.dto;
            this._infoIsLoading = false;
          }
          else{
            // Result model with successful = false
            // If the error is Id not found (key = "NotFound"), redirect to page "page not found".
            if(result.body.errors.some(e => e.key === "NotFound")){
              this._router.navigate(['404']);
            }
            else{
              // Show all errors 
              result.body.errors.forEach(error => console.log(`Error: ${error.key}. Description: ${error.key}.`));
            }
          }
        }
        else{
          console.error("HttpResponse body can't be NULL.");
        }
      },
      error => {
        if(error instanceof HttpErrorResponse){
          if(error.status === 0){
            console.error("Client-side or network error occurred.");
          }
          else{
            console.error(`Server error: ${error.status}.`);
          }
        }else{
          console.error("Unexpected Error.")
        }
      });  
  }

  ngOnDestroy() {
    this._subscriptionToRoutParamChange?.unsubscribe();
    this._configuratorSubscription?.unsubscribe();
  }

  public getExistingProducts(): keyValuePair<ProductMinimal, number>[]{
    return this._tiaStProductsOrder?.existingInDbProducts ?? [];
  }

  public getUnExistingProducts(): keyValuePair<keyValuePair<string, string>, number>[]{
    return this._tiaStProductsOrder?.notExistingInDbProducts ?? [];
  }

}
