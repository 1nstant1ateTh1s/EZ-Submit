using EZSubmitApp.Core.Entities;
using EZSubmitApp.Core.IRepositories;
using EZSubmitApp.Core.Paging;
using EZSubmitApp.Core.Specifications;
using EZSubmitApp.Infrastructure.Data;
using EZSubmitApp.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<IPagedList<CaseForm>> SearchCaseFormsAsync(PageSearchArgs args)
        {
            // Paging & Sorting Option #1: Using specification pattern
            //var allCaseFormsSpec = new CaseFormWithSubmittedBySpecification(args.PageIndex * args.PageSize, args.PageSize);
            //return await GetAsync(allCaseFormsSpec);

            // Paging & Sorting Option #2: Using PagedList<> instance
            var query = Table.Include(c => c.SubmittedBy);
            return await PagedList<CaseForm>.CreateAsync(query, args);
        }

        public async Task<IEnumerable<CaseForm>> GetCaseFormsByUserAsync(string userName)
        {
            var spec = new CaseFormWithSubmittedBySpecification(userName);
            return await GetAsync(spec);
        }
    }
}
