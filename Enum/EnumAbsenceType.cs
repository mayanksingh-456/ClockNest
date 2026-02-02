using System.ComponentModel.DataAnnotations;

namespace ClockNest.Enum
{
    public enum EnumAbsenceType
    {
        [Display(Name = "Holiday")]
        Holiday = 1,
        [Display(Name = "Sickness")]
        Sickness,
        [Display(Name = "Absence")]
        Absence
    }
}
