import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { AutoCompleteCompleteEvent } from 'primeng/autocomplete';
import { Subscription } from 'rxjs';
import { ProductMinimal } from 'src/app/models/product/prodact-minimal';
import { ProductService } from 'src/app/services/shop-service/product.service';

@Component({
  selector: 'app-search-products-page',
  templateUrl: './search-products-page.component.html',
  styleUrl: './search-products-page.component.scss'
})
export class SearchProductsPageComponent {

  private _subscriptionToRoutParamChange: Subscription;
  private _subscriptionToProductMinimals: Subscription;
  private _searchedOrderNumberPart: string = "";
  private _retrievedProducts: ProductMinimal[] = [];
  private _infoIsLoaded: boolean = true; 

  private _subscriptionToOrderNumbers: Subscription;
  public _minQueryLength: number = 5;
  private _startOfRequestedOrderNumer: string = "";
  public _receivedOrderNumbers: string[] = [];
  public _suggestions: string[] = [];

  public _formGroup: FormGroup = new FormGroup({
    autoCompleteControl: new FormControl("")
  });

  constructor( 
    private _activatedRoute: ActivatedRoute,
    private _router: Router,
    private _productService: ProductService) 
  { }

  ngOnInit(){
    this._subscriptionToRoutParamChange = this._activatedRoute.queryParams
      .subscribe(params => {
        this._searchedOrderNumberPart = params["orderNumber"] ?? "";
        this._formGroup.get("autoCompleteControl")?.setValue(this._searchedOrderNumberPart);

        this._subscriptionToProductMinimals = this._productService
          .getProductMinamalsByPartOfOrderNumber(this._searchedOrderNumberPart)
          .subscribe(
            result => { 
              // Check HttpResponse body on Null
              if(result.body){
                this._retrievedProducts = result.body.dto || [];
                this._retrievedProducts = this.orderProductMinimalsByOrderNumber(this._retrievedProducts);
                this._infoIsLoaded = false
              }else{
                console.error("HttpResponse body can't be NULL.");
              }},
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
      });

  }

  ngOnDestroy() {
    this._subscriptionToRoutParamChange?.unsubscribe();
    this._subscriptionToProductMinimals?.unsubscribe();
    this._subscriptionToOrderNumbers?.unsubscribe();
  }

  public checkLoading(): boolean {
    return this._infoIsLoaded;
  }

  public getProducts(){
    return this._retrievedProducts;
  }

  // Callback method to invoke to search for suggestions in AutoComplete input.
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
    var autoCompleteValue = this._formGroup.get("autoCompleteControl")?.value;
    if(autoCompleteValue?.length > 1){
      this._router.navigate(['/shop/search'], {queryParams: {orderNumber : autoCompleteValue}});
    }
  }

  private orderProductMinimalsByOrderNumber(productMinimals: ProductMinimal[]): ProductMinimal[]{
    var result = productMinimals.sort((productA, productB) =>{
      if (productA.orderNumber < productB.orderNumber) {
        return -1;
      } else if (productA.orderNumber > productB.orderNumber) {
        return 1;
      } else {
        return 0;
      }
    });
    return result;
  }

}
