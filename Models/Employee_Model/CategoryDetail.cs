namespace ClockNest.Models.Employee_Model
{
    public class CategoryDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryTypeId { get; set; }
        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public CategoryType CategoryType { get; set; }
    }
}
