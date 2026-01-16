using ClockNest.Models.User_Model;

namespace ClockNest.Models.SelfService_Model
{
    public class EmployeeAnnouncement
    {
        public int Id { get; set; }
        public int AnnouncementId { get; set; }
        public int EmployeeId { get; set; }
        public bool Actioned { get; set; }
        public DateTime? ActionedAt { get; set; }
        public bool IsDocument { get; set; }
        public bool IsDocumentRead { get; set; }
        public DateTime? DocumentReadAt { get; set; }
        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateDisplay => CreatedDate.ToString("G");
        public Announcement Announcement { get; set; }
        public string Message
        {
            get
            {
                if (Announcement != null)
                {
                    return Announcement.Message;
                }
                return string.Empty;
            }
        }
        public User User { get; set; }
        public string Username
        {
            get
            {
                if (User != null)
                {
                    return User.UserName;
                }
                return string.Empty;
            }
        }
    }
}
