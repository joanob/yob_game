import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductionRoutingModule } from './production-routing.module';
import { ProductionComponent } from './production.component';
import { BuildingListComponent } from './components/building-list/building-list.component';

@NgModule({
  declarations: [ProductionComponent, BuildingListComponent],
  imports: [CommonModule, ProductionRoutingModule],
})
export class ProductionModule {}
