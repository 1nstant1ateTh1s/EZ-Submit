import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

import { CaseForm } from '../../core';

@Component({
  selector: 'app-case-form-list',
  templateUrl: './case-form-list.component.html',
  styleUrls: ['./case-form-list.component.css']
})
export class CaseFormListComponent implements OnInit {
  public displayedColumns: string[] = ['id', 'formType', 'caseNumber', 'hearingDateTime', 'plaintiffName', 'submittedBy', 'submissionDateTime'];
  public caseForms = new MatTableDataSource<CaseForm>();

  @ViewChild(MatPaginator) paginator: MatPaginator;

  private displayDateFormat = 'MM/dd/yyyy, h:mm a';

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit(): void {
    this.http.get<CaseForm[]>(this.baseUrl + 'api/CaseForms')
      .subscribe(result => {
        this.caseForms.data = result as CaseForm[];
      }, error => console.error(error));
  }

  ngAfterViewInit(): void {
    this.caseForms.paginator = this.paginator;
  }
}
