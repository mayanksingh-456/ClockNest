using ClockNest.ViewModels.EmployeeViewModel;

namespace ClockNest.ViewModels.UserViewModel
{
    public class CreateEditUserGeneralAccessViewModel
    {
        public CreateEditUserGeneralAccessViewModel()
        {
            Tags = new List<TagSelectViewModel>();
            ValueAccessTypes = new List<ValueAccessTypeSelectViewModel>();
        }

        public List<TagSelectViewModel> Tags { get; set; }
        public List<ValueAccessTypeSelectViewModel> ValueAccessTypes { get; set; }
    }
}
