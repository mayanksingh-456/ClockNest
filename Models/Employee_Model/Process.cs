namespace ClockNest.Models.Employee_Model
{
    public class Process
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ClientTypeId { get; set; }

        public int CompanyId { get; set; }

        public int CreatedByUserId { get; set; }

        public int? UpdatedByUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
