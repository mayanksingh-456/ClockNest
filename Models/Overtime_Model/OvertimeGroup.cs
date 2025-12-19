namespace ClockNest.Models.Overtime_Model
{
    public class OvertimeGroup
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Factor { get; set; }
        public decimal Uplift { get; set; }
        public string UpliftDisplay { get { return Uplift.ToString("C2"); } }
        public int? PayrollCategory { get; set; }
        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
