namespace ClockNest.Models.User_Model
{
    public class UserSelfServiceAccess
    {
        public int UserId { get; set; }
        public int SelfServiceAccessTypeId { get; set; }
        public bool? ReadOnly { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public SelfServiceAccessType SelfServiceAccessType { get; set; }
    }
}
