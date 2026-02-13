using ClockNest.Common;

namespace ClockNest.Models.Dashboard_Model
{
    public class DashboardTopActivities
    {
        public string ActivityName { get; set; }

        public int TotalMins { get; set; }

        public string TotalHoursMinsDisplay => Helper.GetHoursMinsString(TotalMins);
    }
}
