using AutoMapper;
using DocxConverterService.Interfaces;
using DocxConverterService.Models;
using EZSubmitApp.Core.Constants;
using EZSubmitApp.Core.DTOs;
using EZSubmitApp.Core.Entities;
using EZSubmitApp.Core.Interfaces;
using EZSubmitApp.Core.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EZSubmitApp.Core.Services
{
    public class CaseFormService : ICaseFormService
    {
        private readonly IDocxConverter _docxConverterService;
        private readonly ICaseFormRepository _caseFormRepo;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public CaseFormService(
            IDocxConverter docxConverterService,
            ICaseFormRepository caseFormRepo,
            IMapper mapper,
            ILogger<CaseFormService> logger,
            UserManager<ApplicationUser> userManager)
        {
            _docxConverterService = docxConverterService;
            _caseFormRepo = caseFormRepo;
            // TODO: DI for IEmailService
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IEnumerable<CaseFormDto>> GetCaseForms()
        {
            _logger.LogInformation(LoggingEvents.ListItems, "Listing all case forms from database.");

            var caseFormList = await _caseFormRepo.GetCaseFormsAsync();
            var caseFormDtos = _mapper.Map<IEnumerable<CaseFormDto>>(caseFormList);

            return caseFormDtos;
        }

        public async Task<CaseFormDto> GetCaseFormById(int id)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "Getting case form {Id}", id);

            var caseForm = await _caseFormRepo.GetByIdAsync(id);
            if (caseForm == null) _logger.LogWarning(LoggingEvents.GetItemNotFound, "Case form {Id} NOT FOUND", id);
            var caseFormDto = _mapper.Map<CaseFormDto>(caseForm);

            return caseFormDto;
        }

        public async Task<byte[]> GetCaseFormAsDocx(int id)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "Getting case form {Id}", id);

            var caseForm = await _caseFormRepo.GetByIdAsync(id);
            if (caseForm == null) _logger.LogWarning(LoggingEvents.GetItemNotFound, "Case form {Id} NOT FOUND", id);

            // TODO: Map CaseForm to IGeneratable
            IGeneratable generatable = null;
            // TODO: Now, I need to look up the Case Form entity for the given id, and 
            //          map it's values into the model representing the Docx form fields
            /* TEMP */
            if (caseForm is WarrantInDebtForm)
            {
                WarrantInDebtForm wd = caseForm as WarrantInDebtForm;
                generatable = new WarrantInDebtDocxForm()
                {
                    Fields = new WarrantInDebtDocxFormFields()
                    {
                        Court = "CHESAPEAKE", // TODO: READ FROM CONFIGURATION
                        CourtAddress = "307 Albemarle Drive, Suite 200B, Chesapeake, VA 23322, PH-757-382-3143 FAX-757-382-3113", // TODO: READ FROM CONFIGURATION
                        HearingDate = wd.HearingDateTime.ToString("MM/dd/yyyy"),
                        HearingTime = wd.HearingDateTime.ToString("hh:mm tt"),
                        Principle = wd.Principle.ToString(),
                        Interest = wd.Interest.ToString(),
                        InterestStartDate = wd.UseDoj ? "DOJ" : wd.InterestStartDate?.ToString("MM/dd/yyyy"),
                        FilingCost = wd.FilingCost.ToString(),
                        AttorneyFees = wd.AttorneyFees.ToString(),
                        A1 = (wd.AccountType == "Open Account" ? "X" : ""),
                        A2 = (wd.AccountType == "Contract" ? "X" : ""),
                        A3 = (wd.AccountType == "Note" ? "X" : ""),
                        A4 = (wd.AccountType == "Other" ? "X" : ""),
                        AccountTypeOther = wd.AccountTypeOther ?? "{accountTypeOther}",
                        B1 = (wd.HomesteadExemptionWaived == "Yes" ? "X" : ""),
                        B2 = (wd.HomesteadExemptionWaived == "No" ? "X" : ""),
                        B3 = (wd.HomesteadExemptionWaived == "Cannot be determined" ? "X" : ""),
                        Date2 = "", // NOTE: not currently represented on front-end form
                        C1 = "", // NOTE: not currently represented on front-end form
                        C2 = "", // NOTE: not currently represented on front-end form
                        C3 = "", // NOTE: not currently represented on front-end form
                        CaseNumber = wd.CaseNumber,
                        PlaintiffName = wd.PlaintiffName,
                        PlaintiffAddress1 = wd.PlaintiffTaDbaName,
                        PlaintiffAddress2 = wd.PlaintiffAddress1,
                        PlaintiffAddress3 = wd.PlaintiffAddress2,
                        PlaintiffPhone = wd.PlaintiffPhone,
                        DefendantName = wd.DefendantName,
                        DefendantAddress1 = String.Format("{0} {1}", wd.DefendantAddress1, wd.DefendantAddress2),
                        DefendantAddress2 = wd.Defendant2Name,
                        DefendantAddress3 = String.Format("{0} {1}", wd.Defendant2Address1, wd.Defendant2Address2),
                        // TODO: I do not have access to profile / attorney info at this point with a new '*ForCreationDto' obj
                        AttyForPlaintiff1 = "",
                        AttyForPlaintiff2 = "",
                        AttyForDefendant1 = "",
                        AttyForDefendant2 = "",
                        // NOTE: None of these fields are currently represented on front-end form
                        ReturnName1 = "",
                        ReturnName1a = "",
                        ReturnAddress1 = "",
                        ReturnAddress1a = "",
                        ReturnPhone1 = "",
                        ReturnName2 = "",
                        ReturnName2a = "",
                        ReturnAddress2 = "",
                        ReturnAddress2a = "",
                        ReturnPhone2 = "",
                        ReturnName3 = "",
                        ReturnName3a = "",
                        ReturnAddress3 = "",
                        ReturnAddress3a = "",
                        ReturnPhone3 = "",
                        Date3 = "",
                        D1 = "",
                        D2 = "",
                        D3 = ""
                    }
                };
            }
            else if (caseForm is SummonsForUnlawfulDetainerForm)
            {
                // todo: instantiate new SummonsForUnlawfulDetainerDocxForm()
            }
            /* TEMP */

            return await _docxConverterService.Convert(generatable);
        }

        public async Task<IEnumerable<CaseFormDto>> GetCaseFormsByUser(string userName)
        {
            _logger.LogInformation(LoggingEvents.ListItems, "Getting all case forms for {UserName}", userName);

            var caseForms = await _caseFormRepo.GetCaseFormsByUserAsync(userName);
            if (!caseForms.Any()) _logger.LogWarning(LoggingEvents.GetItemNotFound, "No case forms found for {UserName}", userName);
            var caseFormDtos = _mapper.Map<IEnumerable<CaseFormDto>>(caseForms);

            return caseFormDtos;
        }

        public async Task<CaseFormDto> CreateCaseForm(CaseFormForCreationDto caseFormForCreation)
        {
            var newCaseForm = _mapper.Map<CaseForm>(caseFormForCreation);

            // ! TEMP !
            // TODO: Make sure SubmittedBy gets set to currently logged in/authenticated user.
            newCaseForm.SubmittedBy = await _userManager.FindByNameAsync("ambrown@cityofchesapeake.net"); // ! currently just for testing purposes
            // ! TEMP !

            newCaseForm = await _caseFormRepo.AddAsync(newCaseForm);
            _logger.LogInformation(LoggingEvents.InsertItem, "Case form {Id} created", newCaseForm.Id);

            // TODO: Call EmailService to send submission receipt email to user
            // TODO: Call EmailService to send submission receipt email to admin?

            var caseFormDto = _mapper.Map<CaseFormDto>(newCaseForm);
            return caseFormDto;
        }

        public async Task UpdateCaseForm(int id, CaseFormForUpdateDto caseFormForUpdate)
        {
            var existingCaseForm = await _caseFormRepo.GetByIdAsync(id);
            if (existingCaseForm == null)
            {
                // TODO: Global exception handling can take care of the call to logger... I can remove these lines to logger once global exception handling is added in
                _logger.LogWarning(LoggingEvents.UpdateItemNotFound, "Case form {Id} NOT FOUND", id);
                throw new ApplicationException($"Case form with id {id} does not exist.");
            }

            // Map any updated field values in the dto back to the entity
            _mapper.Map(caseFormForUpdate, existingCaseForm);

            await _caseFormRepo.UpdateAsync(existingCaseForm);
            _logger.LogInformation(LoggingEvents.UpdateItem, "Case form {Id} Updated", existingCaseForm.Id);
        }

        public async Task DeleteCaseFormById(int id)
        {
            var existingCaseForm = await _caseFormRepo.GetByIdAsync(id);
            if (existingCaseForm == null)
            {
                // TODO: Global exception handling can take care of the call to logger... I can remove these lines to logger once global exception handling is added in
                _logger.LogWarning(LoggingEvents.DeleteItemNotFound, "Case form {Id} NOT FOUND", id);
                throw new ApplicationException($"Case form with id {id} does not exist.");
            }

            await _caseFormRepo.DeleteAsync(existingCaseForm);
            _logger.LogInformation(LoggingEvents.DeleteItem, "Case form {Id} deleted", id);
        }

    }
}
