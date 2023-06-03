import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StorageRoutingModule } from './storage-routing.module';
import { StorageComponent } from './storage.component';
import { ResourceCardComponent } from './components/resource-card/resource-card.component';


@NgModule({
  declarations: [
    StorageComponent,
    ResourceCardComponent
  ],
  imports: [
    CommonModule,
    StorageRoutingModule
  ]
})
export class StorageModule { }
