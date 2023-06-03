import { Component, Input } from '@angular/core';
import { GamedataService } from 'src/app/services/gamedata.service';

@Component({
  selector: '[app-resource-card]',
  templateUrl: './resource-card.component.html',
  styleUrls: ['./resource-card.component.scss'],
})
export class ResourceCardComponent {
  @Input() resourceStorage: { resourceId: number; quantity: number } = {
    resourceId: 0,
    quantity: 0,
  };

  constructor(public gamedataService: GamedataService) {}
}
