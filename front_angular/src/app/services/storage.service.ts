import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserService } from '../auth/user.service';
import { environment } from 'src/environment/environment';
import { GamedataService } from './gamedata.service';

export interface ResourceStorage {
  resourceId: number;
  quantity: number;
}

@Injectable({
  providedIn: 'root',
})
export class StorageService {
  public storage: { [resourceId: number]: number } = {};

  constructor(
    private http: HttpClient,
    private gamedataService: GamedataService,
    private userService: UserService
  ) {
    this.getAllStorage();
  }

  getAllStorage() {
    this.http
      .get<{ resourceId: number; quantity: number }[]>(
        `${environment.apiBase}/storage`,
        { withCredentials: true }
      )
      .subscribe((response) => {
        response.map((res) => {
          this.storage[res.resourceId] = res.quantity;
        });
      });
  }

  getResourcesStorage(): ResourceStorage[] {
    return Object.keys(this.storage).map((resourceId) => ({
      resourceId: parseInt(resourceId),
      quantity: this.storage[parseInt(resourceId)],
    }));
  }

  buyResources(resourceId: number, quantity: number) {
    const resource = this.gamedataService.resources.find(
      (res) => res.id === resourceId
    );
    if (!resource) {
      return;
    }

    const cost = resource.price * quantity;
    if (cost > this.userService.user.companyMoney) {
      return;
    }

    this.http
      .post(
        `${environment.apiBase}/storage/buy`,
        { resourceId, quantity },
        { withCredentials: true }
      )
      .subscribe(() => {
        this.storage[resourceId] += quantity;
        this.userService.user.companyMoney -= cost;
      });
  }

  sellResources(resourceId: number, quantity: number) {
    const resource = this.gamedataService.resources.find(
      (res) => res.id === resourceId
    );
    if (!resource) {
      return;
    }

    if (this.storage[resourceId] < quantity) {
      return;
    }

    const cost = resource.price * quantity;

    this.http
      .post(
        `${environment.apiBase}/storage/sell`,
        { resourceId, quantity },
        { withCredentials: true }
      )
      .subscribe(() => {
        this.storage[resourceId] -= quantity;
        this.userService.user.companyMoney += cost;
      });
  }
}
