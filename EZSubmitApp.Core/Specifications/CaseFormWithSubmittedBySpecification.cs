using EZSubmitApp.Core.Entities;
using EZSubmitApp.Core.Specifications.Base;

namespace EZSubmitApp.Core.Specifications
{
    public class CaseFormWithSubmittedBySpecification : BaseSpecification<CaseForm>
    {
        public CaseFormWithSubmittedBySpecification()
            :base(null)
        {
            AddInclude(c => c.SubmittedBy);
        }

        public CaseFormWithSubmittedBySpecification(int caseFormId)
            : base(c => c.Id == caseFormId)
        {
            AddInclude(c => c.SubmittedBy);
        }

        public CaseFormWithSubmittedBySpecification(string userName)
            : base(c => c.SubmittedBy.UserName == userName)
        {
            AddInclude(c => c.SubmittedBy);
        }
    }
}
