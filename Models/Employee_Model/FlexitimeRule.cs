namespace ClockNest.Models.Employee_Model
{
    public class FlexitimeRule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public int? Weekday { get; set; }
        public int? OvertimeRule { get; set; }
        public int Days1 { get; set; }
        public int? OvertimeRule1 { get; set; }
        public int Days2 { get; set; }
        public int? OvertimeRule2 { get; set; }
        public int Days3 { get; set; }
        public int? OvertimeRule3 { get; set; }
        public int Days4 { get; set; }
        public int? OvertimeRule4 { get; set; }
        public int Days5 { get; set; }
        public int? OvertimeRule5 { get; set; }
        public int Days6 { get; set; }
        public int? OvertimeRule6 { get; set; }
        public int MaxCredit { get; set; }
        public int MaxDebit { get; set; }
        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? RuleType { get; set; }
    }
}
