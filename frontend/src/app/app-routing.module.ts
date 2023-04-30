import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutPageComponent } from './pages/about-page/about-page.component';
import { DesignPageComponent } from './pages/design-page/design-page.component';
import { DevelopmentPageComponent } from './pages/development-page/development-page.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { ContactsPageComponent } from './pages/contacts-page/contacts-page.component';

const routes: Routes = [
  {path: "", component: HomePageComponent},
  {path: "about", component: AboutPageComponent},
  {path: "development", component: DevelopmentPageComponent},
  {path: "contacts", component: ContactsPageComponent},
  {path: "design", component: DesignPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
