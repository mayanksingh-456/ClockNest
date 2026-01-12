namespace ClockNest.Models.SelfService_Model
{
    public class HolidayThreshold
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HolidayMonday { get; set; }
        public int HolidayTuesday { get; set; }
        public int HolidayWednesday { get; set; }
        public int HolidayThursday { get; set; }
        public int HolidayFriday { get; set; }
        public int HolidaySaturday { get; set; }
        public int HolidaySunday { get; set; }
        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
