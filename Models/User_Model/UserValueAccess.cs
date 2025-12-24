namespace ClockNest.Models.User_Model
{
    public class UserValueAccess
    {
        public int UserId { get; set; }

        public int ValueAccessTypeId { get; set; }

        public int? UpdatedByUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual ValueAccessType ValueAccessType { get; set; }

        public UserValueAccess()
        {
            ValueAccessType = new ValueAccessType();
        }
    }
}
