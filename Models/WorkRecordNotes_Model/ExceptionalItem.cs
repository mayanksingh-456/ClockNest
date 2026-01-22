using ClockNest.Common;
using ClockNest.Models.Employee_Model;

namespace ClockNest.Models.WorkRecordNotes_Model
{
    public class ExceptionalItem
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime ShiftDate { get; set; }

        public int ExceptionalItemTypeId { get; set; }

        public int? CostCentreId { get; set; }

        public int? Count { get; set; }

        public int? Duration { get; set; }

        public decimal? Cost { get; set; }

        public int CompanyId { get; set; }

        public int CreatedByUserId { get; set; }

        public int? UpdatedByUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public ExceptionalItemType ExceptionalItemType { get; set; }

        public CostCentre CostCentre { get; set; }

        public string ExceptionalItemTypeDisplay
        {
            get
            {
                if (ExceptionalItemType == null)
                {
                    return "";
                }

                return ExceptionalItemType.Name;
            }
        }

        public string CostCentreDisplay
        {
            get
            {
                if (CostCentre == null)
                {
                    return "-";
                }

                return CostCentre.Name;
            }
        }

        public string DurationDisplay
        {
            get
            {
                if (!Duration.HasValue)
                {
                    return "";
                }

                return Helper.GetHoursMinsString(Duration.Value);
            }
        }

        public string CostDisplay
        {
            get
            {
                if (!Cost.HasValue)
                {
                    return "";
                }

                return Cost.Value.ToString("C2");
            }
        }
    }
}
