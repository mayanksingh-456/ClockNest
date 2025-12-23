namespace ClockNest.ViewModels.EmployeeViewModel
{
    public class CreateEditEmployeeTagsViewModel
    {
        public CreateEditEmployeeTagsViewModel()
        {
            TagDetails = new List<TagSelectViewModel>();
        }

        public int CompanyId { get; set; }
        public List<int> CurrentUserTagIds { get; set; }
        public List<TagSelectViewModel> TagDetails { get; set; }
    }
}
