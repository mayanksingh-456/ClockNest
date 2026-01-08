namespace ClockNest.Models.Employee_Model
{
    public class ManagerEmployeeDetails
    {
        public ManagerEmployeeDetails()
        {

            ManagerEmployees = new List<EmployeeWithPhoto>();
        }
        public EmployeeWithPhoto Manager { get; set; }

        public List<EmployeeWithPhoto> ManagerEmployees { get; set; }
    }
}
