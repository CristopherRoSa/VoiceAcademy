using System.ComponentModel.DataAnnotations;

namespace VoiceAcademyWEB.DTOs
{
    public class PlayListDTO
    {
        public int IdPlayList { get; set; }
        [Required(ErrorMessage = "El nombre de la play list no puede estar vacío")]
        public string Name { get; set; }
        public int IdUser { get; set; }
        [StringLength(300, ErrorMessage = "La descripcion no puede exceder las 300 posiciones")]
        public string? Description { get; set; }
        public byte[]? Imagen { get; set; }
    }
}
