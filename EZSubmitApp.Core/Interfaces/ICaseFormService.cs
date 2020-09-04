using EZSubmitApp.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EZSubmitApp.Core.Interfaces
{
    public interface ICaseFormService
    {
        Task<CaseFormDto> GetCaseFormById(int id);
        Task<IEnumerable<CaseFormDto>> GetCaseForms();
        Task<IEnumerable<CaseFormDto>> GetCaseFormsByUser(string userName);
        Task<CaseFormDto> CreateCaseForm(CaseFormForCreationDto caseFormForCreation);
        Task UpdateCaseForm(int id, CaseFormForUpdateDto caseFormForUpdate);
        Task DeleteCaseFormById(int id);
    }
}
