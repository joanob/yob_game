import { Component } from '@angular/core';
import { GamedataService } from '../services/gamedata.service';
import { PropertiesService } from '../services/properties.service';

@Component({
  selector: 'app-production',
  templateUrl: './production.component.html',
  styleUrls: ['./production.component.scss'],
})
export class ProductionComponent {
  constructor(
    public gamedataService: GamedataService,
    public propertiesService: PropertiesService
  ) {
    propertiesService.getAllProperties();
  }
}
