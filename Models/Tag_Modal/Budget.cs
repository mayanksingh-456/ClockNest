namespace ClockNest.Models.Tag_Modal
{
    public class Budget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BudgetType { get; set; }
        public bool MultipleWeeks { get; set; }
        public decimal BudgetMonday { get; set; }
        public decimal BudgetTuesday { get; set; }
        public decimal BudgetWednesday { get; set; }
        public decimal BudgetThursday { get; set; }
        public decimal BudgetFriday { get; set; }
        public decimal BudgetSaturday { get; set; }
        public decimal BudgetSunday { get; set; }
        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string BudgetMondayDisplay { get { return BudgetType == 2 ? BudgetMonday.ToString("C2") : BudgetMonday.ToString(); } }
        public string BudgetTuesdayDisplay { get { return BudgetType == 2 ? BudgetTuesday.ToString("C2") : BudgetTuesday.ToString(); } }
        public string BudgetWednesdayDisplay { get { return BudgetType == 2 ? BudgetWednesday.ToString("C2") : BudgetWednesday.ToString(); } }
        public string BudgetThursdayDisplay { get { return BudgetType == 2 ? BudgetThursday.ToString("C2") : BudgetThursday.ToString(); } }
        public string BudgetFridayDisplay { get { return BudgetType == 2 ? BudgetFriday.ToString("C2") : BudgetFriday.ToString(); } }
        public string BudgetSaturdayDisplay { get { return BudgetType == 2 ? BudgetSaturday.ToString("C2") : BudgetSaturday.ToString(); } }
        public string BudgetSundayDisplay { get { return BudgetType == 2 ? BudgetSunday.ToString("C2") : BudgetSunday.ToString(); } }
    }
}
