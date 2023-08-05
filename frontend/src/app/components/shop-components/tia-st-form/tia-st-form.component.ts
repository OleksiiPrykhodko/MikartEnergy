import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-tia-st-form',
  templateUrl: './tia-st-form.component.html',
  styleUrls: ['./tia-st-form.component.scss']
})
export class TiaStFormComponent {

  @Input() _productPid: string;
  @Input() _shadow: boolean;
  @Input() _transparent: boolean;
  @Input() _width: string;

  constructor() {
    
  }

  public getHookurl(): string{
    return "https://localhost:44363/api/Configurator";
  }
  public getLinkToConfigurator(): string{
   return this._productPid ? `https://mall.industry.siemens.com/tst/?edition=siemens_test_ua&manufacturer_pid=${this._productPid}` :  "https://mall.industry.siemens.com/tst/?edition=siemens_test_ua"; 
  }
  public getImg(): string{
    return this._transparent ? "assets/images/StartTiaStInvert.svg" : "assets/images/StartTiaSt.svg";
  }

}
