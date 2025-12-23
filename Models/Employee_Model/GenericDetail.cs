using System.ComponentModel.DataAnnotations;

namespace ClockNest.Models.Employee_Model
{
    public class GenericDetail
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Display(Name = "GenericType")]
        [Required(ErrorMessage = "Please select type")]
        public int? GenericTypeId { get; set; }
        public int CompanyId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<GenericType> GenericTypes { get; set; }
        public GenericType GenericType { get; set; }
    }
}
