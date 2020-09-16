import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CaseFormListComponent } from './case-form-list/case-form-list.component';


const routes: Routes = [
  {
    path: 'caseforms',
    component: CaseFormListComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CaseFormsRoutingModule { }
