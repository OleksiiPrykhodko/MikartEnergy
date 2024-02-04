import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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

  constructor( 
    private _activatedRoute: ActivatedRoute,
    private _productService: ProductService) 
  { }

  ngOnInit(){
    this._subscriptionToRoutParamChange = this._activatedRoute.queryParams
      .subscribe(params => {
        this._searchedOrderNumberPart = params["orderNumber"] ?? "";
    
        this._subscriptionToProductMinimals = this._productService
          .getProductMinamalsByPartOfOrderNumber(this._searchedOrderNumberPart)
          .subscribe(
            result => { 
              this._retrievedProducts = result?.body?.dto || [];
              this._infoIsLoaded = false },
            error => {
              if(error instanceof HttpErrorResponse){
                if(error.status === 0){
                  console.log("Client-side or network error occurred");
                }
                else{
                  console.log(`Server error: ${error.status}`);
                }
              }else{
                console.log("Unexpected Error.")
              }
            });
      });

  }

  ngOnDestroy() {
    this._subscriptionToRoutParamChange?.unsubscribe();
    this._subscriptionToProductMinimals?.unsubscribe();
  }

  public checkLoading(): boolean {
    return this._infoIsLoaded;
  }

  public getProducts(){
    return this._retrievedProducts;
  }

}
