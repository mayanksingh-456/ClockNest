using System.ComponentModel.DataAnnotations;

namespace ClockNest.ViewModels.EmployeeViewModel
{
    public class CreateEditEmployeeNotesViewModel
    {

        [Display(Name = "Notes")]
        public string Notes { get; set; }
    }
}
