using challenge.Models;
using challenge.Repositories;
using Microsoft.Extensions.Logging;
using System;

namespace challenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IReportingStructureRepository _reportingStructureRepository;
        private readonly ILogger<ReportingStructureService> _logger;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IReportingStructureRepository ReportingStructureRepository)
        {
            _reportingStructureRepository = ReportingStructureRepository;
            _logger = logger;
        }
        
        public ReportingStructure GetByEmployeeId(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _reportingStructureRepository.GetByEmployeeId(id);
            }

            return null;
        }
        
    }
}
