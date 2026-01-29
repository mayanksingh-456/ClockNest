using ClockNest.Models.WorkRecordNotes_Model;

namespace ClockNest.ViewModels.Parameter_List
{
    public class ParameterList
    {
        public int CompanyId { get; set; }
        public int EmployeeId { get; set; }
        public int TagId { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }

        public int RoleTypeId { get; set; }
        public int EmployeeShiftId { get; set; }

        public DateTime Date { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool Flag { get; set; }
        public int ShiftId { get; set; }
        public bool CreateDefaultDay { get; set; }

        public EmployeeShift EmployeeShift { get; set; }
        public List<EmployeeShift> EmployeeShifts { get; set; }
        public List<Overtime> Overtime { get; set; }
    }
}
