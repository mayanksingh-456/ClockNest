namespace ClockNest.Models.Employee_Model
{
    public class CostCentre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal HourlyRate { get; set; }
        public int? TerminalCode { get; set; }
        public int? CostCentreGroupId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal Ringfence { get; set; }

        public bool OutOfBounds { get; set; }

        public string OutOfBoundsDisplay
        {
            get
            {
                return OutOfBounds == true ? "Yes" : "No";
            }
        }

        public int? RosterId { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
        public string Custom4 { get; set; }
        public string Custom5 { get; set; }
        public bool Archived { get; set; }

        public string ArchivedDisplay
        {
            get
            {
                return Archived == true ? "Yes" : "No";
            }
        }

        public string HourlyRateDisplay
        {
            get
            {
                return HourlyRate.ToString("C2");
            }
        }

        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
