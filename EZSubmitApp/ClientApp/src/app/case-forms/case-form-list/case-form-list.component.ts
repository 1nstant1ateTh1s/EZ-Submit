import { Component, OnInit, Inject } from '@angular/core';

import { CaseForm } from '../../core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-case-form-list',
  templateUrl: './case-form-list.component.html',
  styleUrls: ['./case-form-list.component.css']
})
export class CaseFormListComponent implements OnInit {
  public displayedColumns: string[] = ['id', 'formType', 'caseNumber', 'hearingDateTime', 'plaintiffName', 'submittedBy', 'submissionDateTime'];
  public caseForms: CaseForm[];

  private displayDateFormat = 'MM/dd/yyyy, h:mm a';

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit(): void {
    this.http.get<CaseForm[]>(this.baseUrl + 'api/CaseForms')
      .subscribe(result => {
        this.caseForms = result;
      }, error => console.error(error));
  }

}
