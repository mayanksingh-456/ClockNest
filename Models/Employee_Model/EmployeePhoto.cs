namespace ClockNest.Models.Employee_Model
{
    public class EmployeePhoto
    {
        public int Id { get; set; }
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
