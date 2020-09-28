using System.Collections.Generic;
using System.Threading.Tasks;
using EZSubmitApp.Core.DTOs;
using EZSubmitApp.Core.Entities;
using EZSubmitApp.Core.Interfaces;
using EZSubmitApp.Core.Paging;
using EZSubmitApp.Requests;
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
        [HttpGet]
        [HttpHead]
        //public async Task<ActionResult<IEnumerable<CaseForm>>> Get([FromQuery] PageSearchRequest request)
        public async Task<ActionResult<IEnumerable<CaseForm>>> Get([FromQuery] PageSearchArgs request)
        {
            var pagingArgs = request;

            var caseForms = await _caseFormService.GetCaseForms(pagingArgs);
            return Ok(caseForms);
        }

        // GET: api/CaseForms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CaseForm>> Get(int id)
        {
            var caseForm = await _caseFormService.GetCaseFormById(id);
            if (caseForm == null)
            {
                return NotFound();
            }

            return Ok(caseForm);
        }

        // TODO: Add a GetByUsername() method

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
            //try
            //{
            //    await _context.SaveChangesAsync();
            //    _logger.LogInformation(LoggingEvents.UpdateItem, "Case form {Id} Updated", caseForm.Id);
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!CaseFormExists(id))
            //    {
            //        _logger.LogWarning(LoggingEvents.UpdateItemNotFound, "UPDATE FAILED: Case form {Id} no longer exists", caseForm.Id);
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();

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
    }
}
