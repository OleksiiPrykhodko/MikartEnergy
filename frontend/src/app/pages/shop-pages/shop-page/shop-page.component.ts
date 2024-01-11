import { Component } from '@angular/core';
import { AutoCompleteCompleteEvent } from 'primeng/autocomplete';

@Component({
  selector: 'app-shop-page',
  templateUrl: './shop-page.component.html',
  styleUrls: ['./shop-page.component.scss']
})
export class ShopPageComponent {

  public minQueryLength: number = 5;
  public receivedOrderNumbers: string[];
  private startOfRequestedOrderNumer: string = "";

  selectedItem: string;

  suggestions: string[];

  search(event: AutoCompleteCompleteEvent) {
    this.suggestions = [...Array(10).keys()].map(item => event.query + '-' + item);
  }

  searchOrderNumber(event: AutoCompleteCompleteEvent) {
    if (event.query.length >= this.minQueryLength) {
      var StartOfJastEnteredValue = event.query.substring(0, this.minQueryLength).toLowerCase();
      if (StartOfJastEnteredValue != this.startOfRequestedOrderNumer) {
       this.startOfRequestedOrderNumer = StartOfJastEnteredValue;
        //this.receivedOrderNumbers = 
        //  service.searchOrderNumberByFirstChars(this.startOfEnteredOrderNumer)
        //  .map(orderNumber => orderNumber.toLowerCase()) ;
      };

      var filtered: string[] = [];
      var query = event.query.toLowerCase();

      for (let i = 0; i < this.receivedOrderNumbers.length; i++) {
          var oderNumber = this.receivedOrderNumbers[i];
          if (oderNumber.indexOf(query) == 0) {
              filtered.push(oderNumber);
          }
      }

      this.suggestions = filtered;
    }
  }

}
