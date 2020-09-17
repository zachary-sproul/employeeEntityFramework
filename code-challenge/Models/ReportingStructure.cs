using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    /// <summary>
    /// Create a new type, ReportingStructure, that has two properties: employee and numberOfReports.
    /// </summary>
    public class ReportingStructure
    {
        public Employee Employee { get; set; } //Why employee instead of employee id? Following the naming style (public properties are cap case) from the Employees class.
        public int NumberOfReports { get; set; } //We're in trouble if someone has more than ~2 billion direct reports.
    }
}
