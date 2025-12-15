namespace ClockNest.Models.UserClaimModel
{
    public class UserClaimsModel
    {
        public string NameIdentifier { get; set; }
        public string Name { get; set; }
        public string UserEmail { get; set; }
        public string CompanyId { get; set; }
        public string RoleTypeId { get; set; }
        public string EmployeeId { get; set; }
        public string IsSwitchCompany { get; set; }
        public string Payroll { get; set; }
        public string XeroPayroll { get; set; }
        public string Region { get; set; }
        public string StaffologyPayroll { get; set; }
        public string LogoutAfter { get; set; }
        public string CascadeCredentialId { get; set; }
        public string Role { get; set; }
        public List<string> AdditionalRoles { get; set; } = new();
    }
}
