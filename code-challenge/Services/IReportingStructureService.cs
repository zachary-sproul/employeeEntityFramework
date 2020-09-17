using challenge.Models;
using System;

namespace challenge.Services
{
    public interface IReportingStructureService
    {
        ReportingStructure GetByEmployeeId(String id);
    }
}
