using ClockNest.Enum;

namespace ClockNest.Models.WorkRecordNotes_Model
{
    public class RealClocking
    {
        public int Id { get; set; }

        public int? EmployeeShiftId { get; set; }

        public int? EmployeeId { get; set; }

        public DateTime ClockingTime { get; set; }

        public int ClockingType { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public int? CostCentreId { get; set; }

        public string CostCentreCode { get; set; }

        public enumClockingDevice ClockingDevice { get; set; }

        public int CompanyId { get; set; }

        public int CreatedByUserId { get; set; }

        public int? UpdatedByUserId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string ClockingTimeDisplay => ClockingTime.ToString("HH:mm dd/MM/yyyy ");

        public string ClockingTypeDisplay
        {
            get
            {
                switch (ClockingType)
                {
                    case 0:
                        return "Clocked Out";
                    case 10:
                        return "Clocked In";
                    case 20:
                        return "Start Break";
                    case 30:
                        return "End Break";
                    case 40:
                        return "Change Activity";
                    case 50:
                        {
                            string text = ((CostCentreCode != string.Empty) ? (" - " + CostCentreCode) : "");
                            return "Change Cost Centre" + text;
                        }
                    default:
                        return "-";
                }
            }
        }

        public string ClockingDeviceDisplay => ClockingDevice switch
        {
            enumClockingDevice.None => "System",
            enumClockingDevice.SmartPhone => "Smartphone",
            enumClockingDevice.SelfService => "SelfService",
            enumClockingDevice.Accutouch => "Accutouch",
            enumClockingDevice.Paymate => "Paymate",
            enumClockingDevice.Access => "Access",
            enumClockingDevice.GT => "GT Series",
            enumClockingDevice.IT => "IT Series",
            enumClockingDevice.Manual => "Manual",
            enumClockingDevice.SmartPhoneOffLine => "Smartphone Offline",
            _ => "-",
        };

        public int? FaceRecognitionExceptionId { get; set; }

        public int? FaceRecognitionPhotoSpotCheckId { get; set; }

        //Added
        public string ClockingDate => ClockingTime.ToString("dd/MM/yyyy");
        public string ClockingTimeOnly => ClockingTime.ToString("HH:mm");
    }
}
