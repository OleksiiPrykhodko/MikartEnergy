import { Component } from '@angular/core';
import { Router, NavigationEnd} from '@angular/router';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'MikartEnergy';

  private _routerSubscription: Subscription = Subscription.EMPTY;
  private _routeNamePathPair: [routeName: string, routePath: string][] = [];

  constructor(private router:Router){
  }

  ngOnInit(){
    this._routerSubscription = this.router.events.pipe(filter(event => event instanceof NavigationEnd))
    .subscribe((event: any) => this.HandleNavigationEndEvent(event))
  }

  ngOnDestroy(){
    this._routerSubscription.unsubscribe();
   }

  public get RouteNamePathPair() : [routeName: string, routePath: string][] {
    return this._routeNamePathPair;
  }

  private HandleNavigationEndEvent(navigationEndEvent : any){
    var currentRoute = navigationEndEvent.url.substring(1);
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
  }

}
