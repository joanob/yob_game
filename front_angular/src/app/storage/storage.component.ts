import { Component } from '@angular/core';
import { GamedataService } from '../services/gamedata.service';

@Component({
  selector: 'app-storage',
  templateUrl: './storage.component.html',
  styleUrls: ['./storage.component.scss'],
})
export class StorageComponent {
  constructor(public gamedataService: GamedataService) {}
}
