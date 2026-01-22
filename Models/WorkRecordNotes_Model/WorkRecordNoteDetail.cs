namespace ClockNest.Models.WorkRecordNotes_Model
{
    public class WorkRecordNoteDetail
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime ShiftDate { get; set; }
        public string ShiftDateDisplay => ShiftDate.ToString("d");
        public string Notes { get; set; }
        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateDisplay => CreatedDate.ToString("d");
        public DateTime? UpdatedDate { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoBase64
        {
            get
            {
                if (Photo == null || Photo.Length == 0)
                    return null;

                return $"data:image/png;base64,{Convert.ToBase64String(Photo)}";
            }
        }
    }
}
