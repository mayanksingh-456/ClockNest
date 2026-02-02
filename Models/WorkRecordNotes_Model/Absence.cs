

namespace ClockNest.Models.WorkRecordNotes_Model
{
    public class Absence
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public short AbsenceTypeId { get; set; }

        public decimal Limit { get; set; }

        public bool Paid { get; set; }

        public string PaidDisplay => Paid ? "Yes" : "No";

        public bool AllowNegative { get; set; }

        public string AllowNegativeDisplay => AllowNegative ? "Yes" : "No";

        public bool CountsTowardsOvertime { get; set; }

        public string CountsTowardsOvertimeDisplay => CountsTowardsOvertime ? "Yes" : "No";

        public int? CountsTowardsAbsenceId { get; set; }

        public bool CountsTowardsBradfordFactor { get; set; }

        public bool CountsTowardsFlexitime { get; set; }

        public string CountsTowardsFlexitimeDisplay => CountsTowardsFlexitime ? "Yes" : "No";

        public bool AllowRequests { get; set; }

        public string AllowRequestsDisplay => AllowRequests ? "Yes" : "No";

        public int? Position { get; set; }

        public bool Archived { get; set; }

        public string ArchivedDisplay => Archived ? "Yes" : "No";

        public int CompanyId { get; set; }

        public int CreatedByUserId { get; set; }

        public int? UpdatedByUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public AbsenceType AbsenceType { get; set; }

        public AbsenceCascadeAbsenceReason AbsenceCascadeAbsenceReason { get; set; }
    }
}
