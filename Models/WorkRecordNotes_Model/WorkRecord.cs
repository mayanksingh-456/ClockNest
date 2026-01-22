using ClockNest.Common;
using ClockNest.Models.Employee_Model;

namespace ClockNest.Models.WorkRecordNotes_Model
{
    public class WorkRecord
    {
        public int Id { get; set; }

        public int EmployeeShiftId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? Duration { get; set; }

        public decimal? HourlyRate { get; set; }

        public bool? IsContracted { get; set; }

        public int? ActivityId { get; set; }

        public int? ShiftBreakId { get; set; }

        public int? CostCentreId { get; set; }

        public int? OvertimeGroupId { get; set; }

        public int? OvertimeStatusId { get; set; }

        public int? OvertimeType { get; set; }

        public DateTime? ActualStartTime { get; set; }

        public DateTime? ActualEndTime { get; set; }

        public int? OvertimeReasonId { get; set; }

        public bool IsJobAndFinish { get; set; }

        public int CompanyId { get; set; }

        public int CreatedByUserId { get; set; }

        public int? UpdatedByUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string OvertimeGroupName { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public DateTime ShiftDate { get; set; }

        public Activity Activity { get; set; }

        public CostCentre CostCentre { get; set; }

        public string StartTimeDisplay => StartTime?.ToString("HH:mm");

        public string EndTimeDisplay => EndTime?.ToString("HH:mm");

        public string ActivityDisplay => Activity?.Name;

        public string CostCentreDisplay => CostCentre?.Name;

        public string UsernameDisplay { get; set; }

        public string StartDateDisplay => StartTime?.ToString("d");

        public string EndDateDisplay => EndTime?.ToString("d");

        public string DurationDisplay
        {
            get
            {
                if (!Duration.HasValue)
                {
                    return "";
                }

                return Helper.GetHoursMinsString(Duration.Value);
            }
        }

        public string HourRateDisplay { get; set; }

        public string HourlyRateDisplay
        {
            get
            {
                if (!HourlyRate.HasValue)
                {
                    return "";
                }

                if (HourlyRate.HasValue)
                {
                    return HourlyRate.Value.ToString("C2");
                }

                return "";
            }
        }

        public string TotalAmountDisplay
        {
            get
            {
                if (!HourlyRate.HasValue || !Duration.HasValue)
                {
                    return "";
                }

                decimal num = default(decimal);
                if (Duration.HasValue && Duration.Value > 0)
                {
                    num = (decimal)Duration.Value / 60m * HourlyRate.Value;
                }

                return num.ToString("C2");
            }
        }

        public bool IsSplitBreak { get; set; }
    }
}
