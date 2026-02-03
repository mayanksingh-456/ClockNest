using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Resources;

namespace ClockNest.ViewModels.WorkRecordNotesViewModel
{
    public class CreateEditWorkRecordExceptionalItemViewModel
    {
        public CreateEditWorkRecordExceptionalItemViewModel()
        {
            ExceptionalItemTypeSelect = new List<SelectListItem>();
            CostCentres = new List<SelectListItem>();
        }

        public int? Id { get; set; }

        public int EmployeeId { get; set; }
        public DateTime ShiftDate { get; set; }

        public int Count { get; set; }

        public decimal Cost { get; set; }

        public List<SelectListItem> ExceptionalItemTypeSelect { get; set; }

        public List<SelectListItem> CostCentres { get; set; }

        [Display(Name = "ItemType")]
        public int ExceptionalItemTypeId { get; set; }

        [Display(Name = "CostCentre")]
        public int CostCentreId { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public TimeSpan StartTimeSpan { get; set; }
        public TimeSpan EndTimeSpan { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
