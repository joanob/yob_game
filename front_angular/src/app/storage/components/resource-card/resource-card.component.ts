import { Component, Input } from '@angular/core';
import { UserService } from 'src/app/auth/user.service';
import { StorageService } from 'src/app/services/storage.service';
import { Resource } from 'src/app/types/Resource';

@Component({
  selector: '[app-resource-card]',
  templateUrl: './resource-card.component.html',
  styleUrls: ['./resource-card.component.scss'],
})
export class ResourceCardComponent {
  @Input() resource: Resource;
  public inputQuantity: number = 0;

  constructor(
    public storageService: StorageService,
    public userService: UserService
  ) {}

  sellMax() {
    this.inputQuantity = this.storageService.storage[this.resource.id] || 0;
  }

  buyMax() {
    const money = this.userService.user.companyMoney;
    this.inputQuantity = Math.floor(money / this.resource.price);
  }

  sellResources() {
    this.storageService.sellResources(this.resource.id, this.inputQuantity);
    this.inputQuantity = 0;
  }

  buyResources() {
    this.storageService.buyResources(this.resource.id, this.inputQuantity);
    this.inputQuantity = 0;
  }
}
