using System.Collections.Generic;
using System.Threading.Tasks;
using EZSubmitApp.Core.DTOs;
using EZSubmitApp.Core.Entities;
using EZSubmitApp.Core.Interfaces;
using EZSubmitApp.Core.Paging;
using EZSubmitApp.Core.ResourceParameters;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EZSubmitApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseFormsController : ControllerBase
    {
        private readonly ICaseFormService _caseFormService;

        public CaseFormsController(
            ICaseFormService caseFormService)
        {
            _caseFormService = caseFormService;
        }

        // GET: api/CaseForms
        // GET: api/CaseForms/?pageIndex=0&pageSize=10
        // GET: api/CaseForms/?pageIndex=0&pageSize=10&sortColumn=caseNumber&
        //  sortOrder=asc
        [HttpGet]
        [HttpHead]
        public async Task<ActionResult<IPagedList<CaseFormDto>>> Get([FromQuery] CaseFormParameters requestParams)
        {
            var caseForms = await _caseFormService.SearchCaseForms(requestParams);
            return Ok(caseForms);
        }

        // GET: api/CaseForms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CaseFormDto>> Get(int id)
        {
            var caseForm = await _caseFormService.GetCaseFormById(id);
            if (caseForm == null)
            {
                return NotFound();
            }

            return Ok(caseForm);
        }

        [HttpGet("{id}/docx")]
        public async Task<IActionResult> GetDocx(int id)
        {
            var bytes = await _caseFormService.GetCaseFormAsDocx(id);
            string mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            //string mimeType = "application/vnd.ms-word";
            return new FileContentResult(bytes, mimeType)
            {
                FileDownloadName = string.Format("{0}_{1}.docx", "Generated_Warrant_In_Debt", id)
            };
        }

        // POST: api/CaseForms
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CaseFormDto>> Post(CaseFormForCreationDto caseForm)
        {
            var newCaseFormDto = await _caseFormService.CreateCaseForm(caseForm);
            return CreatedAtAction("Get", new { id = newCaseFormDto.Id }, newCaseFormDto);
        }

        // PUT: api/CaseForms/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CaseFormForUpdateDto caseForm)
        {
            await _caseFormService.UpdateCaseForm(id, caseForm);
            return Ok();
        }

        // DELETE: api/CaseForms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _caseFormService.DeleteCaseFormById(id);
            return NoContent();
        }

        [HttpPost]
        [Route("IsDupeCaseForm")]
        public async Task<bool> IsDupeCaseForm(CaseFormDto caseForm)
        {
            // Nothing currently implemented here ... just return false
            return false;
        }

        [HttpPost]
        [Route("IsDupeField")]
        public async Task<bool> IsDupeField(int id, string fieldName, string fieldValue)
        {
            return await _caseFormService.IsDupeField(id, fieldName, fieldValue);
        }
    }
}
