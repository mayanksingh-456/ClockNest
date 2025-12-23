namespace ClockNest.Models.Employee_Model
{
    public class CompanyDocument
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public byte[] Document { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
