namespace ClockNest.Models.Tag_Modal
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public int? BudgetPlanId { get; set; }
        public int? HolidayThresholdId { get; set; }
        public int? PayTypeId { get; set; }
        public int Sequence { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsForVisitorModule { get; set; }
        public bool IsCurrent { get; set; }
        public bool Archived { get; set; }

        public string ArchivedDisplay
        {
            get
            {
                return Archived == true ? "Yes" : "No";
            }
        }
    }
}
