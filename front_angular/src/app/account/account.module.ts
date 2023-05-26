import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from './account-routing.module';
import { AccountComponent } from './account.component';
import { SettingsComponent } from './settings/settings.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [AccountComponent, SettingsComponent],
  imports: [CommonModule, AccountRoutingModule, FormsModule],
})
export class AccountModule {}
