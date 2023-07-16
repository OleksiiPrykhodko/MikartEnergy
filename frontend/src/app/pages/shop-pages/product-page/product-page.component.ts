import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Product } from 'src/app/models/product/product';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { ProductMinimal } from 'src/app/models/product/prodact-minimal';
import { ProductService } from 'src/app/services/shop-service/product.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.scss']
})

export class ProductPageComponent {

  private _infoIsLoading: boolean = true;
  private _productIdFromRoute: string = "";
  private _subscriptionToRoutParamChange: Subscription;
  private _product: Product;
  private _productSubscription: Subscription;
  private _relatedProducts: ProductMinimal[] = [];

  constructor(
    private _activateRoute: ActivatedRoute,
    private _productService: ProductService) 
  {
    this._subscriptionToRoutParamChange =
      _activateRoute.params.subscribe(params => this._productIdFromRoute = params["productID"]);
  }

  ngOnInit() {
    this._productSubscription = this._productService.getProductById(this._productIdFromRoute)
    .subscribe(result =>
      {
        if(result.url !== null && result.body !== null && result.status >= 200 && result.status < 400){
          if(result.body?.successful){
            // All OK
            this._product = result.body?.dto;
            //this._relatedProducts = 
            this._infoIsLoading = false;
          }
          else{
            // Result model with successful = false
            // If the error is Id not found, redirect to page "page not found".
            result.body?.errors.forEach(error => console.log(`Error: ${error.key}. Description: ${error.key}.`));
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
      })
  }

  ngOnDestroy() {
    this._subscriptionToRoutParamChange.unsubscribe();
    this._productSubscription.unsubscribe();
  }


  public checkLoading(): boolean {
    return this._infoIsLoading;
  }

  public checkProductPageLink(): boolean {
    return this.getProductPageLink() !== "";
  }
  public checkManualsLink(): boolean {
    return this.getManualsLink() !== "";
  }
  public checkTechnicalDataLink(): boolean {
    return this.getTechnicalDataLink() !== "";
  }
  public checkExamplesLink(): boolean {
    return this.getExamplesLink() !== "";
  }
  public checkFaqLink(): boolean {
    return this.getFaqLink() !== "";
  }
  public checkPdfLink(): boolean {
    return this.getPdfLink() !== "";
  }
  public checkVideoLink(): boolean {
    return this.getVideoLink() !== "";
  }
  public checkEmbededVideoLink(): boolean{
    return this.getEmbededVideoLink() !== "";
  }
  public checkRelatedProducts(): boolean {
    return this._relatedProducts.length > 0;
  }

  public getProductName(): string{
    return this._product ? this._product.productName : "";
  }
  public getImageHighQualityURL(): string{
    return this._product ? this._product.imageHighQualityURL : "";
  }
  public getOrderNumber(): string{
    return this._product ? this._product.orderNumber : "";
  }
  public getLongDescription(): string{
    return this._product ? this._product.longDescription : "";
  }
  public getPriceAmount(): number {
    return this._product ? this._product.price : 0;
  }
  public getPriceCurrency(): string {
    return this._product ? this._product.priceCurrency : "";
  }

  public getProductPageLink(): string {
    return this._product ? this._product.linkToProductPage : "";
  }
  public getManualsLink(): string {
    return this._product ? this._product.linkToManuals : "";
  }
  public getTechnicalDataLink(): string {
    return this._product ? this._product.linkToTechnicalData : "";
  }
  public getExamplesLink(): string {
    return this._product ? this._product.linkToApplicationExample : "";
  }
  public getFaqLink(): string {
    return this._product ? this._product.linkToFAQ : "";
  }
  public getPdfLink(): string {
    return this._product ? this._product.pdfWith3dURL : "";
  }
  public getVideoLink(): string {
    return this._product ? this._product.linkToVideo : "";
  }
  public getEmbededVideoLink(): string{
    // The link from the backend can come empty and comes in the wrong format. 
    if(this._product && this._product.linkToVideo.startsWith("https://youtu.be/")){
      var str = this._product.linkToVideo.replace("https://youtu.be/", "https://www.youtube.com/embed/")
      return str;
    }
    return "";
  }

  public getRelatedProducts(): ProductMinimal[]{
    return this._relatedProducts;
  }

  //private loadRelatedProducts(orderNumbers: string[]): ProductMinimal[]{
  //  
  //}

}
