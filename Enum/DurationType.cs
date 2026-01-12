using System.ComponentModel.DataAnnotations;

namespace ClockNest.Enum
{
    public enum DurationType
    {
        [Display(Name = "Full Day")]
        FullDay = 1,

        [Display(Name = "1st Half")]
        FirstHalf = 2,

        [Display(Name = "2nd Half")]
        SecondHalf = 3,

        [Display(Name = "Custom")]
        Custom = 4
    }
}
