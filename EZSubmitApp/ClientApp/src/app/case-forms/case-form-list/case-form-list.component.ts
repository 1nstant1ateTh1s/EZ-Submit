import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

import { CaseForm } from '../../core';

@Component({
  selector: 'app-case-form-list',
  templateUrl: './case-form-list.component.html',
  styleUrls: ['./case-form-list.component.css']
})
export class CaseFormListComponent implements OnInit {
  public displayedColumns: string[] = ['id', 'formType', 'caseNumber', 'hearingDateTime', 'plaintiffName', 'submittedBy', 'submissionDateTime'];
  public caseForms = new MatTableDataSource<CaseForm>();

  defaultPageIndex: number = 0;
  defaultPageSize: number = 10;
  public defaultSortColumn: string = "caseNumber";
  public defaultSortOrder: string = "asc";

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  private displayDateFormat = 'MM/dd/yyyy, h:mm a';

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    var pageEvent = new PageEvent();
    pageEvent.pageIndex = this.defaultPageIndex;
    pageEvent.pageSize = this.defaultPageSize;

    this.getData(pageEvent);
  }

  getData(event: PageEvent) {
    var url = this.baseUrl + 'api/CaseForms';
    var params = new HttpParams()
      .set("pageIndex", event.pageIndex.toString())
      .set("pageSize", event.pageSize.toString())
      .set("sortColumn", (this.sort)
        ? this.sort.active
        : this.defaultSortColumn)
      .set("sortOrder", (this.sort)
        ? this.sort.direction
        : this.defaultSortOrder);

    this.http.get<any>(url, { params })
      .subscribe(result => {
        this.paginator.length = result.totalCount;
        this.paginator.pageIndex = result.pageIndex;
        this.paginator.pageSize = result.pageSize;
        this.caseForms = new MatTableDataSource<CaseForm>(result.data);
      }, error => console.error(error));
  }
}
