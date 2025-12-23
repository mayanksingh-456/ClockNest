namespace ClockNest.Models.Employee_Model
{
    public class AccessGroupDetails
    {
        public AccessGroupDetails()
        {
            AccessDetails = new List<AccessGroupAccessDetails>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public List<AccessGroupAccessDetails> AccessDetails { get; set; }

        public string AccessDetailsDisplay { get; set; }

        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
