import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Product } from 'src/app/models/product/product';

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.scss']
})

export class ProductPageComponent {

  public _infoLoaded: boolean = false;
  public _productIdFromRoute: string = "";
  private _subscriptionToRoutParamChange: Subscription;
  private _product: Product;

  constructor(private activateRoute: ActivatedRoute) {
    this._subscriptionToRoutParamChange =
      activateRoute.params.subscribe(params => this._productIdFromRoute = params["productID"]);
  }

  ngOnDestroy() {
    this._subscriptionToRoutParamChange.unsubscribe();
  }

}
