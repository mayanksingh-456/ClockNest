namespace ClockNest.Models.WorkRecordNotes_Model
{
    public class Verified
    {
        public int EmployeeId { get; set; }
        public DateTime ShiftDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
