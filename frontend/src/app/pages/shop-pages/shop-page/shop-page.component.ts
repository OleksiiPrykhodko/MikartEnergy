import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AutoCompleteCompleteEvent } from 'primeng/autocomplete';
import { Subscription} from 'rxjs';
import { ProductService } from 'src/app/services/shop-service/product.service';

@Component({
  selector: 'app-shop-page',
  templateUrl: './shop-page.component.html',
  styleUrls: ['./shop-page.component.scss']
})
export class ShopPageComponent {

  public _minQueryLength: number = 5;
  public _receivedOrderNumbers: string[] = [];
  public _suggestions: string[] = [];
  public _selectedOrderNumber: string;

  private _orderNumbersSubscription: Subscription;
  private _startOfRequestedOrderNumer: string = "";
  public _formGroup: FormGroup = new FormGroup({
    autoCompleteControl: new FormControl("")
  });

  constructor(
    private _productService: ProductService,
    private _router: Router) { }

  ngOnDestroy() {
    this._orderNumbersSubscription?.unsubscribe();
  }

  public searchOrderNumber(event: AutoCompleteCompleteEvent): void {
    if (event.query.length >= this._minQueryLength) {
      var queryUpperCase = event.query.replace(" ","").toUpperCase();
      var startOfJastEnteredValue = queryUpperCase.substring(0, this._minQueryLength);
      
      if (startOfJastEnteredValue != this._startOfRequestedOrderNumer) {
        this._orderNumbersSubscription = 
        this._productService.getOrderNumbersByFirstChars(startOfJastEnteredValue)
          .subscribe(result => 
            {
              if(result.url !== null && result.body !== null && result.ok){
                if(result.body.successful){
                  // All OK
                  this._receivedOrderNumbers = result.body.dto.sort();
                  this._startOfRequestedOrderNumer = startOfJastEnteredValue;
                  this._suggestions = this.filterOrderNumbers(this._receivedOrderNumbers, queryUpperCase);
                }
                else{
                  // Result model with successful = false
                  // Show all errors 
                    result.body.errors.forEach(error => console.log(`Error: ${error.key}. Description: ${error.key}.`));
                }
              }
              else{
                if(result.url == null){
                  console.log(`The server is not available.`);
                }
                else{
                  if(result.body == null){
                    console.log(`Server response error. No required response body.`);
                  }
                  console.log(`Something go wrong. HTTP response code: ${result.status}`);
                }
              }  
            });  
      }
      else{
        this._suggestions = this.filterOrderNumbers(this._receivedOrderNumbers, queryUpperCase);
      };
    }
  }

  private filterOrderNumbers(orderNumbers: string[], beginningOfOrderNumber: string): string[]{
    var filtered: string[] = [];
    for (let i = 0; i < orderNumbers.length; i++) {
      var oderNumber = orderNumbers[i];
      if (oderNumber.indexOf(beginningOfOrderNumber) == 0) {
        filtered.push(oderNumber);
      }
    }
    return filtered;
  }

  public submitForm(): void{
    var autoCompleteValue = this._formGroup.value.autoCompleteControl;
    if(autoCompleteValue?.length > 1){
      this._router.navigate(['/shop/search'], {queryParams: {orderNumber : autoCompleteValue}});
    }
  }

}
