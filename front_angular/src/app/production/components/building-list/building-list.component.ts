import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { GamedataService } from 'src/app/services/gamedata.service';
import { PropertiesService } from 'src/app/services/properties.service';

@Component({
  selector: 'app-building-list',
  templateUrl: './building-list.component.html',
  styleUrls: ['./building-list.component.scss'],
})
export class BuildingListComponent {
  constructor(
    public gamedataService: GamedataService,
    private propertyService: PropertiesService,
    private router: Router
  ) {}

  buyProperty(prodBuildingId: number) {
    this.propertyService
      .buyProductionBuilding(prodBuildingId)
      .subscribe((success) => {
        if (success) {
          this.router.navigate(['/production']);
        } else {
          // Display error
        }
      });
  }
}
