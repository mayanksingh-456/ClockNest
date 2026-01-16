using ClockNest.Models.Employee_Model;
using ClockNest.Models.User_Model;

namespace ClockNest.Models.SelfService_Model
{
    public class EmployeeShiftNotification
    {
        public int Id { get; set; }

        public int ShiftNotificationId { get; set; }

        public int EmployeeId { get; set; }

        public int Status { get; set; }

        public int CompanyId { get; set; }

        public int CreatedByUserId { get; set; }

        public int? UpdatedByUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

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

        public ShiftNotification ShiftNotification { get; set; }

        public string ShiftDateDisplay
        {
            get
            {
                if (ShiftNotification != null)
                {
                    return ShiftNotification.ShiftDate.ToString("d");
                }

                return string.Empty;
            }
        }

        public string ShiftCode
        {
            get
            {
                if (ShiftNotification.Shift != null)
                {
                    return ShiftNotification.Shift.Code;
                }

                return string.Empty;
            }
        }

        public string ShiftName
        {
            get
            {
                if (ShiftNotification.Shift != null)
                {
                    return ShiftNotification.Shift.Name;
                }

                return string.Empty;
            }
        }

        public Employee Employee { get; set; }

        public string FullName
        {
            get
            {
                if (Employee != null)
                {
                    return Employee.Forename + " " + Employee.Surname;
                }

                return string.Empty;
            }
        }

        public string PayrollNo
        {
            get
            {
                if (Employee != null)
                {
                    return Employee.PayRollNo;
                }

                return string.Empty;
            }
        }

        public decimal? HourlyRate
        {
            get
            {
                if (Employee != null)
                {
                    return Employee.HourlyRate;
                }

                return 0m;
            }
        }

        public string StatusDisplay => Status switch
        {
            1 => "No",
            2 => "Yes",
            3 => "Maybe",
            _ => "Pending",
        };
    }
}
