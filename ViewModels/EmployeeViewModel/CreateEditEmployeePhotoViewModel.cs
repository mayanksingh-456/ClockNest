using System.ComponentModel.DataAnnotations;

namespace ClockNest.ViewModels.EmployeeViewModel
{
    public class CreateEditEmployeePhotoViewModel
    {
        [Display(Name = "Photo")]
        public byte[] Photo { get; set; }
    }
}
