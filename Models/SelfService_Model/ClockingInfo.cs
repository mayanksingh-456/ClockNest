using ClockNest.Enum;

namespace ClockNest.Models.SelfService_Model
{
    public class ClockingInfo
    {
        public int EmployeeId { get; set; }

        public DateTime? ClockingTime { get; set; }

        public enumClockingType ClockingType { get; set; }

        public int TerminalCode { get; set; }

        public int ChangeId { get; set; }

        public decimal? Longitude { get; set; }

        public decimal? Latitude { get; set; }

        public enumClockingDevice ClockingDevice { get; set; }
    }
}
