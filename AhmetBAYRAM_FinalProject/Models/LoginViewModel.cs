using System.ComponentModel.DataAnnotations;

namespace AhmetBAYRAM_FinalProject.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Username Is Required")]
        [StringLength(30)]

        public string Username { get; set; }


        [Required(ErrorMessage = "Password Is Required")]
        [MinLength(6)]
        [MaxLength(16)]
        
        public string Password { get; set; }
    }
}
