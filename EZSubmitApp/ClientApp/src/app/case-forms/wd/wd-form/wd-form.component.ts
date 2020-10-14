import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { WarrantInDebtForm } from '../../../core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-wd-form',
  templateUrl: './wd-form.component.html',
  styleUrls: ['./wd-form.component.css']
})
export class WdFormComponent implements OnInit {

  // the view title
  pageTitle: string;

  // the submit button text
  submitBtnText: string;

  // the form model
  form: FormGroup;

  // the warrant in debt object to edit or create
  wd: WarrantInDebtForm;

  // the warrant in debt object id, as fetched from the active route:
  // It's NULL when we're adding a new warrant in debt,
  // and not NULL when we're editing an existing one.
  id?: number;

  isSubmitting: boolean = false;

  constructor(private activatedRoute: ActivatedRoute,
              private router: Router,
              private http: HttpClient,
              @Inject('BASE_URL') private baseUrl: string)
  { }

  ngOnInit(): void {
    this.form = new FormGroup({
      caseNumber: new FormControl(''),
      hearingDate: new FormControl(''),
      hearingTime: new FormControl(''),
      plaintiffType: new FormControl(''),
      plaintiffName: new FormControl(''),
      plaintiffTaDbaType: new FormControl(''),
      plaintiffTaDbaName: new FormControl(''),
      plaintiffAddress1: new FormControl(''),
      plaintiffAddress2: new FormControl(''),
      plaintiffPhone: new FormControl(''),
      defendantType: new FormControl(''),
      defendantName: new FormControl(''),
      defendantTaDbaName: new FormControl(''),
      defendantAddress1: new FormControl(''),
      defendantAddress2: new FormControl(''),
      defendant2Type: new FormControl(''),
      defendant2Name: new FormControl(''),
      defendant2TaDbaName: new FormControl(''),
      defendant2Address1: new FormControl(''),
      defendant2Address2: new FormControl(''),

      principle: new FormControl(''),
      interest: new FormControl(''),
      interestStartDate: new FormControl(''),
      useDoj: new FormControl(''),
      filingCost: new FormControl(''),
      attorneyFees: new FormControl(''),
      accountType: new FormControl(''),
      accountTypeOther: new FormControl(''),
      homesteadExemptionWaived: new FormControl('')
    });

    this.loadData();

    this.submitBtnText = (this.id) ? "Save" : "Create";
  }

  hasError = (controlName: string, errorName: string) => {
    return this.form.controls[controlName].hasError(errorName);
  }

  loadData() {
    // retrieve the ID from the 'id' parameter
    this.id = +this.activatedRoute.snapshot.paramMap.get('id');
    if (this.id) {
      // EDIT MODE

      // fetch the warrant in debt from the server
      var url = this.baseUrl + "api/CaseForms/" + this.id;
      this.http.get<WarrantInDebtForm>(url).subscribe(result => {
        this.wd = result;
        this.pageTitle = "Edit - " + this.wd.formType + " " + this.wd.caseNumber;

        // update the form with the warrant in debt value
        this.form.patchValue(this.wd);
      }, error => console.error(error));
    }
    else {
      // ADD NEW MODE

      this.pageTitle = "Create a new Warrant in Debt";
    }

  }

  onSubmit() {
    var wd = (this.id) ? this.wd : <WarrantInDebtForm>{};

    console.log("onSubmit() wd obj values BEFORE Object.assign(): ");
    console.log(wd);

    Object.assign(wd, this.form.value);

    console.log("onSubmit() wd obj values AFTER Object.assign(): ");
    console.log(wd);

    if (this.id) {
      // EDIT MODE

      var url = this.baseUrl + "api/CaseForms/" + this.wd.id;
      this.http.put<WarrantInDebtForm>(url, wd).subscribe(result => {

        console.log("Warrant in Debt " + wd.caseNumber + " has been updated.");

        // go back to case forms list view
        this.router.navigate(['/caseforms']);
      }, error => console.error(error));
    }
    else {
      // ADD NEW MODE

      var url = this.baseUrl + "api/CaseForms";
      this.http.post<WarrantInDebtForm>(url, wd).subscribe(result => {

        console.log("Warrant in Debt " + wd.caseNumber + " has been created.");

        // go back to case forms list view
        this.router.navigate(['/caseforms']);
      }, error => console.error(error));
    }
  }

  private _executeWarrantInDebtUpdate = (wdFormValues) => {

  }
}
