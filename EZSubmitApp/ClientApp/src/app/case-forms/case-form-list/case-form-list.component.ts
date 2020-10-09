import { Component, OnInit, Inject, ViewChild, AfterViewInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { formatDate } from '@angular/common';

import { CaseForm } from '../../core';

@Component({
  selector: 'app-case-form-list',
  templateUrl: './case-form-list.component.html',
  styleUrls: ['./case-form-list.component.css']
})
export class CaseFormListComponent implements OnInit, AfterViewInit {
  public displayedColumns: string[] = ['id', 'formType', 'caseNumber', 'hearingDateTime', 'plaintiffName', 'submittedBy', 'submissionDateTime', 'transferredToState'];
  public caseForms = new MatTableDataSource<CaseForm>();

  defaultPageIndex: number = 0;
  defaultPageSize: number = 10;
  public defaultSortColumn: string = "caseNumber";
  public defaultSortOrder: string = "asc";

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  // Multiple filter criteria allowed - global text filter + checkbox filter for Is Transmitted? column
  globalFilter: string = '';
  hideTransmitted: boolean = false;
  // Object used to track filter criteria on individual columns
  filterValues = {
    transferredToState: ''
  };

  private displayDateFormat = 'MM/dd/yyyy, h:mm a';

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit(): void {
    this.loadData();
  }

  ngAfterViewInit(): void {

    console.log("DEBUG: Entering ngAfterViewInit() function ...");

    // Overwriting filterPredicate here for custom filter behavior
    //this.caseForms.filterPredicate = this.multiFilterPredicate();
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

    if (this.globalFilter) {
      params = params
        .set("search", this.globalFilter);
    }

    this.http.get<any>(url, { params })
      .subscribe(result => {
        this.paginator.length = result.totalCount;
        this.paginator.pageIndex = result.pageIndex;
        this.paginator.pageSize = result.pageSize;
        this.caseForms = new MatTableDataSource<CaseForm>(result.data);
      }, error => console.error(error));
  }

/** FILTER / SELECTION */

  // Filter entire table based on provided filter string
  public doFilter = (value: string) => {

    console.log("DEBUG: Entering doFilter() function ...");
    console.log("DEBUG: value param = " + value);

    // NOTE: multi filter predicate is not working yet ...
    this.globalFilter = value.trim().toLocaleLowerCase();
    this.caseForms.filter = value.trim().toLocaleLowerCase();

    // Note: Global filter is tracked separately from individual column filters
    //this.globalFilter = value.trim().toLocaleLowerCase();
    //this.caseForms.filter = JSON.stringify(this.filterValues);
  }

  // Provide custom filter behavior - the user can search across all table columns using the global filter box, and/or 
  // can independently filter out rows that have already been transmitted to State
  public multiFilterPredicate(): (data: CaseForm, filter: string) => boolean {

    console.log("DEBUG: Entering multiFilterPredicate() function ...");

    let multiFilterFunction = (data, filter): boolean => {

      console.log("DEBUG: Entering multiFilterFunction() function ...");
      console.log("DEBUG: data param = " + data, data);
      console.log("DEBUG: filter param = " + filter, filter);

      var globalMatch = !this.globalFilter;

      // Filter using global filter first, if present
      if (this.globalFilter) {
        // Search all displayed fields w/ global filter
        globalMatch = this.searchDisplayedColumns(data, this.globalFilter);
      }

      if (!globalMatch) {
        return;
      }

      // Filter specifically on Is Transmitted? column
      let searchString = JSON.parse(filter);
      return data.transferredToState.toString().trim().indexOf(searchString.transferredToState) !== -1;
    }
    return multiFilterFunction;
  }

  // Search only displayed columns for the given filter string
  public searchDisplayedColumns = (data, filter) => {

    console.log("DEBUG: Entering searchDisplayedColumns() function ...");
    console.log("DEBUG: data param = " + data, data);
    console.log("DEBUG: filter param = " + filter, filter);

    // TODO: TEST THE PERFORMANCE OF THIS CODE WITH A LARGE DATASET TO FILTER ON

    // TODO: REFACTOR THIS FUNCTION NOW THAT I DON'T NEED TO WORRY ABOUT DIFFERENTIATING BETWEEN "DISPLAYED" & "NON-DISPLAYED" COLUMNS IN MY TABLE DATA SOURCE.

    // Return true if data object is considered a match
    let isMatch = false;
    for (let col of this.displayedColumns) {

      // Only filter on displayed columns that exist on the data object and that contain data, not the entire data object from the database
      if (col in data && (data[col] !== '' && data[col] !== null)) {

        // TODO: FIX BUG: The filter on Manage Forms screen incorrectly matches rows when a column containing a number value is inadvertently interpreted as a datetime value

        // If there is a date, convert it to display format before filtering
        // *NOTE: If a string can't convert to a date, we treat it as a non-date column, 
        // else we treat it as a date column 
        if (new Date(data[col].toString()).toString() == "Invalid Date") {
          // Non date columns
          isMatch = (isMatch || data[col].toString().trim().toLocaleLowerCase().indexOf(filter) !== -1);
        }
        else {
          // Date column - change to the format in which date is displayed on table
          let date = formatDate(new Date(data[col].toString()), this.displayDateFormat, 'en');
          isMatch = (isMatch || date.indexOf(filter) !== -1);
        }
      }
    }
    return isMatch;
  }
}
