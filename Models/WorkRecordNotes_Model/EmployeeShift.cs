using ClockNest.Models.Employee_Model;

namespace ClockNest.Models.WorkRecordNotes_Model
{
    public class EmployeeShift
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime ShiftDate { get; set; }
        public int ShiftId { get; set; }
        public bool? Late { get; set; }
        public string Notes { get; set; }
        public int? OvertimeReasonId { get; set; }
        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string ShiftName { get; set; }

        public Shift Shift { get; set; }

        public string ShiftCode => Shift?.Code;

        public string EmployeeName { get; set; }
    }
}
