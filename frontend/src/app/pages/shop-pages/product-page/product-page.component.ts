import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Product } from 'src/app/models/product/product';
import { HttpClientModule, HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.scss']
})

export class ProductPageComponent {

  private _infoIsLoaded: boolean = false;
  private _productIdFromRoute: string = "";
  private _subscriptionToRoutParamChange: Subscription;
  private _product: Product;

  constructor(private activateRoute: ActivatedRoute, private http: HttpClient) {
    this._subscriptionToRoutParamChange =
      activateRoute.params.subscribe(params => this._productIdFromRoute = params["productID"]);
  }

  public _body: string = "";

  ngOnInit() {
    //this.http.get("https://sie.ag/2OHKMiZ").subscribe(resp => this._body = resp.toString())
  }

  ngOnDestroy() {
    this._subscriptionToRoutParamChange.unsubscribe();
  }

  public checkLoading(): boolean {
    return this._infoIsLoaded;
  }
  public getProdactId(): string {
    return this._productIdFromRoute;
  }

  public checkProductPageLink(): boolean {
    return true;
  }
  public checkManualsLink(): boolean {
    return true;
  }
  public checkTechnicalDataLink(): boolean {
    return true;
  }
  public checkExamplesLink(): boolean {
    return true;
  }
  public checkFaqLink(): boolean {
    return true;
  }
  public checkPdfLink(): boolean {
    return true;
  }
  public checkVideoLink(): boolean {
    return true;
  }

  public getProductPageLink(): string {
    return "https://www.industry-mobile-support.siemens-info.com/#/en/product/5SL4102-6";
  }
  public getManualsLink(): string {
    return "";
  }
  public getTechnicalDataLink(): string {
    return "";
  }
  public getExamplesLink(): string {
    return "";
  }
  public getFaqLink(): string {
    return "";
  }
  public getPdfLink(): string {
    return "https://static.siemens.com/mimes/imagedb_pdf/G_I202_XX_26118V.pdf";
  }
  public getVideoLink(): string {
    return "";
  }
  public getPriceAmount(): number {
    return 9.50;
  }
  public getPriceCurrency(): string {
    return "EUR"
  }

}
