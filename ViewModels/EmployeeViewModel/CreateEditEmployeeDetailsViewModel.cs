using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ClockNest.ViewModels.EmployeeViewModel
{
    public class CreateEditEmployeeDetailsViewModel
    {
        public CreateEditEmployeeDetailsViewModel()
        {
            CreateEditEmployeePhotoViewModel = new CreateEditEmployeePhotoViewModel();
            PersonTypes = new List<SelectListItem>();

            GenderTypes = new List<SelectListItem>();
            GenderTypes.Add(new SelectListItem { Text = "Male", Value = "M" });
            GenderTypes.Add(new SelectListItem { Text = "Female", Value = "F" });

            Titles = new List<SelectListItem>();
            Rosters = new List<SelectListItem>();
            Activities = new List<SelectListItem>();
            CostCentres = new List<SelectListItem>();
            TerminalGroups = new List<SelectListItem>();
            GTTerminalGroups = new List<SelectListItem>();
            AccessGroups = new List<SelectListItem>();
        }

        public int Id { get; set; }

        [Display(Name = "PersonType")]
        public int PersonTypeId { get; set; }

        [Display(Name = "BadgeId")]
        [Required]
        public string BadgeId { get; set; }

        [Display(Name = "BadgeId2")]
        public string BadgeId2 { get; set; }

        [Display(Name = "Forename")]
        [Required]
        public string Forename { get; set; }

        [Display(Name = "Surname")]
        [Required]
        public string Surname { get; set; }

        [Display(Name = "Title")]
        public int? TitleId { get; set; }

        [Display(Name = "DateOfBirth")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Roster")]
        public int? RosterId { get; set; }

        [Display(Name = "Activity")]
        public int? ActivityId { get; set; }

        [Display(Name = "CostCentre")]
        public int? CostCentreId { get; set; }

        [Display(Name = "TerminalGroup")]
        public int? TerminalGroupId { get; set; }

        [Display(Name = "GTITTerminalGroup")]
        public int? GTTerminalGroupId { get; set; }

        [Display(Name = "AccessGroup")]
        public int? AccessGroupId { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        public int Rotation { get; set; }

        public string FullName => Forename + " " + Surname;

        public List<SelectListItem> PersonTypes { get; set; }

        public List<SelectListItem> GenderTypes { get; set; }

        public List<SelectListItem> Titles { get; set; }

        public List<SelectListItem> Rosters { get; set; }

        public List<SelectListItem> Activities { get; set; }

        public List<SelectListItem> CostCentres { get; set; }

        public List<SelectListItem> TerminalGroups { get; set; }

        public List<SelectListItem> GTTerminalGroups { get; set; }

        public List<SelectListItem> AccessGroups { get; set; }

        public CreateEditEmployeePhotoViewModel CreateEditEmployeePhotoViewModel { get; set; }
    }
}
