namespace ClockNest.Models.WorkRecordNotes_Model
{
    public class EmployeeAccessControlSwipes
    {
        public int EmployeeId { get; set; }

        public DateTime SwipedAt { get; set; }

        public string SwipedAtDisplay => SwipedAt.ToString("HH:mm:ss");

        public bool AccessGranted { get; set; }

        public string AccessGrantedDisplay => AccessGranted ? "Yes" : "No";

        public int ReaderId { get; set; }

        public string ReaderName { get; set; }

        public int ZoneId { get; set; }

        public string ZoneName { get; set; }

        public string ZoneColour { get; set; }
    }
}
