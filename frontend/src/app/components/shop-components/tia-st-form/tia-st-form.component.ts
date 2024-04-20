import { Component, Input } from '@angular/core';
import { ConfiguratorService } from 'src/app/services/shop-service/configurator.service';

@Component({
  selector: 'app-tia-st-form',
  templateUrl: './tia-st-form.component.html',
  styleUrls: ['./tia-st-form.component.scss']
})
export class TiaStFormComponent {

  @Input() _productPid: string;
  @Input() _shadow: boolean;
  @Input() _buttonIsTransparent: boolean;
  @Input() _width: string;

  constructor(private _configuratorService: ConfiguratorService) {
    
  }

  public getHookUrl(): string{
    return this._configuratorService.getTiaStHookUrl();
  }

  public getLinkToConfigurator(): string{
   return this._productPid ? this._configuratorService.getLinkToTiaStConfiguratorWithProductPID(this._productPid) :  this._configuratorService.getLinkToTiaStConfigurator(); 
  }

  public getImg(): string{
    return this._buttonIsTransparent ? this._configuratorService.getStartTransparentImgPath() : this._configuratorService.getStartImgPath();
  }

}
