import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// components
import { CaseFormListComponent } from './case-form-list/case-form-list.component';
import { WdFormComponent } from './wd/wd-form/wd-form.component';


const routes: Routes = [
  { path: 'caseforms', component: CaseFormListComponent },
  { path: 'WD/:id', component: WdFormComponent },
  { path: 'WD', component: WdFormComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CaseFormsRoutingModule { }
