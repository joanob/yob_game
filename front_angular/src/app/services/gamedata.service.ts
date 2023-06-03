import { Injectable } from '@angular/core';
import { ProductionBuilding } from '../types/ProductionBuilding';
import { HttpClient } from '@angular/common/http';
import { Resource } from '../types/Resource';
import { environment } from 'src/environment/environment';

@Injectable({
  providedIn: 'root',
})
export class GamedataService {
  public resources: Resource[] = [];
  public productionBuildings: ProductionBuilding[] = [];

  constructor(private http: HttpClient) {
    this.getAllResources();
    this.getAllProductionBuildings();
  }

  getAllResources() {
    this.http
      .get<Resource[]>(`${environment.apiBase}/gamedata/resources`, {
        withCredentials: true,
      })
      .subscribe((res) => {
        this.resources = res;
      });
  }

  getResourceById(resourceId: number) {
    return (
      this.resources.find((res) => res.id === resourceId) || {
        id: 0,
        name: '',
        price: 0,
      }
    );
  }

  getAllProductionBuildings() {
    this.http
      .get<ProductionBuilding[]>(
        `${environment.apiBase}/gamedata/production-buildings`,
        {
          withCredentials: true,
        }
      )
      .subscribe((res) => {
        this.productionBuildings = res;
      });
  }
}
