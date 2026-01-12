using ClockNest.Common;

namespace ClockNest.Models.SelfService_Model
{
    public class WeeklySummary
    {
        public int? EmployeeId { get; set; }

        public DateTime? ShiftDate { get; set; }

        public int? RosterShiftId { get; set; }

        public string RosterShiftCode { get; set; }

        public int? ScheduledShiftId { get; set; }

        public string ScheduledShiftCode { get; set; }

        public int? EmployeeShiftId { get; set; }

        public int? ShiftId { get; set; }

        public string ShiftCode { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? TotalMins { get; set; }

        public int? ContractedMins { get; set; }

        public int? OvertimeMins { get; set; }

        public bool? IsLate { get; set; }

        public bool IsVerified { get; set; }

        public bool? IsLocked { get; set; }

        public string DisplayLocked { get; }

        public string DisplayVerified { get; }

        public int? AbsenceTypeId { get; set; }

        public string Status { get; set; }

        public int StatusId { get; set; }

        public bool VerifyDisabled { get; set; }

        public string Day => ShiftDate?.DayOfWeek.ToString() ?? string.Empty;

        public double Hours => Math.Round(TimeSpan.FromMinutes(TotalMins.GetValueOrDefault(0)).TotalHours, 2);

        public double Contracted => Math.Round(TimeSpan.FromMinutes(ContractedMins.GetValueOrDefault(0)).TotalHours, 2);

        public string HoursDisplay => Helper.GetHoursMinsString(TotalMins.HasValue ? TotalMins.Value : 0);

        public string ContractedDisplay => Helper.GetHoursMinsString(ContractedMins.HasValue ? ContractedMins.Value : 0);

        public string VarianceDisplay
        {
            get
            {
                string text = string.Empty;
                int num = (TotalMins.HasValue ? TotalMins.Value : 0) - (ContractedMins.HasValue ? ContractedMins.Value : 0);
                if (num > 0)
                {
                    text = "+";
                }

                return text += Helper.GetHoursMinsString(num);
            }
        }

        public string OvertimeDisplay => Helper.GetHoursMinsString(OvertimeMins.HasValue ? OvertimeMins.Value : 0);

        public string StartDateDisplay => ShiftDate?.ToString("d") ?? string.Empty;

        public string StartTimeDisplay => StartTime?.ToString("HH:mm") ?? string.Empty;

        public string EndTimeDisplay => EndTime?.ToString("HH:mm") ?? string.Empty;
        public bool IsAvailableForShift { get; set; }

        public List<WorkRecordNote> WorkRecordNotes { get; set; }

        public int HolidayThresholdForCurrentDay { get; set; }

        public int HolidaysTakenForCurrentDay { get; set; }

        public string HolidayThresholdDetailsDisplay
        {
            get
            {
                string text = HolidayThresholdForCurrentDay - HolidaysTakenForCurrentDay + " day(s) out of " + HolidayThresholdForCurrentDay + " available";
                text = (IsHolidayThresholdAvailable ? text : "n/a");
                return (Day == string.Empty || Day.Equals("Total")) ? string.Empty : text;
            }
        }

        public bool IsHolidayThresholdAvailable { get; set; }

        //Added
        public string Notes { get; set; }
    }
}
