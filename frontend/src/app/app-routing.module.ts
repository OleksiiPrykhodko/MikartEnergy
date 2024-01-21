import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutPageComponent } from './pages/about-page/about-page.component';
import { DesignPageComponent } from './pages/design-page/design-page.component';
import { DevelopmentPageComponent } from './pages/development-page/development-page.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { ContactsPageComponent } from './pages/contacts-page/contacts-page.component';
import { ShopPageComponent } from './pages/shop-pages/shop-page/shop-page.component';
import { ProductsPageComponent } from './pages/shop-pages/products-page/products-page.component';
import { ProductPageComponent } from './pages/shop-pages/product-page/product-page.component';
import { NotFoundPageComponent } from './pages/not-found-page/not-found-page.component';
import { ConfiguratorResultPageComponent } from './pages/shop-pages/configurator-result-page/configurator-result-page.component';
import { SearchProductsPageComponent } from './pages/shop-pages/search-products-page/search-products-page.component';

const routes: Routes = [
  { path: "", component: HomePageComponent },
  { path: "about", component: AboutPageComponent },
  { path: "development", component: DevelopmentPageComponent },
  { path: "contacts", component: ContactsPageComponent },
  { path: "shop", component: ShopPageComponent},
  { path: "shop/configurator/:resultID", component: ConfiguratorResultPageComponent },
  { path: "shop/products", component: ProductsPageComponent },
  { path: "shop/products/:productID", component: ProductPageComponent },
  { path: "shop/search/:searchedOrderNumberPart", component: SearchProductsPageComponent},
  { path: "design", component: DesignPageComponent },
  { path: "404", component: NotFoundPageComponent},
  { path: "**", redirectTo: "404"} //  { path: "**", component: NotFoundPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
