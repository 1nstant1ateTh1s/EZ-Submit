using AutoMapper;
using DocxConverterService.Interfaces;
using DocxConverterService.Models;
using EZSubmitApp.Core.Configuration;
using EZSubmitApp.Core.Constants;
using EZSubmitApp.Core.DTOs;
using EZSubmitApp.Core.Entities;
using EZSubmitApp.Core.Extensions;
using EZSubmitApp.Core.Interfaces;
using EZSubmitApp.Core.IRepositories;
using EZSubmitApp.Core.Paging;
using EZSubmitApp.Core.ResourceParameters;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        private readonly ServiceRunSettings _serviceRunSettings;

        public CaseFormService(
            IDocxConverter docxConverterService,
            ICaseFormRepository caseFormRepo,
            IMapper mapper,
            ILogger<CaseFormService> logger,
            UserManager<ApplicationUser> userManager,
            IOptions<ServiceRunSettings> serviceRunSettings)
        {
            _docxConverterService = docxConverterService;
            _caseFormRepo = caseFormRepo;
            // TODO: DI for IEmailService
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _serviceRunSettings = serviceRunSettings.Value;
        }

        public async Task<IEnumerable<CaseFormDto>> GetCaseForms()
        {
            _logger.LogInformation(LoggingEvents.ListItems, "Listing all case forms from database.");

            var caseFormList = await _caseFormRepo.GetCaseFormsAsync();
            var caseFormDtos = _mapper.Map<IEnumerable<CaseFormDto>>(caseFormList);

            return caseFormDtos;
        }

        public async Task<IPagedList<CaseFormDto>> SearchCaseForms(CaseFormParameters caseFormParams)
        {
            _logger.LogInformation(LoggingEvents.ListItems, "Listing case forms from database based on paging & searching arguments.");

            var caseFormPagedList = await _caseFormRepo.SearchCaseFormsAsync(caseFormParams);

            _logger.LogInformation(LoggingEvents.ListItems, $"Retrieved {caseFormPagedList.TotalCount} case forms from database.");

            // TODO: better mapping solution between PagedList<TSource> and PagedList<TDestination> ...
            //      ... maybe create a PagedListConverter ??
            var caseFormDtos = _mapper.Map<IEnumerable<CaseFormDto>>(caseFormPagedList.Data);

            var caseFormDtosPagedList = new StaticPagedList<CaseFormDto>(
                caseFormDtos,
                caseFormPagedList.TotalCount,
                caseFormPagedList.PageIndex,
                caseFormPagedList.PageSize,
                caseFormPagedList.SortColumn,
                caseFormPagedList.SortOrder);

            return caseFormDtosPagedList;
        }

        public async Task<CaseFormDto> GetCaseFormById(int id)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "Getting case form {Id}", id);

            var caseForm = await _caseFormRepo.GetByIdAsync(id);
            if (caseForm == null) _logger.LogWarning(LoggingEvents.GetItemNotFound, "Case form {Id} NOT FOUND", id);
            var caseFormDto = _mapper.Map<CaseFormDto>(caseForm);

            return caseFormDto;
        }

        /// <summary>
        /// Gets a case form entity that has been converted to it's .docx form.
        /// </summary>
        public async Task<byte[]> GetCaseFormAsDocx(int id)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "Getting case form {Id}", id);

            var caseForm = await _caseFormRepo.GetByIdAsync(id);
            if (caseForm == null) _logger.LogWarning(LoggingEvents.GetItemNotFound, "Case form {Id} NOT FOUND", id);

            IGeneratable generatable = null;
            if (caseForm is WarrantInDebtForm)
            {
                generatable = _mapper.Map<WarrantInDebtDocxForm>(caseForm, opt =>
                {
                    opt.Items.Add("court", _serviceRunSettings.CaseFormSettings.CourtName);
                    opt.Items.Add("courtAddress", _serviceRunSettings.CaseFormSettings.CourtAddress);
                    // TODO: If above works, can pass in user profile /attorney info here
                });
            }
            else if (caseForm is SummonsForUnlawfulDetainerForm)
            {
                // todo: map new SummonsForUnlawfulDetainerDocxForm()
            }

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

        public async Task<bool> IsDupeField(int id, string fieldName, string fieldValue)
        {
            var testIfPropExists = typeof(CaseForm).HasProperty(fieldName);
            if (testIfPropExists) {
                // Build up the expression clause to be used to check if exists

                // Example of query we will eventually run:
                // _context.CaseForms.Any(c => c.CaseNumber == fieldValue && c.Id != id)

                var parameter = Expression.Parameter(typeof(CaseForm), "c"); // c =>
                var fieldNameProperty = Expression.Property(parameter, fieldName); // c.[fieldName]
                var fieldNameClause = Expression.Equal(fieldNameProperty, Expression.Constant(fieldValue)); // c.[fieldName] == [fieldValue]

                // The 'Id' check will conditionally disable the 'dupe check' when the user is editing an existing case form, 
                // since keeping a value the same would be allowed. When a new case form is being added, the Id value will always
                // be set to 0, preventing the 'dupe check' from being disabled.
                var idProperty = Expression.Property(parameter, "Id"); // c.Id
                var idClause = Expression.NotEqual(idProperty, Expression.Constant(id)); // c.Id != id

                // Build up the AND statement
                var body = Expression.AndAlso(fieldNameClause, idClause); // c.[fieldName] == [fieldValue] && c.Id != id

                // The final Lambda expression
                var expression = Expression.Lambda<Func<CaseForm, bool>>(body, parameter); // c => c.[fieldName] == [fieldValue] && c.Id != id

                var result = await _caseFormRepo.IfExistsAsync(expression);
                return result;

                //var result = await _caseFormRepo.IfExistsAsync(String.Format("{0} == @0 && Id == @1", fieldName, fieldValue, id));
                //var result = await _caseFormRepo.IfExistsAsync(c => nameof(c.GetType().GetProperty(fieldName)) == fieldValue);
            }
            return false;
        }
    }
}
