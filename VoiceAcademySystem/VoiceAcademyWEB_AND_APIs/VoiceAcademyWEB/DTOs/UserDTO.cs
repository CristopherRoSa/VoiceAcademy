using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VoiceAcademyWEB.DTOs
{
    public class UserDTO
    {


        [Required(ErrorMessage = "Email de usuario es requerido")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email de usuario")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La edad del usuario es requerida")]
        [DisplayName("Edad del usuario")]
        public int Age { get; set; }

        [Required(ErrorMessage = "El nombre del usuario es requerido")]
        [DisplayName("Nombre del usuario")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El apellido del usuario es requerido")]
        [DisplayName("Apellido del usuario")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "La contraseña del usuario es requerida")]
        [DisplayName("Contraseña del usuario")]
        public string Password { get; set; }
        public byte[]? ImageUser { get; set; }

    }
}
