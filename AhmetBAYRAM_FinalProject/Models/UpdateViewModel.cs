using System.ComponentModel.DataAnnotations;


namespace AhmetBAYRAM_FinalProject.Models
{
    public class UpdateViewModel
    {

        [Required(ErrorMessage = "Password Is Not Correct")]
        [MinLength(6)]
        [MaxLength(16)]

        public string Currently_Password { get; set; }



        [Required(ErrorMessage = "Password Is Required")]
        [MinLength(6)]
        [MaxLength(16)]

        public string Password { get; set; }


        [Required(ErrorMessage = "Password Is Required")]
        [MinLength(6)]
        [MaxLength(16)]

        public string RePassword { get; set; }


    }
}
