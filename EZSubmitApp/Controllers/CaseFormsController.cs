using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EZSubmitApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EZSubmitApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseFormsController : ControllerBase
    {
        private readonly ILogger _logger;

        public CaseFormsController(
            ILogger<CaseFormsController> logger)
        {
            _logger = logger;
        }

        // GET: api/<CaseFormsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CaseFormsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CaseFormsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CaseFormsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CaseFormsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
