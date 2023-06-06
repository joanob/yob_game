import { Component, Input } from '@angular/core';
import { StorageService } from 'src/app/services/storage.service';
import { Resource } from 'src/app/types/Resource';

@Component({
  selector: '[app-resource-card]',
  templateUrl: './resource-card.component.html',
  styleUrls: ['./resource-card.component.scss'],
})
export class ResourceCardComponent {
  @Input() resource: Resource;

  constructor(public storageService: StorageService) {}
}
