namespace ClockNest.Models.SelfService_Model
{
    public class SelfServiceEmployeePayrollDocuments
    {
        public string DocumentDisplay { get; set; }
        public int MaxYear { get; set; }
        public string DocumentType { get; set; }
        public string SelectedYear { get; set; }
        public List<string> Years
        {
            get
            {
                var years = new List<string>();
                var currentYear = DateTime.Now.Year;

                // financial years from 2018–2019 to current
                for (int y = currentYear; y >= 2018; y--)
                {
                    years.Add($"{y}-{y + 1}");
                }

                return years;
            }
        }
        public SelfServiceEmployeePayrollDocuments()
        {
            // default select first year (latest one)
            if (Years.Any())
            {
                SelectedYear = Years.First();
            }
        }
    }
}
