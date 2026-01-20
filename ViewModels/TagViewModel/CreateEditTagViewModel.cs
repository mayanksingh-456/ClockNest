using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Resources;

namespace ClockNest.ViewModels.TagViewModel
{
    public class CreateEditTagViewModel
    {
        public CreateEditTagViewModel()
        {
            BudgetPlans = new List<SelectListItem>();
            HolidayThresholds = new List<SelectListItem>();
        }

        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "BudgetPlan")]
        public int? BudgetPlanId { get; set; }

        [Display(Name = "HolidayThreshold")]
        public int? HolidayThresholdId { get; set; }

        public int? PayTypeId { get; set; }

        public int Sequence { get; set; }

        public List<SelectListItem> BudgetPlans { get; set; }

        public List<SelectListItem> HolidayThresholds { get; set; }

        [Display(Name = "IsForVisitorModule")]
        public bool IsForVisitorModule { get; set; }

        public bool Archived { get; set; }
        public bool AssignShiftsToTags { get; set; }
    }
}
