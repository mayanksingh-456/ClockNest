using System.ComponentModel.DataAnnotations;

namespace ClockNest.Enum
{
    public enum EmployeeFilterStatus
    {
        [Display(Name = "All Employees")]
        AllEmployees,
        [Display(Name = "Left Employees")]
        LeftEmployees,
        [Display(Name = "Clocked In")]
        ClockedIn,
        [Display(Name = "Holiday")]
        Holiday,
        [Display(Name = "Sickness")]
        Sickness,
        [Display(Name = "Absence")]
        Absence,
        [Display(Name = "Lateness")]
        Lateness,
        [Display(Name = "Unauthorised Overtime")]
        UnauthorisedOvertime,
        [Display(Name = "Exceptions")]
        Exceptions,
        [Display(Name = "Clocked")]
        Clocked
    }
}
