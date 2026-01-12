namespace ClockNest.Models.SelfService_Model
{
    public class HolidayThresholdCheckDetails
    {
        public HolidayThreshold HolidayThreshold { get; set; }

        public List<AbsenteeRecord> HolidaysBooked { get; set; }
    }
}
