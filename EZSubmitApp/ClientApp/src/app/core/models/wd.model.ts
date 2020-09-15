import { CaseForm } from "./case-form.model";

export interface WarrantInDebtForm extends CaseForm {
  principle: number;
  interest: number;
  interestStartDate?: Date;
  useDoj: boolean;
  filingCost: number;
  attorneyFees: number;
  accountType: string;
  accountTypeOther: string;
  homesteadExemptionWaived: string;
}
