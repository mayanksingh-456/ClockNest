namespace ClockNest.Models.Employee_Model
{
    public class AccrualRule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AccrualTypeId { get; set; }
        public decimal IncreaseBy { get; set; }
        public decimal MaximumEntitlement { get; set; }
        public int LengthOfService { get; set; }
        public int AfterYears1 { get; set; }
        public decimal IncreaseBy1 { get; set; }
        public int AfterYears2 { get; set; }
        public decimal IncreaseBy2 { get; set; }
        public int AfterYears3 { get; set; }
        public decimal IncreaseBy3 { get; set; }
        public int AfterYears4 { get; set; }
        public decimal IncreaseBy4 { get; set; }
        public int AfterYears5 { get; set; }
        public decimal IncreaseBy5 { get; set; }
        public int AfterYears6 { get; set; }
        public decimal IncreaseBy6 { get; set; }
        public int AfterYears7 { get; set; }
        public decimal IncreaseBy7 { get; set; }
        public int AfterYears8 { get; set; }
        public decimal IncreaseBy8 { get; set; }
        public int AfterYears9 { get; set; }
        public decimal IncreaseBy9 { get; set; }
        public int AfterYears10 { get; set; }
        public decimal IncreaseBy10 { get; set; }
        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string AccrualTypeName => AccrualTypeId == 1 ? "Custom" : "Standard";
    }
}
