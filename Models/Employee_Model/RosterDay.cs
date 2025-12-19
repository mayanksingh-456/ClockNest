namespace ClockNest.Models.Employee_Model
{
    public class RosterDay
    {
        public RosterDay()
        {
            Shift = new Shift();
            VariableShift = new VariableShift();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int RosterId { get; set; }
        public int? ShiftId { get; set; }
        public int? VariableShiftId { get; set; }
        public int Sequence { get; set; }
        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Shift Shift { get; set; }
        public VariableShift VariableShift { get; set; }
    }
}
