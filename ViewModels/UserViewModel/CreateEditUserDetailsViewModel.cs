using ClockNest.Models.Employee_Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Resources;

namespace ClockNest.ViewModels.UserViewModel
{
    public class CreateEditUserDetailsViewModel
    {
        public CreateEditUserDetailsViewModel()
        {
            CreateEditUserDetailsPhotoViewModel = new CreateEditUserDetailsPhotoViewModel();
            //RoleTypesSelect = new List<SelectListItem>();
            EmployeesSelect = new List<SelectListItem>();
            Employees = new List<Employee>();
        }

        public CreateEditUserDetailsPhotoViewModel CreateEditUserDetailsPhotoViewModel { get; set; }
        public int? Id { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }


        public string FullName => FirstName + " " + LastName;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceName = "PasswordMustBe8Characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceName = "PasswordMustBe8Characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceName = "PasswordsDoNotMatch")]
        public string ConfirmPassword { get; set; }

        //[Display(Name = "Role")]
        public short RoleTypeId { get; set; }

        //public List<SelectListItem> RoleTypesSelect { get; set; }

        [Display(Name = "ForcePasswordChange")]
        public bool ForcePasswordChange { get; set; }

        [Display(Name = "SendWelcomeEmail")]
        public bool SendWelcomeEmail { get; set; }

        [Display(Name = "Photo")]
        public byte[] Photo { get; set; }

        public int? PhotoId { get; set; }

        public bool RequestResetPassword { get; set; }

        [Display(Name = "IsEmployee")]
        public int? EmployeeId { get; set; }

        public List<SelectListItem> EmployeesSelect { get; set; }

        public List<Employee> Employees { get; set; }

        [Display(Name = "ReceiveTAAlerts")]
        public bool ReceiveEmails { get; set; }

        [Display(Name = "ReceiveHRAlerts")]
        public bool ReceiveHREmails { get; set; }

        [Display(Name = "SystemAdminUser")]
        public bool IsSystemAdminUser { get; set; }
        [Display(Name = "MobileAppUser")]
        public bool IsMobileAppUser { get; set; }
        [Display(Name = "SelfServiceUser")]
        public bool IsSelfServiceUser { get; set; }

        [Display(Name = "PayrollRunUser")]
        public bool IsPayrollRunUser { get; set; }

        [Display(Name = "VisitorModuleUser")]
        public bool IsVisitorModuleUser { get; set; }

        [Display(Name = "ManagerAppUser")]
        public bool IsManagerAppUser { get; set; }

        public int SystemUsersRemaining { get; set; }
        public int MobileAppUsersRemaining { get; set; }
        public int SelfServiceUsersRemaining { get; set; }
        public int PayrollRunUsersRemaining { get; set; }
        public int VisitorModuleUsersRemaining { get; set; }
        public int ManagerAppUsersRemaining { get; set; }

        public int? CompanyId { get; set; }

        public bool? IsRegisteredForFaceRecognition { get; set; }

        public string FaceRecognitionPersonId { get; set; }

        public int? ManagerAppFireReportTagId { get; set; }
        public string MobileAppRecentVersionDetails { get; set; }

        public DateTime LastLoginDateTimeUtc { get; set; }
        public string LastLoginDate => LastLoginDateTimeUtc.ToString("dd MMM yyyy");
        public string LastLoginTime => LastLoginDateTimeUtc.ToString("t");

        public string AccountType
        {
            get
            {
                var type = IsSystemAdminUser ? "Admin" : "";
                if (IsSelfServiceUser)
                    type += type.Length > 0 ? ", Self Service" : "Self Service";
                if (IsMobileAppUser)
                    type += type.Length > 0 ? ", Mobile" : "Mobile";
                if (IsVisitorModuleUser)
                    type += type.Length > 0 ? ", Visitor Module" : "Visitor Module";
                if (IsPayrollRunUser)
                    type += type.Length > 0 ? ", Payroll" : "Payroll";
                if (IsManagerAppUser)
                    type += type.Length > 0 ? ", Manager App" : "Manager App";

                return type;
            }
        }

        public string SelectedEmployeeIds { get; set; }
    }
}
