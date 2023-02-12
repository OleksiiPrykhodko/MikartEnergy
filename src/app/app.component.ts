import { Component } from '@angular/core';
import { Router, NavigationEnd} from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'MikartEnergy';

  private _routeNamePathPair: [routeName: string, routePath: string][] = [];

  constructor(router:Router){
    router.events.pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: any) => {
        var currentRoute = event.url.substring(1);
        if(currentRoute.length > 0){
          currentRoute = currentRoute.split("/", 3);
          var routeNamePathPair: [string, string][] = [];
          for (let index = 0; index < currentRoute.length; index++) {
            if(index === 0){
              routeNamePathPair.push([currentRoute[index], "/"+currentRoute[index]])
            }
            else{
              routeNamePathPair.push([currentRoute[index], `${currentRoute[index - 1]}/${currentRoute[index]}`]);
            }
          }
          this._routeNamePathPair = routeNamePathPair;
        }
      })
  }

  public get RouteNamePathPair() : [routeName: string, routePath: string][] {
    return this._routeNamePathPair;
  }

}
