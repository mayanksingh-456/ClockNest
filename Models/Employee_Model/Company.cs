namespace ClockNest.Models.Employee_Model
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Vat { get; set; }

        public string LandlineNumber { get; set; }

        public string Website { get; set; }

        public string Email { get; set; }

        public int AddressId { get; set; }

        public bool Locked { get; set; }

        public string StartOfYear { get; set; }

        public bool UseJoinDate { get; set; }

        public string StartOfYearSick { get; set; }

        public bool UseJoinDateSick { get; set; }

        public string StartOfYearAbsence { get; set; }

        public bool UseJoinDateAbsence { get; set; }

        public string StartOfYearAnnualisedHours { get; set; }

        public string StartOfWeek { get; set; }

        public bool UseRingfencing { get; set; }

        public int? LogoutAfter { get; set; }

        public bool? UseBarcodeScannerInMobileApp { get; set; }

        public int ContactlessLogic { get; set; }

        public int Payroll { get; set; }

        public bool? UseTwoFactorAuthentication { get; set; }

        public bool HideJobNumber { get; set; }

        public bool HideCustomHolidays { get; set; }

        public bool ApplySSPRules { get; set; }

        public string TimeZone { get; set; }

        public string Culture { get; set; }

        public int XeroPayroll { get; set; }

        public int? PasswordPolicyTypeId { get; set; }

        public bool? EnableLockOnTablet { get; set; }

        public string TabletLockPin { get; set; }

        public bool EnableFacialRecognitionSpotCheck { get; set; }

        public bool EnableCostCentreSelectAtClockIn { get; set; }

        public int CreatedByUserId { get; set; }

        public int? UpdatedByUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? SystemExpiryDate { get; set; }

        public int BudgetPlanType { get; set; }

        public decimal AccrualCalculation { get; set; }

        public bool EntitlementsInHrsMins { get; set; }

        public bool DefaultDocumentsToSensitive { get; set; }

        public bool ApplyAverageHolidayPay { get; set; }

        public bool StaffologyPayroll { get; set; }
        public bool CalendarSyncEnabled { get; set; }

        public bool AssignShiftsToTags { get; set; }
    }
}
