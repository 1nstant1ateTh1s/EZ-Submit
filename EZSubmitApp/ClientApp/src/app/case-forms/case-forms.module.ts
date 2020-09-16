import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CaseFormsRoutingModule } from './case-forms-routing.module';
import { WdFormComponent } from './wd/wd-form/wd-form.component';


@NgModule({
  declarations: [WdFormComponent],
  imports: [
    CommonModule,
    CaseFormsRoutingModule
  ]
})
export class CaseFormsModule { }
