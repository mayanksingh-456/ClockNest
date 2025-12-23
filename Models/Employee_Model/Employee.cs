using System.Diagnostics;

namespace ClockNest.Models.Employee_Model
{
    public class Employee
    {
        public Employee()
        {
            EmployeeTagDetails = new List<EmployeeTagDetails>();
            Titles = new List<Title>();
            Rosters = new List<Roster>();
            Activities = new List<Activity>();
            CostCentres = new List<CostCentre>();
            TerminalGroups = new List<TerminalGroupDetails>();
            GTTerminalGroups = new List<GTTerminalGroupDetails>();
            EmployeeTag = new List<EmployeeTag>();
            EmployeeOnOffSite = new List<EmployeeOnOffSite>();
            EmployeeItem = new List<EmployeeItem>();
            AbsencePeriod = new List<AbsencePeriod>();
            EmployeePhoto = new EmployeePhoto();
        }

        public int Id { get; set; }
        public int PersonTypeId { get; set; }
        public string? BadgeId { get; set; }
        public string BadgeId2 { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public int? TitleId { get; set; }

        public string FullName => Forename + " " + Surname;

        public List<EmployeeTagDetails> EmployeeTagDetails { get; set; }

        public string EmployeeTagDetailsDisplay
        {
            get
            {
                var displayText = string.Empty;
                //return String.Join(", ", TerminalDetails.FindAll(t=>t.Name).ToArray());
                EmployeeTagDetails.ForEach(delegate (EmployeeTagDetails t)
                {
                    displayText += t.Name + " ";

                });

                return displayText;
            }

        }

        public DateTime? DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public string Mobile2 { get; set; }
        public string Home { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public int? RosterId { get; set; }
        public int Rotation { get; set; }
        public int? ActivityId { get; set; }
        public int? CostCentreId { get; set; }
        public decimal? HourlyRate { get; set; }
        public decimal? DailyRate { get; set; }
        public decimal? MonthlyRate { get; set; }
        public decimal? AnnualRate { get; set; }
        public int? TerminalGroupId { get; set; }
        public int? GTTerminalGroupId { get; set; }
        public int? AccessGroupId { get; set; }
        public int? FlexitimeRuleId { get; set; }
        public string PayType { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? LeavingDate { get; set; }
        public string Gender { get; set; }
        public string NationalInsuranceNo { get; set; }
        public string PayRollNo { get; set; }
        public string ReasonForLeaving { get; set; }
        public string Notes { get; set; }
        public bool? FirstAider { get; set; }
        public bool? FireMarshall { get; set; }
        public bool? Supervisor { get; set; }
        public string NextOfKin { get; set; }
        public string NextOfKinContactNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Postcode { get; set; }
        public string JobTitle { get; set; }
        public int? ManagerId { get; set; }
        public bool PinOnly { get; set; }
        public string BankName { get; set; }
        public string BankAccountName { get; set; }
        public string BankSortCode { get; set; }
        public string BankAccountNo { get; set; }
        public string PayFrequency { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
        public string Custom4 { get; set; }
        public string Custom5 { get; set; }
        public string Custom6 { get; set; }
        public string Custom7 { get; set; }
        public string Custom8 { get; set; }
        public string Custom9 { get; set; }
        public string Custom10 { get; set; }
        public int Offset { get; set; }
        public int? AccrualRuleId { get; set; }
        public int? SSPRuleId { get; set; }
        public int? EmploymentTypeId { get; set; }
        public int? PayCategory { get; set; }
        public bool IsMigrated { get; set; }
        public decimal? PensionSacrificeAmount { get; set; }
        public string ShapeCompanyId { get; set; }
        public decimal TargetHours1 { get; set; }
        public decimal TargetHours2 { get; set; }
        public decimal TargetHours3 { get; set; }
        public decimal TargetHours4 { get; set; }
        public decimal TargetHours5 { get; set; }
        public string CustomLabel1 { get; set; }
        public string CustomLabel2 { get; set; }
        public string CustomLabel3 { get; set; }
        public string CustomLabel4 { get; set; }
        public string CustomLabel5 { get; set; }
        public string CustomLabel6 { get; set; }
        public string CustomLabel7 { get; set; }
        public string CustomLabel8 { get; set; }
        public string CustomLabel9 { get; set; }
        public string CustomLabel10 { get; set; }
        public bool EntitlementInHours { get; set; }
        public bool DisableCarriedOver { get; set; }
        public string HolidayType1 { get; set; }
        public decimal HolidayEntitlement1 { get; set; }
        public decimal HolidayCarried1 { get; set; }
        public decimal HolidayTaken1 { get; set; }
        public string HolidayType2 { get; set; }
        public decimal HolidayEntitlement2 { get; set; }
        public decimal HolidayCarried2 { get; set; }
        public decimal HolidayTaken2 { get; set; }
        public string HolidayType3 { get; set; }
        public decimal HolidayEntitlement3 { get; set; }
        public decimal HolidayCarried3 { get; set; }
        public decimal HolidayTaken3 { get; set; }
        public string HolidayType4 { get; set; }
        public decimal HolidayEntitlement4 { get; set; }
        public decimal HolidayCarried4 { get; set; }
        public decimal HolidayTaken4 { get; set; }
        public string HolidayType5 { get; set; }
        public decimal HolidayEntitlement5 { get; set; }
        public decimal HolidayCarried5 { get; set; }
        public decimal HolidayTaken5 { get; set; }
        public string HolidayType6 { get; set; }
        public decimal HolidayEntitlement6 { get; set; }
        public decimal HolidayCarried6 { get; set; }
        public decimal HolidayTaken6 { get; set; }
        public string HolidayType7 { get; set; }
        public decimal HolidayEntitlement7 { get; set; }
        public decimal HolidayCarried7 { get; set; }
        public decimal HolidayTaken7 { get; set; }
        public string HolidayType8 { get; set; }
        public decimal HolidayEntitlement8 { get; set; }
        public decimal HolidayCarried8 { get; set; }
        public decimal HolidayTaken8 { get; set; }
        public string HolidayType9 { get; set; }
        public decimal HolidayEntitlement9 { get; set; }
        public decimal HolidayCarried9 { get; set; }
        public decimal HolidayTaken9 { get; set; }
        public string HolidayType10 { get; set; }
        public decimal HolidayEntitlement10 { get; set; }
        public decimal HolidayCarried10 { get; set; }
        public decimal HolidayTaken10 { get; set; }
        public decimal SicknessEntitlement { get; set; }
        public decimal SicknessCarried { get; set; }
        public decimal SicknessTaken { get; set; }
        public decimal AbsenceEntitlement { get; set; }
        public decimal AbsenceCarried { get; set; }
        public decimal AbsenceTaken { get; set; }
        public int CompanyId { get; set; }
        public int? TagId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string SerialNumber { get; set; }
        public List<Title> Titles { get; set; }
        public EmployeeCascadeEmployees EmployeeCascadeEmployees { get; set; }
        public List<Roster> Rosters { get; set; }
        public List<Activity> Activities { get; set; }
        public List<CostCentre> CostCentres { get; set; }
        public List<TerminalGroupDetails> TerminalGroups { get; set; }
        public List<GTTerminalGroupDetails> GTTerminalGroups { get; set; }
        public List<EmployeeTag> EmployeeTag { get; set; }
        public List<EmployeeOnOffSite> EmployeeOnOffSite { get; set; }
        public List<EmployeeItem> EmployeeItem { get; set; }
        public List<AbsencePeriod> AbsencePeriod { get; set; }
        public EmployeePhoto EmployeePhoto { get; set; }
        public string RosterName { get; set; }
        public string ActivityName { get; set; }
        public string CostCentreName { get; set; }
        public string TerminalGroupName { get; set; }
        public string GTTerminalGroupName { get; set; }
        public string AccessGroupName { get; set; }
        public string FlexitimeRuleName { get; set; }
        public string EmploymentTypeName { get; set; }
        public string SSPRuleName { get; set; }
        public string AccrualRuleName { get; set; }
        public string HolidayRuleName { get; set; }
        public int? NationalityTypeId { get; set; }
        public int? EthnicTypeId { get; set; }
        public int? ReligionTypeId { get; set; }

        public int? StaffologyPayCodeId { get; set; }
        public string TimeZone { get; set; }
        public Guid? EarningsRateId { get; set; }
        public string PhotoBase64 { get; set; }
        public byte[] Photo { get; set; }
        public List<int> TagIds { get; set; } = new List<int>();
        public string PhotosBase64
        {
            get
            {
                if (Photo == null || Photo.Length == 0)
                    return null;

                return $"data:image/png;base64,{Convert.ToBase64String(Photo)}";
            }
        }
    }
}
