namespace ClockNest.Models.Employee_Model
{
    public class CompanyLicenceDetails
    {
        public int CompanyId { get; set; }
        public int? NumberOfEmployees { get; set; }
        public int? NumberOfSystemUsers { get; set; }
        public int? NumberOfMobileAppUsers { get; set; }
        public int? NumberOfSelfServiceUsers { get; set; }
        public int? NumberOfVisitorModuleUsers { get; set; }
        public int? NumberOfPayrollRunUsers { get; set; }
        public int? NumberOfManagerAppUsers { get; set; }
        public int? EmployeesRemaining { get; set; }
        public int? SystemUsersRemaining { get; set; }
        public int? MobileAppUsersRemaining { get; set; }
        public int? SelfServiceUsersRemaining { get; set; }
        public int? VisitorModuleUsersRemaining { get; set; }
        public int? PayrollRunUsersRemaining { get; set; }
        public int? ManagerAppUsersRemaining { get; set; }
        public bool IsUserLicenceAvailable { get; set; }
        public bool IsEmployeeLicenceAvailable { get; set; }
        public int? EmployeesUsed { get; set; }
        public int? SystemUsersUsed { get; set; }
        public int? MobileAppUsersUsed { get; set; }
        public int? SelfServiceUsersUsed { get; set; }
        public int? VisitorModuleUsersUsed { get; set; }
        public int? PayrollRunUsersUsed { get; set; }
        public int? ManagerAppUsersUsed { get; set; }
        public bool HasTandA { get; set; }
        public bool HasHR { get; set; }
        public bool HasSmartApp { get; set; }
        public bool HasPayroll { get; set; }
        public bool HasVisitor { get; set; }
        public bool HasManagerApp { get; set; }
        public bool HasSelfService { get; set; }
        public DateTime? SystemExpiryDate { get; set; }
        public int ActionByUserId { get; set; }
    }
}
