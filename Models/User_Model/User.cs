using ClockNest.Models.Role;
using ClockNest.Models.User_Model;

namespace ClockNest.Models.User
{
    public class User
    {
        public int Id { get; set; }

        public int? ReportToUserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool Active { get; set; }

        public bool Locked { get; set; }

        public short LoginFailedAttemptCount { get; set; }

        public short RoleTypeId { get; set; }

        public RoleType RoleType { get; set; }

        public short? UserAccountStatusTypeId { get; set; }

        public UserAccountStatusType UserAccountStatusType { get; set; }

        public string Salt { get; set; }

        public string LandlineNumber { get; set; }

        public string MobileNumber { get; set; }

        public bool? ForcePasswordChange { get; set; }

        public string JobTitle { get; set; }

        public DateTime CreationDateTimeUtc { get; set; }

        public DateTime LastLoginDateTimeUtc { get; set; }

        public int CreatedByUserId { get; set; }

        public int? UpdatedByUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public int? AddressId { get; set; }

        public int? CompanyId { get; set; }

        public string FullName => FirstName + " " + LastName;

        public List<RoleType> RoleTypes { get; set; }

        public int? PhotoId { get; set; }

        public Photo Photo { get; set; }

        public List<UserAccess> UserAccess { get; set; }

        public List<UserSettingAccess> UserSettingAccess { get; set; }

        public List<UserValueAccess> UserValueAccess { get; set; }

        public int? EmployeeId { get; set; }

        public bool ReceiveEmails { get; set; }

        public bool ReceiveHREmails { get; set; }

        public bool? IsSystemAdminUser { get; set; }

        public bool? IsMobileAppUser { get; set; }

        public bool? IsSelfServiceUser { get; set; }

        public bool? IsAutoSignUpUser { get; set; }

        public bool? IsAutoSignUpWelcomeDetailsShown { get; set; }

        public DateTime? AutoSignUpTrialPeriodExpiryDate { get; set; }

        public bool? IsRegisteredForFaceRecognition { get; set; }

        public DateTime? AccountExpiryDate { get; set; }

        public string FaceRecognitionPersonId { get; set; }

        public bool IsSwitchCompany { get; set; }

        public bool? IsVisitorModuleUser { get; set; }

        public string PushNotificationToken { get; set; }

        public bool? IsPayrollRunUser { get; set; }

        public Customer CustomerDetails { get; set; }

        public bool IsAutoSignUpTrialPeriodExpired { get; set; }

        public bool? IsRegisteredWithAuthenticatorApp { get; set; }

        public bool? IsManagerAppUser { get; set; }

        public int SwitchCompanyId { get; set; }

        public int? ManagerAppFireReportTagId { get; set; }

        public string MobileAppRecentVersionDetails { get; set; }

        public User()
        {
            RoleTypes = new List<RoleType>();
            Photo = new Photo();
            UserAccess = new List<UserAccess>();
            UserSettingAccess = new List<UserSettingAccess>();
            UserValueAccess = new List<UserValueAccess>();
            CustomerDetails = new Customer();
        }
        public string PhotoBase64 { get; set; }
    }
}
