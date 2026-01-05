namespace ClockNest.Models.User_Model
{
    public class UserMobileAppAccess
    {
        public int UserId { get; set; }

        public int MobileAppAccessTypeId { get; set; }

        public bool? ReadOnly { get; set; }

        public int UpdatedByUserId { get; set; }

        public DateTime UpdatedDate { get; set; }

        public MobileAppAccessType MobileAppAccessType { get; set; }

        public UserMobileAppAccess()
        {
            MobileAppAccessType = new MobileAppAccessType();
        }
    }
}
