namespace ClockNest.Models.Dashboard_Model
{
    public class DashboardTopJobAndFinish
    {
        public string EmployeeName { get; set; }

        public int TotalMins { get; set; }

        public decimal TotalHours => (decimal)Math.Round((double)TotalMins / 60.0, 2);
    }
}
