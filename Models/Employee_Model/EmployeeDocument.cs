namespace ClockNest.Models.Employee_Model
{
    public class EmployeeDocument
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public byte[] Document { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsCompanyDocument { get; set; }
        public bool Sensitive { get; set; }
        public string SensitiveDisplay { get { return Sensitive == true ? "Yes" : "No"; } }
        public int ParentFolderId { get; set; }
    }
}
