using System.ComponentModel.DataAnnotations;

namespace HW_18_Student_Journal.ViewModel
{
    public class CreateGroupViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength =3, ErrorMessage = "Group name must be between 3 and 25 characters.")]
        public string Name { get; set; }
    }
}
