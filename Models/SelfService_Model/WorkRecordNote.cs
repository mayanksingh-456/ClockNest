namespace ClockNest.Models.SelfService_Model
{
    public class WorkRecordNote
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime ShiftDate { get; set; }
        public string Notes { get; set; }
        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
