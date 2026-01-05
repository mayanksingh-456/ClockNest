namespace ClockNest.Models.User_Model
{
    public class UserTag
    {
        public int UserId { get; set; }
        public int TagId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
