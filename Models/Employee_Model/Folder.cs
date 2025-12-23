namespace ClockNest.Models.Employee_Model
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public ICollection<Folder> ChildFolders { get; set; }
        public int ParentFolderId { get; set; }
        public Folder ParentFolder { get; set; }
        public Company Company { get; set; }
        public ICollection<EmployeeDocument> EmployeeDocument { get; set; }
    }
}
