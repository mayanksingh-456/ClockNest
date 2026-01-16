using ClockNest.Models.User_Model;
using System.ComponentModel.DataAnnotations;

namespace ClockNest.Models.SelfService_Model
{
    public class Announcement
    {
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        public byte[] Document { get; set; }

        public string Filename { get; set; }

        public string ContentType { get; set; }

        public int CompanyId { get; set; }

        public int CreatedByUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<int> EmployeeIds { get; set; }

        public string CreatedDateDisplay => CreatedDate.ToString("G");

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
