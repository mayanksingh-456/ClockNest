using System.ComponentModel.DataAnnotations;

namespace ClockNest.ViewModels.EmployeeViewModel
{
    public class CreateEditEmployeePersonalInfoViewModel
    {
        [Display(Name = "Address1")]
        public string Address1 { get; set; }

        [Display(Name = "Address2")]
        public string Address2 { get; set; }

        [Display(Name = "Address3")]
        public string Address3 { get; set; }

        [Display(Name = "Address4")]
        public string Address4 { get; set; }

        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        [Display(Name = "Mobile1")]
        public string Mobile { get; set; }

        [Display(Name = "Mobile2")]
        public string Mobile2 { get; set; }

        public string Home { get; set; }

        [Display(Name = "Email1")]
        public string Email { get; set; }

        [Display(Name = "Email2")]
        public string Email2 { get; set; }

        [Display(Name = "NextOfKin")]
        public string NextOfKin { get; set; }

        [Display(Name = "NextOfKinContactNo")]
        public string NextOfKinContactNo { get; set; }
    }
}
