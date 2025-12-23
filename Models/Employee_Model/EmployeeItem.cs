namespace ClockNest.Models.Employee_Model
{
    public class EmployeeItem
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int CategoryDetailId { get; set; }
        public string Provider { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Cost { get; set; }
        public int Importance { get; set; }
        public int? GenericDetailId { get; set; }
        public int RecurringId { get; set; }
        public bool IsCompleted { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int EmployeeDocumentId { get; set; }

        public EmployeeDocument EmployeeDocument { get; set; }

        public CategoryDetail CategoryDetail { get; set; }

        public GenericDetail GenericDetail { get; set; }
    }
}
