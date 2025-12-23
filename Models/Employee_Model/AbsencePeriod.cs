namespace ClockNest.Models.Employee_Model
{
    public class AbsencePeriod
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int? AccrualRuleId { get; set; }
        public bool EntitlementsInHours { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
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
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int CompanyId { get; set; }
        public byte[] Photo { get; set; }
    }
}
