using EZSubmitApp.Core.Entities;
using EZSubmitApp.Core.IRepositories;
using EZSubmitApp.Core.Paging;
using EZSubmitApp.Core.ResourceParameters;
using EZSubmitApp.Core.Specifications;
using EZSubmitApp.Infrastructure.Data;
using EZSubmitApp.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EZSubmitApp.Infrastructure.Repository
{
    public class CaseFormRepository : EfRepository<CaseForm>, ICaseFormRepository
    {
        public CaseFormRepository(EZSubmitDbContext dbContext)
            : base(dbContext)
        {
        }

        public override async Task<CaseForm> GetByIdAsync(int caseFormId)
        {
            var spec = new CaseFormWithSubmittedBySpecification(caseFormId);
            return await FirstOrDefaultAsync(spec);
        }

        public async Task<IEnumerable<CaseForm>> GetCaseFormsAsync()
        {
            var allCaseFormsSpec = new CaseFormWithSubmittedBySpecification();
            return await GetAsync(allCaseFormsSpec);
        }

        public async Task<IPagedList<CaseForm>> SearchCaseFormsAsync(CaseFormParameters caseFormParams)
        {
            // Paging & Sorting Option #1: Using specification pattern
            //var allCaseFormsSpec = new CaseFormWithSubmittedBySpecification(args.PageIndex * args.PageSize, args.PageSize);
            //return await GetAsync(allCaseFormsSpec);

            // Paging & Sorting Option #2: Using PagedList<> instance
            var query = Table.Include(c => c.SubmittedBy);

            // Building filter criteria
            var filterList = new List<Expression<Func<CaseForm, bool>>>();
            if (!String.IsNullOrEmpty(caseFormParams.CaseNumber))
            {
                filterList.Add(c => c.CaseNumber == caseFormParams.CaseNumber);
            }

            if (caseFormParams.HideTransmitted)
            {
                filterList.Add(c => !c.TransferredToState);
            }

            // TODO: Remaining filter criteria ...

            if (!String.IsNullOrEmpty(caseFormParams.Search))
            {
                var searchString = caseFormParams.Search.Trim().ToLower();

                filterList.Add(c => c.CaseNumber.ToLower().Contains(searchString)
                                || c.HearingDateTime.ToString().Contains(searchString)
                                || c.SubmittedBy.FirstName.ToLower().Contains(searchString)
                                || c.SubmittedBy.LastName.ToLower().Contains(searchString));

                // TODO: Remaining properties to search on ...
            }

            //query = query.Where(filterList);

            return await PagedList<CaseForm>.CreateAsync(query, new PageSortArgs { PageIndex = caseFormParams.PageIndex, PageSize = caseFormParams.PageSize, SortColumn = caseFormParams.SortColumn, SortOrder = caseFormParams.SortOrder }, filterList);
        }

        public async Task<IEnumerable<CaseForm>> GetCaseFormsByUserAsync(string userName)
        {
            var spec = new CaseFormWithSubmittedBySpecification(userName);
            return await GetAsync(spec);
        }
    }
}
