
using ClockNest.Models.Overtime_Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ClockNest.Models.Employee_Model
{
    public class Roster
    {
        public Roster()
        {
            RosterDay = new List<RosterDay>();
            Shifts = new List<Shift>();
            VariableShifts = new List<VariableShift>();
            OvertimeGroups = new List<OvertimeGroup>();
        }

        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public string StartDateDisplay => StartDate.ToString("d");
        public int? Duration { get; set; }
        public List<SelectListItem>? DurationValues { get; set; }
        public int? OvertimeRuleId { get; set; }
        public bool Archived { get; set; }
        public string ArchivedDisplay { get { return Archived == true ? "Yes" : "No"; } }
        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<RosterDay> RosterDay { get; set; }
        public List<RosterDay> RosterDays { get; set; }
        public List<Shift> Shifts { get; set; }
        public List<VariableShift> VariableShifts { get; set; }
        public List<OvertimeGroup> OvertimeGroups { get; set; }
        public int? SelectedShiftId { get; set; }         // For Shift dropdown
        public int? SelectedVariableShiftId { get; set; }
    }
}
