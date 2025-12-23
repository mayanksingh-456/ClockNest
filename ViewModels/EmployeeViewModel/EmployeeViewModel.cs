namespace ClockNest.ViewModels.EmployeeViewModel
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public int PersonTypeId { get; set; }
        public byte[] Photo { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public int? TitleId { get; set; }

        public string FullName => Forename + " " + Surname;
    }
}
