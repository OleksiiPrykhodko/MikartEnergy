import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ButtonModule } from 'primeng/button';
import { AutoCompleteModule } from 'primeng/autocomplete';

import { HomePageComponent } from './pages/home-page/home-page.component';
import { DesignPageComponent } from './pages/design-page/design-page.component';
import { AboutPageComponent } from './pages/about-page/about-page.component';
import { DevelopmentPageComponent } from './pages/development-page/development-page.component';
import { ContactsPageComponent } from './pages/contacts-page/contacts-page.component';
import { ShopPageComponent } from './pages/shop-pages/shop-page/shop-page.component';
import { ProductPageComponent } from './pages/shop-pages/product-page/product-page.component';
import { ProductsPageComponent } from './pages/shop-pages/products-page/products-page.component';
import { SafeUrlPipe } from './pipes/safe-url/safe-url.pipe';
import { NotFoundPageComponent } from './pages/not-found-page/not-found-page.component';
import { SlimProductMinimalsComponent } from './components/shop-components/slim-product-minimals/slim-product-minimals.component';
import { ConfiguratorResultPageComponent } from './pages/shop-pages/configurator-result-page/configurator-result-page.component';
import { WideProductMinimalsComponent } from './components/shop-components/wide-product-minimals/wide-product-minimals.component';
import { TruncatePipe } from './pipes/truncate/truncate.pipe';
import { TiaStFormComponent } from './components/shop-components/tia-st-form/tia-st-form.component';


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
    WideProductMinimalsComponent,
    TruncatePipe,
    TiaStFormComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    RouterModule,
    FormsModule,

    ButtonModule,
    AutoCompleteModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
