using EZSubmitApp.Controllers;
using EZSubmitApp.Core.Constants;
using EZSubmitApp.Core.DTOs;
using EZSubmitApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace EZSubmitApp.UnitTests.Controllers
{
    public class CaseFormsController_Tests
    {

        public CaseFormsController_Tests()
        {
        }

        /// <summary>
        /// Test the Get(int id) method when case form exists.
        /// </summary>
        [Fact]
        public async void GetById_WhenCaseFormExists_ReturnsOkResult()
        {
            #region Arrange
            var id = 1;
            var mockCaseFormService = new Mock<ICaseFormService>();
            mockCaseFormService.Setup(service => service.GetCaseFormById(id))
                .ReturnsAsync(GetTestCaseForm());
            var controller = new CaseFormsController(mockCaseFormService.Object);
            #endregion

            #region Act
            var result = await controller.Get(id);
            #endregion

            #region Assert
            Assert.IsType<OkObjectResult>(result.Result);
            #endregion
        }

        /// <summary>
        /// Test the Get(int id) method when case form does not exist.
        /// </summary>
        [Fact]
        public async void GetById_WhenCaseFormDoesNotExist_ReturnsNotFound()
        {
            #region Arrange
            var id = 100;
            var mockCaseFormService = new Mock<ICaseFormService>();
            mockCaseFormService.Setup(service => service.GetCaseFormById(id))
                .ReturnsAsync((CaseFormDto)null);
            var controller = new CaseFormsController(mockCaseFormService.Object);
            #endregion

            #region Act
            var result = await controller.Get(id);
            #endregion

            #region Assert
            Assert.IsType<NotFoundResult>(result.Result);
            #endregion
        }

        private CaseFormDto GetTestCaseForm()
        {
            return new WarrantInDebtFormDto()
            {
                Id = 1, 
                FormType = CaseFormTypes.WARRANT_IN_DEBT,
                CaseNumber = "001", 
                HearingDateTime = DateTime.Parse("01/01/2021 09:00:00")
            };
        }
    }
}
