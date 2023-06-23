using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VoiceAcademyAPIGeneral.DTOs
{
    public class EditUserDTO
    {
        public int IdUser { get; set; }
        [Required(ErrorMessage = "La edad del usuario es requerida")]
        [DisplayName("Edad del usuario")]
        public int Age { get; set; }

        [Required(ErrorMessage = "El nombre del usuario es requerido")]
        [DisplayName("Nombre del usuario")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El apellido del usuario es requerido")]
        [DisplayName("Apellido del usuario")]
        public string LastName { get; set; }
        public byte[]? ImageUser { get; set; }
    }
}
