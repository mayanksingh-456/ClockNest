namespace ClockNest.Models.SelfService_Model
{
    public class AnnualisedHours
    {
        public int Pot { get; set; }
        public decimal TargetHours { get; set; }
        public decimal ActualHours { get; set; }
        public decimal Balance { get { return TargetHours - ActualHours; } }
    }
}
