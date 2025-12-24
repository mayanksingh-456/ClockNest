namespace ClockNest.Models.User_Model
{
    public class Customer
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public int UserId { get; set; }

        public string CustomerStripeId { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
