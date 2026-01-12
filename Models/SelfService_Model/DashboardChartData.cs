namespace ClockNest.Models.SelfService_Model
{
    public class DashboardChartData
    {
        public List<string> Labels { get; set; }

        public List<DateTime> Dates { get; set; }

        public List<DashboardChartDataset> Datasets { get; set; }
        public List<EmployeeCategoryDto> EmployeeCategory { get; set; }
        public DashboardChartData()
        {
            Datasets = new List<DashboardChartDataset>();
            Labels = new List<string>();
            Dates = new List<DateTime>();
            EmployeeCategory = new List<EmployeeCategoryDto>();
        }
    }
}
