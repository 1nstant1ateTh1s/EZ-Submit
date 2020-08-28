using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;
using EZSubmitApp.Core.Constants;
using EZSubmitApp.Core.Entities;
using EZSubmitApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EZSubmitApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseFormsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly EZSubmitDbContext _context;

        public CaseFormsController(
            ILogger<CaseFormsController> logger,
            EZSubmitDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/CaseForms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseForm>>> Get()
        {
            _logger.LogInformation(LoggingEvents.ListItems, "Listing all case forms from database.");

            return await _context.CaseForms.ToListAsync();
        }

        // GET: api/CaseForms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CaseForm>> Get(int id)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "Getting case form {Id}", id);

            var caseForm = await _context.CaseForms.FindAsync(id);
            if (caseForm == null)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, "Case form {Id} NOT FOUND", id);
                return NotFound();
            }

            return caseForm;
        }

        // POST: api/CaseForms
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CaseForm>> Post(CaseForm caseForm)
        {
            _context.CaseForms.Add(caseForm);
            await _context.SaveChangesAsync();
            _logger.LogInformation(LoggingEvents.InsertItem, "Case form {Id} created", caseForm.Id);

            return CreatedAtAction("Get", new { id = caseForm.Id }, caseForm);
        }

        // PUT: api/CaseForms/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CaseForm caseForm)
        {
            if (id != caseForm.Id)
            {
                return BadRequest();
            }

            WarrantInDebtForm form = caseForm as WarrantInDebtForm;
            //_context.Entry(form).State = EntityState.Modified;

            var caseFormFromRepo = await _context.CaseForms.FindAsync(id) as WarrantInDebtForm;
            if (caseFormFromRepo == null)
            {
                _logger.LogWarning(LoggingEvents.UpdateItemNotFound, "UPDATE FAILED: Case form {Id} NOT FOUND", id);
                return NotFound();
            }

            // TODO: Updates only work when I manually map properties after retrieving existing entity from DB.
            // TODO: Will replace with DTO and AutoMapper mapping profile
            caseFormFromRepo.CaseNumber = form.CaseNumber;
            caseFormFromRepo.AttorneyFees = form.AttorneyFees;
            caseFormFromRepo.FilingCost = form.FilingCost;
            caseFormFromRepo.Interest = form.Interest;
            caseFormFromRepo.SubmittedById = form.SubmittedById;
            // ...

            //caseForm.FormType = "WD";
            //_context.Entry(caseForm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation(LoggingEvents.UpdateItem, "Case form {Id} Updated", caseForm.Id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseFormExists(id))
                {
                    _logger.LogWarning(LoggingEvents.UpdateItemNotFound, "UPDATE FAILED: Case form {Id} no longer exists", caseForm.Id);
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            //catch (Exception ex)
            //{
            //    var e = ex;
            //    throw ex;
            //}

            return NoContent();
        }

        // DELETE: api/CaseForms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CaseForm>> Delete(int id)
        {
            var caseForm = await _context.CaseForms.FindAsync(id);
            if (caseForm == null)
            {
                _logger.LogWarning(LoggingEvents.DeleteItemNotFound, "Case form {Id} NOT FOUND", id);
                return NotFound();
            }

            _context.CaseForms.Remove(caseForm);
            await _context.SaveChangesAsync();
            _logger.LogInformation(LoggingEvents.DeleteItem, "Case form {Id} deleted", id);

            return caseForm;
        }

        private bool CaseFormExists(int id)
        {
            return _context.CaseForms.Any(e => e.Id == id);
        }
    }
}
