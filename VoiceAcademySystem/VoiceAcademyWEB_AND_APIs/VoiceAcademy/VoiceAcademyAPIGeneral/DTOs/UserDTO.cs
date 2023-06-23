using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VoiceAcademyAPI.DTOs
{
    public class UserDTO
    {

        public int IdUser { get; set; }
        [Required(ErrorMessage = "Email de usuario es requerido")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email de usuario")]
        public string email { get; set; }

        [Required(ErrorMessage = "La edad del usuario es requerida")]
        [DisplayName("Edad del usuario")]
        public int age { get; set; }

        [Required(ErrorMessage = "El nombre del usuario es requerido")]
        [DisplayName("Nombre del usuario")]
        public string name { get; set; }

        [Required(ErrorMessage = "El apellido del usuario es requerido")]
        [DisplayName("Apellido del usuario")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "La contraseña del usuario es requerida")]
        [DisplayName("Contraseña del usuario")]
        public string password { get; set; }
        public byte[]? ImageUser { get; set; }
    }
}