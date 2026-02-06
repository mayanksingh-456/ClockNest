namespace ClockNest.Models.Timesheet_Model
{
    public class EmployeeClockingStatus
    {
        public int? EmployeeId { get; set; }

        public DateTime? ShiftDate { get; set; }

        public string? BadgeId { get; set; }

        public string? BadgeId2 { get; set; }

        public string? PayrollNo { get; set; }

        public string? Forename { get; set; }

        public string? Surname { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? EmployeeStatus { get; set; }

        public string? ActivityName { get; set; }

        public string? CostCentreName { get; set; }

        public bool? WorkRecords { get; set; }

        public bool? UnauthorisedOvertime { get; set; }

        public bool? Late { get; set; }

        public string? HolidayName { get; set; }

        public string? SicknessName { get; set; }

        public string? AbsenceName { get; set; }

        public int? ExceptionSeverity { get; set; }

        public DateTime? LeavingDate { get; set; }

        public int Position { get; set; }

        public string FullName => Forename + " " + Surname;

        public string? StatusDisplay { get; set; }

        public int StatusId { get; set; }

        public byte?[] Photo { get; set; }
        public int RecordCount { get; set; }

        public string Notes { get; set; }
        public string PhotoBase64
        {
            get
            {
                if (Photo == null || !Photo.Any(b => b.HasValue))
                    return null;

                var bytes = Photo.Where(b => b.HasValue).Select(b => b.Value).ToArray();
                if (bytes.Length == 0)
                    return null;

                return $"data:image/png;base64,{Convert.ToBase64String(bytes)}";
            }
        }
    }
}
