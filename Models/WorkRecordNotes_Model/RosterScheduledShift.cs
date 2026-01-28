namespace ClockNest.Models.WorkRecordNotes_Model
{
    public class RosterScheduledShift
    {
        public int? EmployeeId { get; set; }

        public DateTime? ShiftDate { get; set; }

        public int? RosterShiftId { get; set; }

        public string RosterShiftCode { get; set; }

        public int? RosterContractedMins { get; set; }

        public bool? RosterRestDay { get; set; }

        public int? RosterVariableShiftId { get; set; }

        public int? ScheduledShiftId { get; set; }

        public string ScheduledShiftCode { get; set; }

        public int? ScheduledContractedMins { get; set; }

        public bool? ScheduledRestDay { get; set; }
    }
}
