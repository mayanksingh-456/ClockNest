namespace ClockNest.Models.Employee_Model
{
    public class CategoryType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HideFromSelfService { get; set; }
        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
