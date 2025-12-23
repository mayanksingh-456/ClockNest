namespace ClockNest.ViewModels.EmployeeViewModel
{
    public class CreateEditEmployeeViewModel
    {
        public CreateEditEmployeeViewModel()
        {
            CreateEditEmployeeDetailsViewModel = new CreateEditEmployeeDetailsViewModel();
            CreateEditEmployeePersonalInfoViewModel = new CreateEditEmployeePersonalInfoViewModel();
            CreateEditEmployeeAdditionalInfoViewModel = new CreateEditEmployeeAdditionalInfoViewModel();
            //CreateEditEmployeeEntitlementsViewModel = new CreateEditEmployeeEntitlementsViewModel();
            //CreateEditEmployeeCustomViewModel = new CreateEditEmployeeCustomViewModel();
            CreateEditEmployeeNotesViewModel = new CreateEditEmployeeNotesViewModel();
            CreateEditEmployeeTagsViewModel = new CreateEditEmployeeTagsViewModel();
            //CreateEditEmployeeItemViewModel = new CreateEditEmployeeItemViewModel();
            //CreateEditEmployeeWorkflowViewModel = new CreateEditEmployeeWorkflowViewModel();
            //CreateEditEmployeePayrollViewModel = new CreateEditEmployeePayrollViewModel();
            //CreateEditEmployeeDocumentsViewModel = new CreateEditEmployeeDocumentsViewModel();
            //CreateEditEmployeeAnnualisedHoursViewModel = new CreateEditEmployeeAnnualisedHoursViewModel();

            CreateEditEmployeePhotoViewModel = new CreateEditEmployeePhotoViewModel();

            CreateEditEmployeeManagerEmployeeViewModel = new CreateEditEmployeeManagerEmployeeViewModel();

        }

        public int? Id { get; set; }
        public int CompanyId { get; set; }

        public CreateEditEmployeeDetailsViewModel CreateEditEmployeeDetailsViewModel { get; set; }
        public CreateEditEmployeePersonalInfoViewModel CreateEditEmployeePersonalInfoViewModel { get; set; }
        public CreateEditEmployeeAdditionalInfoViewModel CreateEditEmployeeAdditionalInfoViewModel { get; set; }
        //public CreateEditEmployeeEntitlementsViewModel CreateEditEmployeeEntitlementsViewModel { get; set; }
        //public CreateEditEmployeeCustomViewModel CreateEditEmployeeCustomViewModel { get; set; }
        public CreateEditEmployeeNotesViewModel CreateEditEmployeeNotesViewModel { get; set; }
        public CreateEditEmployeeTagsViewModel CreateEditEmployeeTagsViewModel { get; set; }
        //public CreateEditEmployeeItemViewModel CreateEditEmployeeItemViewModel { get; set; }
        //public CreateEditEmployeeWorkflowViewModel CreateEditEmployeeWorkflowViewModel { get; set; }
        //public CreateEditEmployeePayrollViewModel CreateEditEmployeePayrollViewModel { get; set; }
        //public CreateEditEmployeeDocumentsViewModel CreateEditEmployeeDocumentsViewModel { get; set; }
        //public CreateEditEmployeeAnnualisedHoursViewModel CreateEditEmployeeAnnualisedHoursViewModel { get; set; }

        public CreateEditEmployeePhotoViewModel CreateEditEmployeePhotoViewModel { get; set; }

        public CreateEditEmployeeManagerEmployeeViewModel CreateEditEmployeeManagerEmployeeViewModel { get; set; }
    }
}
