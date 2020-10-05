using EZSubmitApp.Core.Entities;
using EZSubmitApp.Core.IRepositories.Base;
using EZSubmitApp.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EZSubmitApp.Core.IRepositories
{
    public interface ICaseFormRepository : IAsyncRepository<CaseForm>
    {
        Task<IEnumerable<CaseForm>> GetCaseFormsAsync();
        Task<IPagedList<CaseForm>> SearchCaseFormsAsync(PageSearchArgs args);
        Task<IEnumerable<CaseForm>> GetCaseFormsByUserAsync(string userName);
    }
}
