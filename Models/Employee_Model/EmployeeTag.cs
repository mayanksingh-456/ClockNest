namespace ClockNest.Models.Employee_Model
{
    public class EmployeeTag
    {
        public int EmployeeId { get; set; }
        public int TagId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
