import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { CaseFormsRoutingModule } from './case-forms-routing.module';
import { WdFormComponent } from './wd/wd-form/wd-form.component';
import { CaseFormListComponent } from './case-form-list/case-form-list.component';


@NgModule({
  declarations: [WdFormComponent, CaseFormListComponent],
  imports: [
    SharedModule,
    CaseFormsRoutingModule
  ]
})
export class CaseFormsModule { }
