import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';

import { HomePageComponent } from './pages/home-page/home-page.component';
import { DesignPageComponent } from './pages/design-page/design-page.component';
import { AboutPageComponent } from './pages/about-page/about-page.component';
import { DevelopmentPageComponent } from './pages/development-page/development-page.component';
import { ContactsPageComponent } from './pages/contacts-page/contacts-page.component';
import { ShopPageComponent } from './pages/shop-pages/shop-page/shop-page.component';
import { ProductPageComponent } from './pages/shop-pages/product-page/product-page.component';
import { ProductsPageComponent } from './pages/shop-pages/products-page/products-page.component';
import { HttpClientModule } from '@angular/common/http';
import { ProductMinimalSlimComponent } from './components/shop-components/product-minimal-slim/product-minimal-slim.component';
import { ProductMinimalWideComponent } from './components/shop-components/product-minimal-wide/product-minimal-wide.component';
import { SafeUrlPipe } from './pipes/safe-url/safe-url.pipe';
import { NotFoundPageComponent } from './pages/not-found-page/not-found-page.component';



@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    DesignPageComponent,
    AboutPageComponent,
    DevelopmentPageComponent,
    ContactsPageComponent,
    ShopPageComponent,
    ProductPageComponent,
    ProductsPageComponent,
    ProductMinimalSlimComponent,
    ProductMinimalWideComponent,
    SafeUrlPipe,
    NotFoundPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    RouterModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
