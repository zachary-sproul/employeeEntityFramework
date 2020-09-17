using System;
using System.ComponentModel.DataAnnotations;

namespace challenge.Models
{
    /// <summary>
    /// Create a new type, Compensation. A Compensation has the following fields: employee, salary, and effectiveDate.
    /// </summary>
    public class Compensation
    {
        public Employee Employee { get; set; } //Why employee instead of employee id? Following the naming style (public properties are cap case) from the Employees class.
        public decimal Salary { get; set; } //Decimal to minimize rounding errors.
        public DateTime EffectiveDate { get; set; }
    }
}
