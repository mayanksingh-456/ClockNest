using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ClockNest.ViewModels.WorkRecordNotesViewModel
{
    public class CreateEditWorkRecordShiftViewModel
    {
        public CreateEditWorkRecordShiftViewModel()
        {
            ShiftsSelect = new List<SelectListItem>();
        }

        public List<SelectListItem> ShiftsSelect { get; set; }

        [Display(Name = "Shift")]
        public int ShiftId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime ShiftDate { get; set; }

        [Display(Name = "AutoGenerateWorkRecord")]
        public bool AutoGenerateWorkRecord { get; set; }
    }
}
