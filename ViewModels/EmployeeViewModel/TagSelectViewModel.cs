using ClockNest.Models.Employee_Model;

namespace ClockNest.ViewModels.EmployeeViewModel
{
    public class TagSelectViewModel
    {
        public bool Selected { get; set; }
        public int TagId { get; set; }
        public string Name { get; set; }

        public static explicit operator TagSelectViewModel(List<EmployeeTagDetails> v)
        {
            throw new NotImplementedException();
        }
    }
}
