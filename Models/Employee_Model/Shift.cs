using static Azure.Core.HttpHeader;

namespace ClockNest.Models.Employee_Model
{
    public class Shift
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public bool Isselected { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public int CoreStart { get; set; }
        public int CoreEnd { get; set; }
        public int Contracted { get; set; }
        public int Holiday { get; set; }
        public int Holiday_1st { get; set; }
        public int Holiday_2nd { get; set; }
        public int? TagId { get; set; }
        public int TS_ClockIn { get; set; }
        public int GP_ClockIn { get; set; }
        public int TS_BreakStart { get; set; }
        public int GP_BreakStart { get; set; }
        public int TS_BreakEnd { get; set; }
        public int GP_BreakEnd { get; set; }
        public int TS_ChangeActivity { get; set; }
        public int GP_ChangeActivity { get; set; }
        public int TS_ClockOut { get; set; }
        public int GP_ClockOut { get; set; }
        public int ZR_Start1 { get; set; }
        public int ZR_End1 { get; set; }
        public int ZR_Direction1 { get; set; }
        public int ZR_Start2 { get; set; }
        public int ZR_End2 { get; set; }
        public int ZR_Direction2 { get; set; }
        public int ZR_Start3 { get; set; }
        public int ZR_End3 { get; set; }
        public int ZR_Direction3 { get; set; }
        public int ZR_Start4 { get; set; }
        public int ZR_End4 { get; set; }
        public int ZR_Direction4 { get; set; }
        public int ZR_Start5 { get; set; }
        public int ZR_End5 { get; set; }
        public int ZR_Direction5 { get; set; }
        public int ZR_Start6 { get; set; }
        public int ZR_End6 { get; set; }
        public int ZR_Direction6 { get; set; }
        public int ZR_Start7 { get; set; }
        public int ZR_End7 { get; set; }
        public int ZR_Direction7 { get; set; }
        public int ZR_Start8 { get; set; }
        public int ZR_End8 { get; set; }
        public int ZR_Direction8 { get; set; }
        public bool OT_AfterContracted { get; set; }
        public int OT_After { get; set; }
        public int? DailyOvertimeRuleId { get; set; }
        public int OT_Fixed1 { get; set; }
        public int? OT_Fixed_Group1 { get; set; }
        public bool? OT_Fixed_Authorised1 { get; set; }
        public int OT_Fixed2 { get; set; }
        public int? OT_Fixed_Group2 { get; set; }
        public bool? OT_Fixed_Authorised2 { get; set; }
        public int OT_Fixed3 { get; set; }
        public int? OT_Fixed_Group3 { get; set; }
        public bool? OT_Fixed_Authorised3 { get; set; }
        public int OT_Fixed4 { get; set; }
        public int? OT_Fixed_Group4 { get; set; }
        public bool? OT_Fixed_Authorised4 { get; set; }
        public int OT_Fixed5 { get; set; }
        public int? OT_Fixed_Group5 { get; set; }
        public bool? OT_Fixed_Authorised5 { get; set; }
        public int OT_Zoned_Start1 { get; set; }
        public int OT_Zoned_End1 { get; set; }
        public int? OT_Zoned_Group1 { get; set; }
        public bool? OT_Zoned_Authorised1 { get; set; }
        public int OT_Zoned_Start2 { get; set; }
        public int OT_Zoned_End2 { get; set; }
        public int? OT_Zoned_Group2 { get; set; }
        public bool? OT_Zoned_Authorised2 { get; set; }
        public int OT_Zoned_Start3 { get; set; }
        public int OT_Zoned_End3 { get; set; }
        public int? OT_Zoned_Group3 { get; set; }
        public bool? OT_Zoned_Authorised3 { get; set; }
        public int OT_Zoned_Start4 { get; set; }
        public int OT_Zoned_End4 { get; set; }
        public int? OT_Zoned_Group4 { get; set; }
        public bool? OT_Zoned_Authorised4 { get; set; }
        public int OT_Zoned_Start5 { get; set; }
        public int OT_Zoned_End5 { get; set; }
        public int? OT_Zoned_Group5 { get; set; }
        public bool? OT_Zoned_Authorised5 { get; set; }
        public bool AUT_ClockIn_Enabled { get; set; }
        public int AUT_ClockIn { get; set; }
        public int AUT_ClockIn_At { get; set; }
        public bool AUT_ClockOut_Enabled { get; set; }
        public int AUT_ClockOut { get; set; }
        public int AUT_ClockOut_At { get; set; }
        public bool AUT_BreakStart_Enabled { get; set; }
        public int AUT_BreakStart { get; set; }
        public bool AUT_BreakEnd_Enabled { get; set; }
        public int AUT_BreakEnd { get; set; }
        public int? ShiftStartClockIn { get; set; }
        public int? ShiftEndClockIn { get; set; }
        public int? AutoClockOutAfter { get; set; }
        public bool? RestDay { get; set; }
        public bool OmitFromWeeklyOvertime { get; set; }
        public bool CountsTowardsDailyAndWeeklyOvertime { get; set; }
        public int LatenessGraceMins { get; set; }
        public int LeftEarlyGraceMins { get; set; }
        public bool UseJobAndFinish { get; set; }
        public bool DontExceedShiftEnd { get; set; }
        public int? ActivityId { get; set; }
        public bool Clawback { get; set; }
        public bool Archived { get; set; }
        public string ArchivedDisplay { get { return Archived == true ? "Yes" : "No"; } }
        public bool NoCOEWhenConHoursWorked { get; set; }
        public bool NoCOEWhenAbsence { get; set; }
        public bool ApplyShiftPremium { get; set; }
        public int? ExceptionalItemTypeId { get; set; }
        public decimal ShiftPremiumPercentage { get; set; }
        public decimal ShiftPremiumAmount { get; set; }
        public bool ShiftPremiumApplyToBasic { get; set; }
        public decimal HourlyRate { get; set; }
        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string ContractedDisplay { get { return Common.Helper.GetHoursMinsString(Contracted); } }
        public string StartDisplay { get { return Common.Helper.GetHoursMinsString(Start); } }
        public string EndDisplay { get { return Common.Helper.GetHoursMinsString(End); } }
    }
}
