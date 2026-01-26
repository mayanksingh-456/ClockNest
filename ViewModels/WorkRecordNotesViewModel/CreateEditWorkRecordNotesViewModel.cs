using ClockNest.Models.WorkRecordNotes_Model;

namespace ClockNest.ViewModels.WorkRecordNotesViewModel
{
    public class CreateEditWorkRecordNotesViewModel
    {
        public string Notes { get; set; }
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime ShiftDate { get; set; }

        public WorkRecordAllDetails WorkDetails { get; set; }
    }
}
