using ClockNest.Models.Employee_Model;

namespace ClockNest.Models.SelfService_Model
{
    public class ShiftNotification
    {
        public int Id { get; set; }
        public int ShiftId { get; set; }
        public DateTime ShiftDate { get; set; }
        public int? EmployeeShiftNotificationId { get; set; }
        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Shift Shift { get; set; }
        public string Details
        {
            get
            {
                if (Shift != null)
                {
                    return ShiftDate.ToString("d") + " - " + Shift.Name;
                }
                return string.Empty;
            }
        }

    }
}
