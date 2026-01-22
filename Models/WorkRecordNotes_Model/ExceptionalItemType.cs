namespace ClockNest.Models.WorkRecordNotes_Model
{
    public class ExceptionalItemType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public int? PayrollCategory { get; set; }

        public int Sequence { get; set; }

        public int Schedule { get; set; }

        public string WeekDay { get; set; }

        public bool LastDayOfMonth { get; set; }

        public int DayOfMonth { get; set; }

        public int CompanyId { get; set; }

        public int CreatedByUserId { get; set; }

        public int? UpdatedByUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
