namespace ClockNest.Models.Login
{
    public class CompanyLicenceModule
    {
        public int CompanyId { get; set; }

        public int LicenceModuleTypeId { get; set; }

        public int? UpdatedByUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public LicenceModuleType LicenceModuleType { get; set; }
    }
}
