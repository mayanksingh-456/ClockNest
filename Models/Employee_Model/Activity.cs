using System.Globalization;

namespace ClockNest.Models.Employee_Model
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal HourlyRate { get; set; }
        public bool IsBreak { get; set; }
        public int? TerminalCode { get; set; }
        public bool IsJobAndFinish { get; set; }
        public bool Offsite { get; set; }
        public bool DoesNotCountTowardsOvertime { get; set; }
        public bool Archived { get; set; }

        public string ArchivedDisplay
        {
            get
            {
                return Archived == true ? "Yes" : "No";
            }
        }

        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string HourlyRateDisplay { get { return HourlyRate.ToString("C2", CultureInfo.CreateSpecificCulture("en-GB")); } }
    }
}
