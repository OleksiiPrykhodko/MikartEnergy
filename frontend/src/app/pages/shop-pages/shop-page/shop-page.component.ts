import { HttpErrorResponse } from '@angular/common/http';
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

  private _minQueryLength: number = 5;
  private _receivedOrderNumbers: string[] = [];
  private _suggestions: string[] = [];
  private _selectedOrderNumber: string;

  private _subscriptionToOrderNumbers: Subscription;
  private _startOfRequestedOrderNumer: string = "";
  private _formGroup: FormGroup = new FormGroup({
    autoCompleteControl: new FormControl("")
  });

  constructor(
    private _productService: ProductService,
    private _router: Router) { }

  ngOnDestroy() {
    this._subscriptionToOrderNumbers?.unsubscribe();
  }

  public getMinQueryLength(): number{
    return this._minQueryLength;
  }

  public getSuggestions(): string[]{
    return this._suggestions;
  }

  public getFormGroup(): FormGroup{
    return this._formGroup;
  }

  public searchOrderNumber(event: AutoCompleteCompleteEvent): void {
    if (event.query.length >= this._minQueryLength) {
      var queryUpperCase = event.query.replace(" ","").toUpperCase();
      var startOfJastEnteredValue = queryUpperCase.substring(0, this._minQueryLength);
      
      if (startOfJastEnteredValue != this._startOfRequestedOrderNumer) {
        this._subscriptionToOrderNumbers = 
        this._productService.getOrderNumbersByFirstChars(startOfJastEnteredValue)
          .subscribe(
            result => {
              // Check HttpResponse body on Null
              if(result.body){
                if(result.body.successful){
                  // All OK
                  this._receivedOrderNumbers = result.body.dto.sort() || [];
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
