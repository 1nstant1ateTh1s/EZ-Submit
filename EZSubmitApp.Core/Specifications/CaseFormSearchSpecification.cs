using EZSubmitApp.Core.Entities;
using EZSubmitApp.Core.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZSubmitApp.Core.Specifications
{
    public class CaseFormSearchSpecification : BaseSpecification<CaseForm>
    {
        public CaseFormSearchSpecification(string searchString)
            :base(c => c.CaseNumber.ToLower().Contains(searchString.Trim().ToLower()))
        {
            AddInclude(c => c.SubmittedBy);
        }
    }
}
