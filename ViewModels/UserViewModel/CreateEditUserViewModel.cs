namespace ClockNest.ViewModels.UserViewModel
{
    public class CreateEditUserViewModel
    {
        public CreateEditUserViewModel()
        {
            CreateEditUserDetailsViewModel = new CreateEditUserDetailsViewModel();
            CreateEditUserGeneralAccessViewModel = new CreateEditUserGeneralAccessViewModel();
            CreateEditUserAccessViewModel = new CreateEditUserAccessViewModel();
            //CreateEditUserMobileAppAccessViewModel = new CreateEditUserMobileAppAccessViewModel();
            //CreateEditUserSettingAccessViewModel = new CreateEditUserSettingAccessViewModel();
            //CreateEditUserSelfServiceAccessViewModel = new CreateEditUserSelfServiceAccessViewModel();
            //CreateEditUserEmployeeAccessViewModel = new CreateEditUserEmployeeAccessViewModel();
            //CreateEditUserManagerAppAccessViewModel = new CreateEditUserManagerAppAccessViewModel();
        }

        public CreateEditUserDetailsViewModel CreateEditUserDetailsViewModel { get; set; }

        public CreateEditUserGeneralAccessViewModel CreateEditUserGeneralAccessViewModel { get; set; }

        public CreateEditUserAccessViewModel CreateEditUserAccessViewModel { get; set; }

        //public CreateEditUserMobileAppAccessViewModel CreateEditUserMobileAppAccessViewModel { get; set; }

        //public CreateEditUserSettingAccessViewModel CreateEditUserSettingAccessViewModel { get; set; }

        //public CreateEditUserSelfServiceAccessViewModel CreateEditUserSelfServiceAccessViewModel { get; set; }

        //public CreateEditUserEmployeeAccessViewModel CreateEditUserEmployeeAccessViewModel { get; set; }

        //public CreateEditUserManagerAppAccessViewModel CreateEditUserManagerAppAccessViewModel { get; set; }


        public int? Id { get; set; }
    }
}
