using challenge.Models;

namespace challenge.Repositories
{
    public interface IReportingStructureRepository
    {
        ReportingStructure GetByEmployeeId(string id);
    }
}