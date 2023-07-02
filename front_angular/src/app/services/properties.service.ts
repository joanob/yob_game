import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GamedataService } from './gamedata.service';
import { UserService } from '../auth/user.service';
import { environment } from 'src/environment/environment';
import { Property } from '../types/Property';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PropertiesService {
  public properties: Property[] = [];

  constructor(
    private http: HttpClient,
    private gamedataService: GamedataService,
    private userService: UserService
  ) {
    this.getAllProperties();
  }

  getAllProperties() {
    this.http
      .get<Property[]>(`${environment.apiBase}/property`, {
        withCredentials: true,
      })
      .subscribe((response) => {
        this.properties = response;
      });
  }

  buyProductionBuilding(prodBuildingId: number): Observable<boolean> {
    const success = new Subject<boolean>();

    const prodBuilding = this.gamedataService.productionBuildings.find(
      (pb) => pb.id === prodBuildingId
    );
    if (!prodBuilding) {
      success.next(false);
      return success;
    }

    if (prodBuilding.buildCost > this.userService.user.companyMoney) {
      success.next(false);
      return success;
    }

    this.http
      .post<{ property: Property }>(
        `${environment.apiBase}/property`,
        { prodBuildingId },
        { withCredentials: true }
      )
      .subscribe({
        next: (res) => {
          success.next(true);
          this.properties.push(res.property);
          this.userService.user.companyMoney -= prodBuilding.buildCost;
        },
        error: () => {
          success.next(false);
        },
      });

    return success;
  }
}
