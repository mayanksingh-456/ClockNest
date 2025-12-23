using ClockNest.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ClockNest.ViewModels.EmployeeViewModel
{
    public class CreateEditEmployeeAdditionalInfoViewModel
    {
        public CreateEditEmployeeAdditionalInfoViewModel()
        {
            Employees = new List<SelectListItem>();
            FlexitimeRules = new List<SelectListItem>();
            NationalityTypes = new List<SelectListItem>();
            EthnicTypes = new List<SelectListItem>();
            ReligionTypes = new List<SelectListItem>();
            TimeZones = new List<SelectListItem>();

            foreach (TimeZoneInfo tz in TimeZoneInfo.GetSystemTimeZones().OrderBy(x => x.DisplayName))
            {
                TimeZones.Add(new SelectListItem { Text = tz.DisplayName, Value = tz.Id });
            }

            TimeZones.Insert(0, new SelectListItem { Value = "", Text = "" });

        }

        [Display(Name = "FirstAider")]
        public bool? FirstAider { get; set; }

        [Display(Name = "FireMarshall")]
        public bool? FireMarshall { get; set; }

        public bool? Supervisor { get; set; }

        public bool NoNullFirstAider
        {
            get { return FirstAider ?? false; }
            set { FirstAider = value; }
        }

        public bool NoNullFireMarshall
        {
            get { return FireMarshall ?? false; }
            set { FireMarshall = value; }
        }

        public bool NoNullSupervisor
        {
            get { return Supervisor ?? false; }
            set { Supervisor = value; }
        }

        [Display(Name = "JoinDate")]
        public DateTime? JoinDate { get; set; }

        [Display(Name = "LeavingDate")]
        public DateTime? LeavingDate { get; set; }

        [Display(Name = "ReasonForLeaving")]
        public string ReasonForLeaving { get; set; }

        [Display(Name = "JobTitle")]
        public string JobTitle { get; set; }

        [Display(Name = "Manager")]
        public int? ManagerId { get; set; }

        [Display(Name = "FlexitimeRule")]
        public int? FlexitimeRuleId { get; set; }

        [Display(Name = "PinOnly")]
        public bool PinOnly { get; set; }

        public List<SelectListItem> Employees { get; set; }
        public List<SelectListItem> FlexitimeRules { get; set; }



        [Display(Name = "Nationality")]
        public int? NationalityTypeId { get; set; }

        [Display(Name = "Ethnicity")]
        public int? EthnicTypeId { get; set; }

        [Display(Name = "Religion")]
        public int? ReligionTypeId { get; set; }

        public string LengthOfService => JoinDate.HasValue ? DateTime.Now.Subtract(JoinDate.Value).GetYears() + "yr(s) " +
                                            (DateTime.Now.Subtract(JoinDate.Value).GetMonths() % 12) + "mo(s)." : string.Empty;

        public List<SelectListItem> NationalityTypes { get; set; }
        public List<SelectListItem> EthnicTypes { get; set; }
        public List<SelectListItem> ReligionTypes { get; set; }

        public string JoinDateDisplay => JoinDate?.ToString("d");

        [Display(Name = "TimeZone")]
        public string TimeZone { get; set; }

        public List<SelectListItem> TimeZones { get; set; }
    }
}
