namespace ClockNest.Models.WorkRecordNotes_Model
{
    public class Exceptions
    {
        public int Id { get; set; }

        public int ExceptionTypeId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime ShiftDate { get; set; }

        public string ShiftDateDisplay => ShiftDate.ToString("d");

        public DateTime ExceptionTime { get; set; }

        public bool Alert { get; set; }

        public string Raised => Alert ? "Yes" : "No";


        public int CompanyId { get; set; }

        public int CreatedByUserId { get; set; }

        public int? UpdatedByUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string ExceptionName { get; set; }

        public string EmployeeName { get; set; }
        public string? Forename { get; set; }

        public string? Surname { get; set; }

        public string FullName => Forename + " " + Surname;

        public byte?[] Photo { get; set; }
        public string BadgeId { get; set; }
        public string PayrollNo { get; set; }

        public string ExceptionTimeDisplay => CreatedDate.ToString("G");

        //    public string PhotoBase64 => Photo != null
        //? $"data:image/png;base64,{Convert.ToBase64String(Photo.Where(b => b.HasValue).Select(b => b.Value).ToArray())}"
        //: null;

        public string PhotoBase64
        {
            get
            {
                if (Photo == null || !Photo.Any(b => b.HasValue))
                    return null;

                var bytes = Photo.Where(b => b.HasValue).Select(b => b.Value).ToArray();
                if (bytes.Length == 0)
                    return null;

                return $"data:image/png;base64,{Convert.ToBase64String(bytes)}";
            }
        }

        public string Notes { get; set; }
    }
}
