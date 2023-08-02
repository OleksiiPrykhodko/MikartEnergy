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
import { SafeUrlPipe } from './pipes/safe-url/safe-url.pipe';
import { NotFoundPageComponent } from './pages/not-found-page/not-found-page.component';
import { SlimProductMinimalsComponent } from './components/shop-components/slim-product-minimals/slim-product-minimals.component';
import { ConfiguratorResultPageComponent } from './pages/shop-pages/configurator-result-page/configurator-result-page.component';
import { WideProductMinimalsComponent } from './components/shop-components/wide-product-minimals/wide-product-minimals.component';



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
    SafeUrlPipe,
    NotFoundPageComponent,
    SlimProductMinimalsComponent,
    ConfiguratorResultPageComponent,
    WideProductMinimalsComponent
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
