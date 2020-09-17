using challenge.Models;

namespace challenge.Services
{
    public interface ICompensationService
    {
        Compensation GetByEmployeeId(string id);
        Compensation Create(Compensation compensation);
        Compensation Replace(Compensation existingCompensation, Compensation newCompensation);
    }
}
