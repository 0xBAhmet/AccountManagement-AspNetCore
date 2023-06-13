using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AhmetBAYRAM_FinalProject.Entities
{
    [Table("Users")]//Tablo adı
    public class Users
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(30)]
        public string? Name { get; set; } // boş geçilebilir

        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [Required,StringLength(100)]
        public string Password { get; set; }

        public bool Locked { get; set; }//db de yük azaltır

        public DateTime CreatedAt { get; set; } = DateTime.Now; //bulunduğu zamanı varsayılan değer olarak atar.

        [Required]
        public string Role { get; set; } = "User";//varsayılan değer kullanıcı 
    }
    
}
