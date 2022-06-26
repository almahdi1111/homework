using System.ComponentModel.DataAnnotations;

namespace homeworksatarday.Models.RequestDTO
{
    public class UserUpdateDTO
    {

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(15)]
        public string Password { get; set; }
    }
}
