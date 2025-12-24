namespace ClockNest.Models.User_Model
{
    public class UserAccess
    {
        public int UserId { get; set; }

        public int AccessTypeId { get; set; }

        public bool? ReadOnly { get; set; }

        public AccessType AccessType { get; set; }

        public UserAccess()
        {
            AccessType = new AccessType();
        }
    }
}
