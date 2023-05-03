using System.Collections.Generic;
using InterviewManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManager.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ReportingService _reportingService;

        public ReportController(ReportingService reportingService)
        {
            _reportingService = reportingService;
        }

        [HttpGet("status")]
        public ActionResult<Dictionary<string, int>> GetInterviewsByStatus()
        {
            return _reportingService.GetInterviewsByStatus();
        }

        [HttpGet("position")]
        public ActionResult<Dictionary<string, int>> GetInterviewsByPosition()
        {
            return _reportingService.GetInterviewsByPosition();
        }

        [HttpGet("month")]
        public ActionResult<Dictionary<string, int>> GetInterviewsByMonth()
        {
            return _reportingService.GetInterviewsByMonth();
        }
    }
}
