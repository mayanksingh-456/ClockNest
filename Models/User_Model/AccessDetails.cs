using ClockNest.Models.Employee_Model;
using ClockNest.Models.Tag_Modal;

namespace ClockNest.Models.User_Model
{
    public class AccessDetails
    {
        public AccessDetails()
        {
            AccessTypes = new List<AccessType>();
            UserAccess = new List<UserAccess>();
            Employees = new List<Employee>();
            MobileAppAccessTypes = new List<MobileAppAccessType>();
            UserMobileAppAccess = new List<UserMobileAppAccess>();
            SettingAccessTypes = new List<SettingAccessType>();
            UserSettingAccess = new List<UserSettingAccess>();
            SelfServiceAccessTypes = new List<SelfServiceAccessType>();
            UserSelfServiceAccess = new List<UserSelfServiceAccess>();
            ValueAccessTypes = new List<ValueAccessType>();
            UserValueAccess = new List<UserValueAccess>();
            //EmployeeAccessTypes = new List<EmployeeAccessType>();
            //UserEmployeeAccess = new List<UserEmployeeAccess>();
            //ManagerAppAccessTypes = new List<ManagerAppAccessType>();
            //UserManagerAppAccess = new List<UserManagerAppAccess>();
        }

        public List<AccessType> AccessTypes { get; set; }

        public List<UserAccess> UserAccess { get; set; }
        public List<ValueAccessType> UserValueAccessTypes { get; set; }

        public List<Tag> Tags { get; set; }

        public List<UserTag> UserTags { get; set; }

        public int? UserId { get; set; }
        public int? UpdatedByUserId { get; set; }


        public List<Employee> Employees { get; set; }

        public List<MobileAppAccessType> MobileAppAccessTypes { get; set; }

        public List<UserMobileAppAccess> UserMobileAppAccess { get; set; }

        public List<SettingAccessType> SettingAccessTypes { get; set; }

        public List<UserSettingAccess> UserSettingAccess { get; set; }

        public List<SelfServiceAccessType> SelfServiceAccessTypes { get; set; }

        public List<UserSelfServiceAccess> UserSelfServiceAccess { get; set; }

        //public List<EmployeeAccessType> EmployeeAccessTypes { get; set; }

        //public List<UserEmployeeAccess> UserEmployeeAccess { get; set; }

        public List<ValueAccessType> ValueAccessTypes { get; set; }

        public List<UserValueAccess> UserValueAccess { get; set; }

        //public List<ManagerAppAccessType> ManagerAppAccessTypes { get; set; }

        //public List<UserManagerAppAccess> UserManagerAppAccess { get; set; }

        public bool? IsRegisteredForFaceRecognition { get; set; }

        public string FaceRecognitionPersonId { get; set; }
    }
}
