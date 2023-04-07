import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { DesignPageComponent } from './pages/design-page/design-page.component';
import { AboutPageComponent } from './pages/about-page/about-page.component';
import { DevelopmentPageComponent } from './pages/development-page/development-page.component';
import { ContactsPageComponent } from './pages/contacts-page/contacts-page.component';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    DesignPageComponent,
    AboutPageComponent,
    DevelopmentPageComponent,
    ContactsPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
