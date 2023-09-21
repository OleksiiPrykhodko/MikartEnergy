import { Component } from '@angular/core';

@Component({
  selector: 'app-contacts-page',
  templateUrl: './contacts-page.component.html',
  styleUrls: ['./contacts-page.component.scss']
})
export class ContactsPageComponent {

  private _audioFile = new Audio();

  ngAfterViewInit(): void {
    this.playSound();
  }
  
  private playSound(): void {
    this._audioFile.src = "../../../assets/audio/RadioUA.mp3";
    this._audioFile.volume = 0.05;
    this._audioFile.load();
    this._audioFile.play();
  }


}
