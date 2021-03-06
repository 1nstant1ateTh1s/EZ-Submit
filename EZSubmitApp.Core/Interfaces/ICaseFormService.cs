﻿using EZSubmitApp.Core.DTOs;
using EZSubmitApp.Core.Paging;
using EZSubmitApp.Core.ResourceParameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EZSubmitApp.Core.Interfaces
{
    public interface ICaseFormService
    {
        Task<CaseFormDto> GetCaseFormById(int id);
        Task<byte[]> GetCaseFormAsDocx(int id);
        Task<IEnumerable<CaseFormDto>> GetCaseForms();
        Task<IPagedList<CaseFormDto>> SearchCaseForms(CaseFormParameters caseFormParams);
        Task<IEnumerable<CaseFormDto>> GetCaseFormsByUser(string userName);
        Task<CaseFormDto> CreateCaseForm(CaseFormForCreationDto caseFormForCreation);
        Task UpdateCaseForm(int id, CaseFormForUpdateDto caseFormForUpdate);
        Task DeleteCaseFormById(int id);
        Task<bool> IsDupeField(int id, string fieldName, string fieldValue);
    }
}
