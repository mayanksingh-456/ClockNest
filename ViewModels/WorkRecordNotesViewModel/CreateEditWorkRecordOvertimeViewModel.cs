using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ClockNest.ViewModels.WorkRecordNotesViewModel
{
    public class CreateEditWorkRecordOvertimeViewModel
    {
        public CreateEditWorkRecordOvertimeViewModel()
        {
            OvertimeGroups = new List<SelectListItem>();
        }

        public int Id { get; set; }
        public int? EmployeeShiftId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime ShiftDate { get; set; }
        public TimeSpan Duration { get; set; }

        [Display(Name = "OvertimeGroup")]
        public int OvertimeGroupId { get; set; }

        [Display(Name = "OvertimeGroups")]
        public List<SelectListItem> OvertimeGroups;

        public int OvertimeStatusId { get; set; }
    }
}
