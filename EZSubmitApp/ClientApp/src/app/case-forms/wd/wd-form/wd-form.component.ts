import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { WarrantInDebtForm, Option } from '../../../core';
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
  isOtherAcctTypeSelected: boolean = false;
  isSecondDefendantAdded: boolean = false;

  // the hearing time options for the select
  hearingTimeOptions: Option[] = [
    { value: '9:00 AM', display: '9:00 AM' },
    { value: '10:00 AM', display: '10:00 AM' },
    { value: '11:00 AM', display: '11:00 AM' }
  ]

  // the entity type options for the select
  entityTypes: Option[] = [
    { value: 'I', display: 'Individual' },
    { value: 'C', display: 'Company' }
  ]

  // the ta/dba type options for the select
  taDbaTypes: Option[] = [
    { value: 'TA', display: 'Trading As' },
    { value: 'DBA', display: 'Doing Business As' }
  ]

  // the account type options for the select
  accountTypes: Option[] = [
    { value: 'Open Account', display: 'Open Account' },
    { value: 'Contract', display: 'Contract' },
    { value: 'Note', display: 'Note' },
    { value: 'Other', display: 'Other' }
  ]

  // the homestead exemption waived options for the select
  homesteadExemptionWaivedOptions: string[] = [

  ]

  constructor(private activatedRoute: ActivatedRoute,
              private router: Router,
              private http: HttpClient,
              @Inject('BASE_URL') private baseUrl: string)
  { }

  ngOnInit(): void {
    this.form = new FormGroup({
      caseNumber: new FormControl('', Validators.required),
      hearingDate: new FormControl('', Validators.required),
      hearingTime: new FormControl('', Validators.required),
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

      principle: new FormControl('', Validators.pattern('\\d+\\.?\\d{0,2}$')),
      interest: new FormControl('', Validators.pattern('\\d+\\.?\\d{0,2}$')),
      interestStartDate: new FormControl(''),
      useDoj: new FormControl(''),
      filingCost: new FormControl('', Validators.pattern('\\d+\\.?\\d{0,2}$')),
      attorneyFees: new FormControl('', Validators.pattern('\\d+\\.?\\d{0,2}$')),
      accountType: new FormControl(''),
      accountTypeOther: new FormControl(''),
      homesteadExemptionWaived: new FormControl('')
    });

    this.loadData();

    this.submitBtnText = (this.id) ? "Save" : "Create";

    if (this.id) {
      console.log('value of this.wd.defendant2Name: ' + this.wd.defendant2Name);
    }
    this.isSecondDefendantAdded = (this.id) && (this.wd.defendant2Name != null && this.wd.defendant2Name.trim() != ''); // set indicator if Defendant #2 section show be visible when editing an existing form
  }


  // Retrieve a FormControl
  getControl(name: string) {
    return this.form.get(name);
  }

  // Returns TRUE if the FormControl is raising an error
  // i.e. an invalid state after user changes
  hasError(name: string) {
    var e = this.getControl(name);
    return e && (e.dirty || e.touched) && e.invalid;
  }

  //hasError = (controlName: string, errorName: string) => {
  //  return this.form.controls[controlName].hasError(errorName);
  //}

  useDojChanged() {
    // Disable Interest Start Date control when Use DOJ option is checked
    this.form.get('useDoj').value == true ?
      this.form.get('interestStartDate').disable() :
      this.form.get('interestStartDate').enable();
  }

  onAccountTypeChange(event) {
    // Track whether 'Other' account type option is selected. If it is, then provide an add't. text input for the user
    let selected = event.value;
    if (selected == "Other") {
      this.isOtherAcctTypeSelected = true;
      this.form.get('accountTypeOther').enable();
    }
    else {
      this.isOtherAcctTypeSelected = false;
      this.form.get('accountTypeOther').disable();
    }
  }

  addDefendant() {
    // Display Defendant #2 form fields
    this.isSecondDefendantAdded = true;
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
