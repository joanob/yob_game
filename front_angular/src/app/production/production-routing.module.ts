import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductionComponent } from './production.component';
import { BuildingListComponent } from './components/building-list/building-list.component';

const routes: Routes = [
  { path: '', component: ProductionComponent },
  { path: 'buy', component: BuildingListComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProductionRoutingModule {}
