import { Component } from '@angular/core';
// Routing
import { Router, NavigationEnd} from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'MikartEnergy';

  private _currentRoute: string[] = [];

  constructor(router:Router){
    router.events.pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: any) => {
        var currentRoute = event.url.substring(1);
        if(currentRoute.length > 0){
          this._currentRoute = currentRoute.split("/", 3);
        }
      })
  }

  public get CurrentRoute() : string[] {
    return this._currentRoute;
  }

}
