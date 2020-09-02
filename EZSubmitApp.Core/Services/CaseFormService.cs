﻿using AutoMapper;
using EZSubmitApp.Core.Constants;
using EZSubmitApp.Core.DTOs;
using EZSubmitApp.Core.Entities;
using EZSubmitApp.Core.Interfaces;
using EZSubmitApp.Core.IRepositories;
using EZSubmitApp.Core.Mapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EZSubmitApp.Core.Services
{
    public class CaseFormService : ICaseFormService
    {
        private readonly ICaseFormRepository _caseFormRepo;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CaseFormService(
            ICaseFormRepository caseFormRepo,
            IMapper mapper,
            ILogger<CaseFormService> logger)
        {
            _caseFormRepo = caseFormRepo;
            // TODO: DI for IEmailService, & IDocxConverter
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CaseFormDto>> GetCaseForms()
        {
            _logger.LogInformation(LoggingEvents.ListItems, "Listing all case forms from database.");

            var caseFormList = await _caseFormRepo.GetCaseFormsAsync();
            var caseFormDtos = _mapper.Map<IEnumerable<CaseFormDto>>(caseFormList);
            //var caseFormDtos = ObjectMapper.Mapper.Map<IEnumerable<CaseFormDto>>(caseFormList);

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
            // TODO: Make sure SubmittedBy gets set to currently logged in/authenticated user.
            newCaseForm = await _caseFormRepo.AddAsync(newCaseForm);
            _logger.LogInformation(LoggingEvents.InsertItem, "Case form {Id} created", newCaseForm.Id);

            // TODO: Call EmailService to send submission receipt email to user
            // TODO: Call EmailService to send submission receipt email to admin?

            var caseFormDto = _mapper.Map<CaseFormDto>(newCaseForm);
            return caseFormDto;
        }

        public Task UpdateCaseForm(CaseFormDto caseFormDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCaseFormById(int id)
        {
            throw new NotImplementedException();
        }

    }
}
