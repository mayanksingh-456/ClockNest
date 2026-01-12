namespace ClockNest.Models.SelfService_Model
{
    public class DashboardChartDataset
    {
        public string Label { get; set; }

        public List<decimal> Data { get; set; }

        public DashboardChartDataset()
        {
            Data = new List<decimal>();
        }
    }
}
