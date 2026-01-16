namespace ClockNest.Models.SelfService_Model
{
    public class ShapePayment
    {
        public string ShapePaymentId { get; set; }
        public string ShapePayrunId { get; set; }
        public int EmployeeId { get; set; }
        public decimal Amount { get; set; }
        public string AmountDisplay => Amount.ToString("C2");
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateDisplay => CreatedDate.ToString("d");
    }
}
