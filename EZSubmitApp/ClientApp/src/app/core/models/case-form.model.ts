import { User } from './user.model';

export interface CaseForm {
  id?: string; // id is present if editing or returning from DB

  formType: string;
  caseNumber: string;
  hearingDateTime: Date;

  // Plaintiff
  plaintiffType: string;
  plaintiffName: string;
  plaintiffTaDbaType: string;
  plaintiffTaDbaName: string;
  plaintiffAddress1: string;
  plaintiffAddress2: string;
  plaintiffPhone: string;

  // Defendant #1
  defendantType: string;
  defendantName: string;
  defendantTaDbaName: string;
  defendantAddress1: string;
  defendantAddress2: string;

  // Defendant #2
  defendant2Type: string;
  defendant2Name: string;
  defendant2TaDbaName: string;
  defendant2Address1: string;
  defendant2Address2: string;

  submittedBy: User;
  submissionDateTime: Date;
  transferredToState: boolean;
  transferDateTime?: Date;
}
