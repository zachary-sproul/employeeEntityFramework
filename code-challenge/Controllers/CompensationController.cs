using challenge.Models;
using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService CompensationService)
        {
            _logger = logger;
            _compensationService = CompensationService;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received Compensation create request for '{compensation.Employee.EmployeeId}'");

            _compensationService.Create(compensation);

            return CreatedAtRoute("GetCompensationByEmployeeId", new { id = compensation.Employee.EmployeeId }, compensation);
        }

        [HttpGet("{id}", Name = "GetCompensationByEmployeeId")]
        public IActionResult GetCompensationByEmployeeId(string id)
        {
            _logger.LogDebug($"Received Compensation get request for '{id}'");

            var compensation = _compensationService.GetByEmployeeId(id);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceCompensation(string id, [FromBody]Compensation newCompensation)
        {
            _logger.LogDebug($"Recieved Compensation update request for '{id}'");

            var existingCompensation = _compensationService.GetByEmployeeId(id);
            if (existingCompensation == null)
                return NotFound();

            _compensationService.Replace(existingCompensation, newCompensation);

            return Ok(newCompensation);
        }
    }
}
