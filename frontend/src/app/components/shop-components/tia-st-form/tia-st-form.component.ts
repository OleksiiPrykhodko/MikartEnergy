import { Component, Input } from '@angular/core';
import { ConfiguratorService } from 'src/app/services/shop-service/configurator.service';

@Component({
  selector: 'app-tia-st-form',
  templateUrl: './tia-st-form.component.html',
  styleUrls: ['./tia-st-form.component.scss']
})
export class TiaStFormComponent {

  @Input() _productPid: string;
  @Input() _shadow: boolean = true;
  @Input() _buttonIsTransparent: boolean = false;
  @Input() _width: string;
  @Input() _styleAsLink: boolean = false;
  @Input() _linkText: string = "add text of link (@Input _linkText)";
  @Input() _linkTextStylingClass: string = "";

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

  public getLinkText(): string{
    return this._linkText;
  }

  public buttonStyledAsLink(): boolean{
    return this._styleAsLink;
  }

  public getStylingClass(): string{
    return this._linkTextStylingClass;
  }

}
