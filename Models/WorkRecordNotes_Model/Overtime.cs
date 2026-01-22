using ClockNest.Common;

namespace ClockNest.Models.WorkRecordNotes_Model
{
    public class Overtime
    {
        public int Id { get; set; }
        public int? EmployeeShiftId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? WeekEndingDate { get; set; }
        public bool IsDailyOvertime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Duration { get; set; }
        public decimal? HourlyRate { get; set; }
        public int? OvertimeGroupId { get; set; }

        public string HourlyRateDisplay
        {
            get
            {
                if (HourlyRate == null)
                {
                    return "";
                }
                else
                {
                    if (HourlyRate.HasValue)
                    {
                        return HourlyRate.Value.ToString("C2");
                    }

                    return "";
                }
            }
        }

        public string TotalAmountDisplay
        {
            get
            {
                var amount = 0M;

                if (HourlyRate == null || Duration == null)
                {
                    return "";
                }

                if (Duration.HasValue && Duration.Value > 0)
                {
                    amount = ((Duration.Value / 60M) * HourlyRate.Value);
                }

                return amount.ToString("C2");
            }
        }

        public string Dummy
        {
            get
            {
                return string.Empty;
            }
        }

        public string ActivityName { get; set; }
        public string CostCentreName { get; set; }
        public string OvertimeGroupName { get; set; }
        public string EmployeeName { get; set; }
        public int? OvertimeStatusId { get; set; }
        public string StartTimeDisplay => StartTime?.ToString("HH:mm");
        public string EndTimeDisplay => EndTime?.ToString("HH:mm");
        public string OvertimeStatusDisplay
        {
            get
            {
                switch (OvertimeStatusId.Value)
                {
                    case 0:
                        return "Pending";
                    case 10:
                        return "Authorised";
                    case 20:
                        return "Declined (paid as Basic)";
                    case 30:
                        return "Toil";
                    case 40:
                        return "Declined (unpaid)";
                }
                return "Pending";
            }
            set
            {
            }
        }

        public string OvertimeReason { get; set; }

        public string DurationDisplay
        {
            get
            {
                if (Duration == null)
                {
                    return "";
                }
                else
                {
                    string durationDisplay = Helper.GetHoursMinsString(Duration.Value);
                    return durationDisplay;
                }
            }
        }

    }
}
