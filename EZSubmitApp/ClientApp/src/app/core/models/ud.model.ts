import { CaseForm } from "./case-form.model";

export interface SummonsForUnlawfulDetainerForm extends CaseForm {
  principle: number;
  rentPeriodStartDate: Date;
  rentPeriodEndDate: Date;
  lateFee: number;
  damagesAmount: number;
  damagesFor: string;
  interest: number;
  interestStartDate?: Date;
  useDoj: boolean;
  filingCost: number;
  civilRecoveryAmount: number;
  attorneyFees: number;
  isAmountDueAtHearing: boolean;
  useToTerminateTenancy: boolean;
}
