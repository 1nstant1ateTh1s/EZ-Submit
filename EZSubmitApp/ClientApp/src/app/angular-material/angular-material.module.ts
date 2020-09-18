import { NgModule } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';

@NgModule({
  declarations: [],
  imports: [
    // material design
    MatTableModule,
    MatPaginatorModule
  ],
  exports: [
    // material design
    MatTableModule,
    MatPaginatorModule
  ]
})
export class AngularMaterialModule { }
