import { NgModule } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';

@NgModule({
  declarations: [],
  imports: [
    // material design
    MatTableModule,
    MatPaginatorModule,
    MatSortModule
  ],
  exports: [
    // material design
    MatTableModule,
    MatPaginatorModule,
    MatSortModule
  ]
})
export class AngularMaterialModule { }
