namespace ClockNest.Models.SelfService_Model
{
    public class AbsenteeRecord
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime AbsenceDate { get; set; }
        public string AbsenceDateDisplay => AbsenceDate.ToString("d");
        public int AbsenceId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Finish { get; set; }
        public int DurationInMins { get; set; }
        public decimal DurationInDays { get; set; }
        public decimal HourlyRate { get; set; }
        public bool Paid { get; set; }
        public string Notes { get; set; }
        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int Status { get; set; }
        public string StartTimeDisplay => Start?.ToString("HH:mm");
        public string FinishTimeDisplay => Finish?.ToString("HH:mm");
        public string DurationInDaysDisplay => DurationInDays.ToString();
        public string DurationInHoursDisplay => (DurationInMins / 60.0).ToString();
        public string PaidDisplay => Paid == true ? "Yes" : "No";

        public string HolidayRequestedAtDisplay => CreatedDate.ToString("G");

        public string UsernameDisplay { get; set; }

        public string AbsenceName { get; set; }
        public int AbsenceTypeId { get; set; }

        public string EmployeeName { get; set; }
        public string? Forename { get; set; }

        public string? Surname { get; set; }

        public string FullName => Forename + " " + Surname;

        public string PayrollNo { get; set; }

        //public byte?[] Photo { get; set; }
        //public string PhotoBase64
        //{
        //    get
        //    {
        //        if (Photo == null || !Photo.Any(b => b.HasValue))
        //            return null;

        //        var bytes = Photo.Where(b => b.HasValue).Select(b => b.Value).ToArray();
        //        if (bytes.Length == 0)
        //            return null;

        //        return $"data:image/png;base64,{Convert.ToBase64String(bytes)}";
        //    }
        //}

        public string AbsenceNameStatus
        {
            get
            {

                var day = string.Empty;

                if (DurationInDays == 0.5M)
                {
                    day = "1/2 ";
                }
                else if (DurationInDays < 1)
                {
                    day = "(C) ";
                }

                if (Duration == Enum.DurationType.FirstHalf)
                {
                    day += "(AM) ";
                }

                if (Duration == Enum.DurationType.SecondHalf)
                {
                    day += "(PM) ";
                }

                return Status == 10 ? day + AbsenceName + " Requested" : Status == 20 ? day + AbsenceName + " Declined " : day + AbsenceName;
            }
        }

        public string StatusDisplay
        {
            get
            {
                return Status == 10 ? " Requested" : Status == 20 ? " Declined " : " Authorised";
            }
        }

        public string HourlyRateDisplay
        {
            get
            {
                return HourlyRate.ToString("C2");
            }
        }

        public string TotalAmountDisplay
        {
            get
            {
                var amount = 0M;

                if (DurationInMins > 0)
                {
                    amount = ((DurationInMins / 60M) * HourlyRate);
                    return amount.ToString("C2");
                }

                return string.Empty;
            }
        }
        public int Position { get; set; }
        public Enum.DurationType Duration { get; set; }
        public byte?[] Photo { get; set; }

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
