import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
  private _searchedOrderNumberPart: string = "";
  private _retrievedProducts: ProductMinimal[] = [];

  constructor( 
    private _activatedRoute: ActivatedRoute,
    private _productService: ProductService) 
  {
    this._subscriptionToRoutParamChange = _activatedRoute.queryParams
      .subscribe(params => this._searchedOrderNumberPart = params["orderNumber"] ?? "");
  }

  ngOnInit(){
    
  }

  ngOnDestroy() {
    this._subscriptionToRoutParamChange?.unsubscribe();
  }

  public getProducts(){
    return this._retrievedProducts;
  }

}
