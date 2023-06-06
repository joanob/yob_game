import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StorageRoutingModule } from './storage-routing.module';
import { StorageComponent } from './storage.component';
import { ResourceCardComponent } from './components/resource-card/resource-card.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [StorageComponent, ResourceCardComponent],
  imports: [CommonModule, StorageRoutingModule, FormsModule],
})
export class StorageModule {}
