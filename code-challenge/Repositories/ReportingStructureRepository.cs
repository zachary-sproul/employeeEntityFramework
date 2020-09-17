using challenge.Data;
using challenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace challenge.Repositories
{
    public class ReportingStructureRepository : IReportingStructureRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IReportingStructureRepository> _logger;

        public ReportingStructureRepository(ILogger<IReportingStructureRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public ReportingStructure GetByEmployeeId(string id)
        {
            ReportingStructure reportingStructure = new ReportingStructure();
            reportingStructure.Employee = GetEmployeeById(id);
            reportingStructure.NumberOfReports = GetNumberOfDirectReports(reportingStructure.Employee);

            return reportingStructure;
        }

        /// <summary>
        /// Retrieve an employee from the context by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Employee GetEmployeeById(string id)
        {
            return _employeeContext.Employees.Include(x => x.DirectReports).SingleOrDefault(e => e.EmployeeId == id);
        }

        /// <summary>
        /// Returns a count of all hires by recursively moving through direct reports.
        /// TODO -> handle circular references
        /// TODO -> handle long runtimes?
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private int GetNumberOfDirectReports(Employee employee)
        {
            int numberOfDirectReports = 0;

            if (employee != null && employee.DirectReports != null)
            {
                numberOfDirectReports = employee.DirectReports.Count;

                foreach (Employee directReportId in employee.DirectReports)
                {
                    Employee directReport = GetEmployeeById(directReportId.EmployeeId);

                    numberOfDirectReports += GetNumberOfDirectReports(directReport);
                }
            }

            return numberOfDirectReports;
        }
    }
}
