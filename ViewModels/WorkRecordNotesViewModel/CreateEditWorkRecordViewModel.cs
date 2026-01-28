using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ClockNest.ViewModels.WorkRecordNotesViewModel
{
    public class CreateEditWorkRecordViewModel
    {
        public CreateEditWorkRecordViewModel()
        {
            ActivitySelect = new List<SelectListItem>();
            CostCentreSelect = new List<SelectListItem>();
        }

        public int? Id { get; set; }

        public int EmployeeShiftId { get; set; }

        [Display(Name = "Start")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End")]
        public DateTime EndTime { get; set; }

        public TimeSpan StartTimeSpan { get; set; }
        public TimeSpan EndTimeSpan { get; set; }

        public List<SelectListItem> ActivitySelect { get; set; }

        [Display(Name = "Activity")]
        public int ActivityId { get; set; }

        public List<SelectListItem> CostCentreSelect { get; set; }

        [Display(Name = "CostCentre")]
        public int CostCentreId { get; set; }

        public string StartTimeDisplay { get; set; }
        public string EndTimeDisplay { get; set; }
        public string ActivityDisplay { get; set; }
        public string CostCentreDisplay { get; set; }
        public string StartDateDisplay { get; set; }
        public string EndDateDisplay { get; set; }
        public string DurationDisplay { get; set; }
    }
}
