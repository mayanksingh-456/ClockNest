namespace ClockNest.Models.Employee_Model
{
    public class EmployeeTagDetails
    {
        public bool Selected { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public static explicit operator EmployeeTagDetails(List<EmployeeTagDetails> v)
        {
            throw new NotImplementedException();
        }
    }
}
