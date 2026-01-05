using ClockNest.ViewModels.EmployeeViewModel;

namespace ClockNest.ViewModels.UserViewModel
{
    public class CreateEditUserAccessViewModel
    {
        public CreateEditUserAccessViewModel()
        {
            AccessTypes = new List<AccessTypeSelectViewModel>();
            Tags = new List<TagSelectViewModel>();
            ValueAccessTypes = new List<ValueAccessTypeSelectViewModel>();
        }

        public List<AccessTypeSelectViewModel> AccessTypes { get; set; }

        public List<TagSelectViewModel> Tags { get; set; }

        public List<ValueAccessTypeSelectViewModel> ValueAccessTypes { get; set; }
    }
}
