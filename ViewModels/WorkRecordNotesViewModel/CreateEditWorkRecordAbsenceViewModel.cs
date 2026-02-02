using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ClockNest.ViewModels.WorkRecordNotesViewModel
{
    public class CreateEditWorkRecordAbsenceViewModel
    {
        public CreateEditWorkRecordAbsenceViewModel()
        {
            CategorySelect = new List<SelectListItem>();
            TypeSelect = new List<SelectListItem>();
            DurationSelect = new List<SelectListItem>();
        }

        public int? Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime AbsenceDate { get; set; }

        public List<SelectListItem> CategorySelect { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public List<SelectListItem> TypeSelect { get; set; }

        [Display(Name = "Type")]
        public int TypeId { get; set; }

        public List<SelectListItem> DurationSelect { get; set; }

        [Display(Name = "Duration")]
        public int DurationId { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public TimeSpan StartTimeSpan { get; set; }
        public TimeSpan EndTimeSpan { get; set; }

        //Added
        public bool HideAbsenceDate { get; set; } = false;
    }
}
