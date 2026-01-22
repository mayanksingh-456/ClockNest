using ClockNest.Models.Employee_Model;

namespace ClockNest.Models.WorkRecordNotes_Model
{
    public class ScheduledShift
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime ShiftDate { get; set; }

        public int ShiftId { get; set; }

        public int? CostCentreId { get; set; }

        public int CompanyId { get; set; }

        public int CreatedByUserId { get; set; }

        public int UpdatedByUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? SelectedFilterTagId { get; set; }

        public Shift Shift { get; set; }
    }
}
