using ClockNest.Common;

namespace ClockNest.Models.Dashboard_Model
{
    public class DashboardTopCostCentres
    {
        public string CostCentreName { get; set; }

        public int TotalMins { get; set; }

        public string TotalHoursMinsDisplay => Helper.GetHoursMinsString(TotalMins);
    }
}
