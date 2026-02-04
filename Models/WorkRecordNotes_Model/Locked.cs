namespace ClockNest.Models.WorkRecordNotes_Model
{
    public class Locked
    {
        public int EmployeeId { get; set; }

        public DateTime ShiftDate { get; set; }

        public int CreatedByUserId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
