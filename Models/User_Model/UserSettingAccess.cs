namespace ClockNest.Models.User_Model
{
    public class UserSettingAccess
    {
        public int UserId { get; set; }

        public int SettingAccessTypeId { get; set; }

        public bool? ReadOnly { get; set; }

        public int? UpdatedByUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public SettingAccessType SettingAccessType { get; set; }

        public UserSettingAccess()
        {
            SettingAccessType = new SettingAccessType();
        }
    }
}
