using ClockNest.Common;
using ClockNest.Models.Employee_Model;
using ClockNest.Models.SelfService_Model;
using System;

namespace ClockNest.Models.WorkRecordNotes_Model
{
    public class WorkRecordAllDetails
    {
        public DateTime ShiftDate { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public byte[] EmployeePhoto { get; set; }

        //public string EmployeePhotoBase64 => Convert.ToBase64String(EmployeePhoto);
        public string EmployeePhotoBase64
        {
            get
            {
                if (EmployeePhoto == null || EmployeePhoto.Length == 0)
                    return string.Empty;

                return Convert.ToBase64String(EmployeePhoto);
            }
        }


        public int EmployeeShiftId { get; set; }

        public string ShiftCode { get; set; }

        public ScheduledShift ScheduledShift { get; set; }

        public RosterDay RosterShift { get; set; }

        public string Notes { get; set; }

        public bool Late { get; set; }

        public bool Locked { get; set; }

        public bool Verified { get; set; }

        public List<EmployeeShift> EmployeeShifts { get; set; }

        public List<WorkRecord> WorkRecords { get; set; }

        public List<RealClocking> RealClockings { get; set; }

        public List<Overtime> Overtime { get; set; }

        public List<AbsenteeRecord> Absence { get; set; }

        public List<Exceptions> Exceptions { get; set; }

        public List<ExceptionalItem> ExceptionalItems { get; set; }

        public string ShiftDateDisplay => $"{ShiftDate:dddd, MMMM d, yyyy}";

        public string TotalDurationDisplay
        {
            get
            {
                int num = 0;
                foreach (WorkRecord workRecord in WorkRecords)
                {
                    if (workRecord.Duration.HasValue)
                    {
                        num += workRecord.Duration.Value;
                    }
                }

                if (WorkRecords.Count == 0)
                {
                    return "";
                }

                return "(" + Helper.GetHoursMinsString(num) + ")";
            }
        }

        public string TotalAmountDisplay
        {
            get
            {
                decimal num = default(decimal);
                foreach (WorkRecord workRecord in WorkRecords)
                {
                    if ((workRecord.HourlyRate.HasValue || workRecord.Duration.HasValue) && workRecord.Duration.HasValue && workRecord.Duration.Value > 0)
                    {
                        num += decimal.Round((decimal)workRecord.Duration.Value / 60m * workRecord.HourlyRate.Value, 2, MidpointRounding.AwayFromZero);
                    }
                }

                if (WorkRecords.Count == 0)
                {
                    return "";
                }

                return "(" + num.ToString("C2") + ")";
            }
        }

        public WorkRecordAllDetails()
        {
            WorkRecords = new List<WorkRecord>();
            RealClockings = new List<RealClocking>();
            Overtime = new List<Overtime>();
            Absence = new List<AbsenteeRecord>();
            EmployeeShifts = new List<EmployeeShift>();
            Exceptions = new List<Exceptions>();
            ExceptionalItems = new List<ExceptionalItem>();
        }
    }
}
